using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.DialogueGenerator.LinguisticDictionary
{
    class TreeNode
    {
        private Random r;

        private string nodeName;
        public string name
        {
            get { return nodeName;  }
            set { nodeName = value;  }
        }

        private Double salience;
        public Double saliency
        {
            set { salience = value;  }
            get { return salience; }
        }

        public int count { get { return this.getCountALL(); } }
        public bool hasChildren { get { return children.Count > 0;  } }

        private TreeNode parent;
        public TreeNode parentNode
        {
            get { return parent; }
            set { parent = value; }
        }

        private List<TreeNode> children;//subcategories of words/phrases
        private List<DictionaryEntry> values;//words/phrases

        public TreeNode(String name, float salience)
        {
            nodeName = name;
            this.salience = salience;

            children = new List<TreeNode>();
            values = new List<DictionaryEntry>();
            r = new Random();
        }

        public TreeNode(String name)
        {
            salience = 1.0;//default value
            nodeName = name;

            children = new List<TreeNode>();
            values = new List<DictionaryEntry>();

            r = new Random();
        }

        public void add(DictionaryEntry newEntry)
        {
            values.Add(newEntry);
        }

        public void add(TreeNode newChild)
        {
            children.Add(newChild);
        }

        /*FUNCTION DESCRIPTION getRandomEntry
         * function returns a random dictionaryentry from this node or its children
         * 
         * ALGORITHM:
         * 1 get total count of all entries including entries of children
         * 2 average probability = 1.0 / total
         * 3 now randomly pick one:
         * 3.1 get random number between 0 and total
         * 3.2 take an entry and subtrac its probability from the random number
         * 3.3 if random number is now less than the probability, return this element
         * continue until random number gets small enough to finally pick one
         */
        //TODO implement saliency weighting
        //TODO add in a function for selecting which class of words to choose from among
        //TODO add in politeness etc. metrics
        public String getRandomEntry()
        {
            //first decide which node to return a value from (any of the children or this node itself)
            Double totalChoices = 0.0;

            foreach (TreeNode element in children)
                totalChoices += element.salience;// * element.getCountSALIENT();//TODO doesnt take into account if one category has way more entries... might be bad. means small categories are unfairly advantaged

            if (values.Count > 0)
                totalChoices++;

            //error check
            if (totalChoices == 0)
                return "< error: empty dictionary >";

            //now we have non-zero list of possible nodes to choose from

            Double randomNumber = r.NextDouble() * totalChoices;



            //Double probability = 1.0 / (Double)totalChoices;


            foreach (TreeNode element in children)
            {
                if (randomNumber <= element.saliency)
                    return element.getRandomEntry();//RECURSIVELY SEARCH THIS NODE
                else
                    randomNumber -= element.saliency;
            }


            /*so we chose to get the value from this node instead of a child node*/
            //re-calculate probabilities
            totalChoices = 0.0;

            foreach (DictionaryEntry element in values)
                totalChoices += element.salience;
            
            //error check
            if (totalChoices == 0)
                return "< error: empty dictionary >";

            //now we have non-zero list of possible nodes to choose from

            randomNumber = r.NextDouble()*totalChoices;

            foreach (DictionaryEntry element in values)
            {
                if (randomNumber <= element.salience)
                    return element.value;//ITEM FOUND
                else
                randomNumber -= element.salience;
            }

            //should never get here
            return "<dictionary getRandom() error: no entry chosen>";
        }

//counts all ancestors
        private int getCountALL()
        {
            int count = values.Count;

            foreach (TreeNode element in children)
                count += element.getCountALL();

            return count;
        }

//counts ancestors, ignores branches or values with 0 saliency
        private Double getCountSALIENT()
        {
            Double count = 0.0;

            foreach (DictionaryEntry element in values)
            {
                    count += element.salience;
            }

            foreach (TreeNode element in children)
            {
                if (element.salience > 0)
                    count += element.getCountSALIENT();
            }

            return count;
        }

        //TODO seems to retun null and break system if it finds nothing
        public TreeNode searchChildrenFor(String name)
        {
            //return children.Find(c => c.name == name);//TODO problem here?

            TreeNode node = new TreeNode("error in searchChildrenFor");

            foreach (TreeNode element in children)
            {
                if (element.name.CompareTo(name) == 0)
                    node = element;
            }
            return node;
        }

        public override string ToString()
        {
            String output = " { Node: " + this.name + ": ";

            foreach (DictionaryEntry element in values)
                output += element.value + ", ";
            
            foreach (TreeNode element in children)
                output += element.ToString();
                
            output += " } ";

            return output;
        }

    }
}
