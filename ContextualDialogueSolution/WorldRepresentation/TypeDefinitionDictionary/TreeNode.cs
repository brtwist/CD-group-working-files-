using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary
{
    [Serializable]
    public class TreeNode
    {
        protected Random r;

        public String nodeName;//often refered to as typename
        //protected abstract String getRandomNoun();//used in the inherited classes

        /*saliency currently unused*/
        private double salience { get; set; }


        public int count { get { return this.getCountALL(); } }
        public bool hasChildren { get { return children.Count > 0; } }

        public TreeNode parentNode;

        protected List<TreeNode> children;//subcategories of words/phrases

        public TreeNode() : this(1.0) { }//chained constructor

        public TreeNode(Double salience)
        {
            this.salience = salience;

            children = new List<TreeNode>();

            r = new Random();
        }



        //public void add(DictionaryEntry newEntry)
        //{
        //    values.Add(newEntry);
        //}

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

            //error check
            if (totalChoices == 0)
                return "< error: empty node " + nodeName.ToUpper() + " in dictionary >";

            //now we have non-zero list of possible nodes to choose from

            Double randomNumber = r.NextDouble() * totalChoices;



            //Double probability = 1.0 / (Double)totalChoices;


            foreach (TreeNode element in children)
            {
                if (randomNumber <= element.salience)
                    return element.getRandomEntry();//RECURSIVELY SEARCH THIS NODE
                else
                    randomNumber -= element.salience;
            }

            //should never get here TODO maybe throw exception
            return "<dictionary getRandom() error: no entry chosen>";
        }

        //counts all ancestors
        private int getCountALL()
        {
            int count = 0;

            foreach (TreeNode element in children)
                count += element.getCountALL();

            return count;
        }

        //counts ancestors, ignores branches or values with 0 saliency
        private Double getCountSALIENT()
        {
            Double count = 0.0;

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

            TreeNode node = new TreeNode(-1);//TODO - ERROR if this -1 ever makes it. maybe throw exception?

            foreach (TreeNode element in children)
            {
                if (element.nodeName.CompareTo(name) == 0)
                    node = element;
            }
            return node;
        }

        public override string ToString()
        {
            String output = " { " + this.nodeName + ": ";


            foreach (TreeNode element in children)
                output += element.ToString();

            output += " } ";

            return output;
        }

    }
}
