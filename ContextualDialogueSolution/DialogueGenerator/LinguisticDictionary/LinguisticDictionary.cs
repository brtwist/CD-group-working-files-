using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.DialogueGenerator.LinguisticDictionary
{
    public partial class LinguisticDictionary
    {
        //private list of list of words
        private TreeNode root;

        public LinguisticDictionary()
        {
            root = new TreeNode("root");
            loadFile("C:\\Users\\benjamint\\Documents\\visual studio 2015\\Projects\\ContextualDialogueSolution\\hibye.vocab");
        }

        //loadfromfile
        private void loadFile(String p)
        {
            string line;
            string[] subStrings;
            TreeNode newTree = new TreeNode("temp");
            TreeNode currentNode = newTree;//initialise current node as root node
            int index;

            System.IO.StreamReader file = new System.IO.StreamReader(p);

            // Read the file and display it line by line.
            while ((line = file.ReadLine()) != null)
            {
                //first eat the file header
                line = line.Trim();

                if (line.Length > 0)//not a blank line containing only whitespace
                {
                    char firstLetter = line[0];

                    /*start processing*/
                    //the first letter could either be a # which means header file or new class or end of class
                    //else it could be an entry to the dictionary
                    if (firstLetter == '#')
                    {
                        //char[] delimiter = new char[2] { '\t', ' ' };
                        subStrings = line.Split(' ');

                        if (subStrings[0].CompareTo("#name") == 0)//header: name of file
                        {
                            newTree.name = subStrings[1];//set name of tree
                        }
                        else if (subStrings[0].CompareTo("#class") == 0)
                        {
                            //check whether class start or class end
                            if (subStrings[1].CompareTo("start") == 0)//header: name of file
                            {
                                //create and insert new node
                                TreeNode newNode = new TreeNode(subStrings[2]);

                                if (subStrings.Length > 3)//check for additional paramaters like saliency
                                {
                                    for (int i = 3; i < subStrings.Length; i++)//if there are paramaters process them one at a time
                                    {
                                        line = subStrings[i];
                                        if (/*line.Contains("=") &&*/ line[line.LastIndexOf('=') - 1].CompareTo('S') == 0) //if the character before the = is S ...
                                            newNode.saliency = double.Parse( line.Substring(line.LastIndexOf('=') + 1));//returns number directly after the = 
                                    }
                                }

                                newNode.parentNode = currentNode;
                                currentNode.add(newNode);

                                currentNode = newNode;
                            }
                            else if (subStrings[1].CompareTo("end") == 0)//end of current class
                            {
                                //go up one level in tree
                                //TODO will break if node has no parent
                                currentNode = currentNode.parentNode;
                            }

                        }
                    }//end first letter '#'

                    else if (firstLetter == '>')//entry
                    {
                        subStrings = line.Split('|');
                        DictionaryEntry newEntry = new DictionaryEntry(subStrings[0].Substring(1).Trim());

                        
                        if (subStrings.Length > 1)//check for additional paramaters like saliency
                        {
                            for (int i = 1; i < subStrings.Length; i++)//if there are paramaters process them one at a time
                            {
                                line = subStrings[i];
                                if (/*line.Contains("=") &&*/ line[line.LastIndexOf('=') - 1].CompareTo('S') == 0) //if the character before the = is S ...
                                    newEntry.salience = double.Parse(line.Substring(line.LastIndexOf('=') + 1));//returns number directly after the = 


                            }
                        }
                        currentNode.add(newEntry);
                    }
                    else if (firstLetter == '%')//commentline
                    {
                        //do nothing
                    }
                    else
                    {
                        //error, should be no other possibility
                    }
                }//end if (blank line)

            }

            file.Close();

            //finally add the new tree to the existing dictionary
            newTree.parentNode = root;
            root.add(newTree);
        }

        /*takes a 'path' in the form "classa/classb/classc/" each representing a node in a path in the tree
         returns random entry from specified class*/
        public String generateRandom(String path)
        {
            //first traverse to target node
            TreeNode node = TraverseToNode(path);
            
            return node.getRandomEntry();
        }
        //search entry-type
        //search entry-y-from-dictionary-x
        //search entry-y-from-dictionary-x vector one
        //search entry-y-from-dictionary-x vector two
        //search entry-y-from-dictionary-x vector one, vector two

        /*the following two functions provide search to node capability*/
        /*the first initialises the paramaters, the second recursively traverses the tree*/
        private TreeNode TraverseToNode(String path)
        {
            char delimiter = '/';
            String[] substrings = path.Split(delimiter);

            return TraverseToNode(root, substrings);
        }
        
         /*takes a node and an array of strings representing a path through the tree
          recursively processes path until it returns the destination node*/
        private TreeNode TraverseToNode(TreeNode currentNode, String[] oldPath)
        {
            while (oldPath.Length > 0)//while (not found)
            {
                //make a new string array, one slot smaller than previously
                String[] newPath = new String[oldPath.Length - 1];
                
                //copy all but one element of old path into new path
                Array.Copy(oldPath, 1, newPath, 0, newPath.Length);
               

                //submit new path as recursive search paramater
                //TODO probably breaks if not found
                currentNode = TraverseToNode(currentNode.searchChildrenFor(oldPath[0]), newPath);
                oldPath = new String[0];//oldpath needs to be emptied now to satisfy while condition
            }
            //retun destination node
            return currentNode;//.searchChildrenFor(oldPath[0]);
        }

        //provides tostring for testing purposes
        public override string ToString()
        {
            return "Dictionary { " + root.ToString() + " }";
        }
    }
}
