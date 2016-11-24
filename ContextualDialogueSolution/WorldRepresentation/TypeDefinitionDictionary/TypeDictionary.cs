using EnumNamespace;
using System;
using System.Collections.Generic;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary
{
    [Serializable]
    public class TypeDictionary
    {
        //private list of list of words
        private TypeDefinition root;
        private Dictionary<String, TypeDefinition> dictionary;

        public TypeDictionary()
        {
            dictionary = new Dictionary<string, TypeDefinition>();
            root = new TypeDefinition("root");
            dictionary.Add("root", root);
            loadTypeDefinitions("..\\..\\..\\typeDictionary.typedefs");
        }

        /*FILE LOADER
         * this function loads type definitions from text file
         * algorithm: iteratively build a tree of new typeDefinition objects, then add tree to the main tree
         * */
        public void loadTypeDefinitions(String p)
        {
            string line;
            string[] subStrings;
            //TypeDefinition newTree = new TypeDefinition("temp");
            //TypeDefinition currentNode = newTree;//initialise current node as root node
            TypeDefinition newNode = new TypeDefinition("errorLoadingFile");
            TypeDefinition parentNode;

            try //catch syntax errors in the file
            {
                System.IO.StreamReader file = new System.IO.StreamReader(p);

                // Read the file and process it line by line.
                while ((line = file.ReadLine()) != null)
                {
                    //first trim whitespace
                    line = line.Trim();

                    if (line.Length > 0)//not a blank line containing only whitespace
                    {
                        //pull out the first letter to identify what kind of line it is
                        char firstLetter = line[0];

                        /*start processing*/
                        //the first letter could either be a # which means header file or new class or end of class
                        //else it could be an entry to the dictionary. anything else is garbage
                        if (firstLetter == '#')
                        {
                            //char[] delimiter = new char[2] { '\t', ' ' };
                            subStrings = line.Split(' ');

                            if (subStrings[0].ToLower().CompareTo("#typedefinition") == 0)
                            {

                                //check whether class start or class end
                                if (subStrings[1].CompareTo("start") == 0)//header: name of file
                                {
                                    //create and insert new node
                                    //TypeDefinition newNode;//declare new blank node
                                    try
                                    {
                                        //substring number 4 is the parent class to where the new node should be inserted
                                        //if the parent class does exist, it will be assigned to currentNode
                                        //NOTE breaks everything if the specified parent node isnt found
                                        dictionary.TryGetValue(subStrings[4], out parentNode);
                                    }
                                    catch (ArgumentNullException)
                                    {
                                        throw new FileLoadException("Error: typedefinition isa " + subStrings[4].ToUpper() + " but that was not found amongst already loaded typedefinitions. Check spelling and define types in a top-down order.");
                                    }

                                    //new node inherits all its values from it's parent
                                    newNode = parentNode.shallowCopy();
                                    newNode.typeName = subStrings[2];


                                    //if (subStrings.Length > 4)//check for additional paramaters in the first line, e.g. saliency                                    {
                                    //TODO SALIENCY/typicality
                                    //for (int i = 3; i < subStrings.Length; i++)//if there are paramaters process them one at a time
                                    //{
                                    //    line = subStrings[i];
                                    //    if (/*line.Contains("=") &&*/ line[line.LastIndexOf('=') - 1].CompareTo('S') == 0) //if the character before the = is S ...
                                    //        newNode.saliency = double.Parse( line.Substring(line.LastIndexOf('=') + 1));//returns number directly after the = 
                                    //}
                                    //}
                                    /*TODO else if (no saliency/typicality specified)
                                     * {
                                     * set saliency/typicality to default value
                                     * }*/

                                    newNode.parentNode = parentNode;//tell the new node who its parent is
                                    parentNode.add(newNode);
                                    //currentNode.add(newNode);//add new node to tree
                                    dictionary.Add(subStrings[2], newNode);

                                    //currentNode = newNode;//now in future lines of the file we can access this via current node
                                }
                                else if (subStrings[1].CompareTo("end") == 0)//end of current class
                                {
                                    //all done with that object
                                }

                                else// if #class is not followed by either 'start' nor 'end' a problem exists in the file
                                {
                                    throw new FileLoadException("Error \"#typedefinition\" should be followed by either a \"start\" or \"end\" tag. ");
                                }

                            }
                        }//end first letter '#'

                        else if (firstLetter == '|')//variable entry
                        {
                            subStrings = line.Substring(1).Split('|');//first eat first character, then first substring must be a value, each subsequent substring will be a paramater of the value
                            String field = subStrings[0].ToLower().Trim();
                            int equalSign = field.IndexOf('=');
                            //field = field.Substring(0, equalSign);//now we have sanitised string containing the name of a paramater to add

                            switch (field)
                            {

                                //TODO typicality

                                case "accesslevel":
                                    newNode.accessLevel = parseAccessLevel(subStrings[1]);
                                    break;

                                case "noun"://one or two paramaters: noun object takes either just a noun, or a noun and its plural
                                    newNode.addNoun(parseNoun(subStrings));
                                    break;

                                case "canuseparentnoun":
                                    newNode.canUseParentNoun = parseBoolean(subStrings[1]);
                                    break;

                                case "gradeable":
                                    //newNode.addGradeable(subStrings);
                                    break;

                                case "ability":
                                    newNode.addClassAbility(parseDescriptor(subStrings));
                                    break;

                                case "attribute":
                                    newNode.addClassAttribute(parseDescriptor(subStrings));
                                    break;

                                /*case "comesfrom":
                                    //TODO
                                    break;

                                case "madefrom":
                                    //TODO
                                    break;*/

                                default:
                                    //garbage?
                                    throw new FileLoadException("Unrecognised field \"" + field + "\" (hint: check spelling) or unexpected syntax. ");
                            }


                        }
                        else if (firstLetter == '%')//commentline
                        {
                            //do nothing
                        }
                        else
                        {
                            //error, should be no other possibility
                            //TODO throw exception?
                            throw new FileLoadException("Unexpected text found, check syntax and spelling. ");
                        }
                    }//end if (blank line)
                }//end while (end of file has been reached)

                file.Close();

            }//end try

            catch (FileLoadException ex)
            {
                //TODO handle all these exceptions which have been thrown in the file load area
                //throw new FileLoadException("Error loading type definitions from file " + p + "", ex);
            }


            //finally add the new tree to the existing dictionary
            //newTree.parentNode = root;
            //root.add(newTree);
        }

        //attempt get type
        public TypeDefinition tryGetValue(String typeName)
        {
            //TypeDefinition found;
            //dictionary.TryGetValue(typeName, out found);
            //string test = found.typeName;
            //return found;
            //TODO WILL BREAK IF NOT FOUND

            //TODO FOLLOWING TWO LINES FOR DEBUGGING ONLY
            String[] foos = new string[dictionary.Count];
            dictionary.Keys.CopyTo(foos, 0);


            //try
            //{
            return dictionary[typeName];
            //}
            //catch (KeyNotFoundException)
            //{
            //    Console.WriteLine("Key = \"tif\" is not found.");
            //}

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


        /*PARSING FUNCTIONS*/
        private AccessLevel parseAccessLevel(String value)
        {
            if (value.CompareTo("blacklist") == 0)
                return AccessLevel.BLACKLIST;

            else if (value.CompareTo("public") == 0)
                return AccessLevel.PUBLIC;

            else if (value.CompareTo("private") == 0)
                return AccessLevel.PRIVATE;

            else //(value.CompareTo("whitelist") == 0)
                return AccessLevel.WHITELIST;
            //TODO not sure if whitelist should be the default, an error messge would be nicer
        }

        //can deal with singular and plural
        private Noun parseNoun(String[] subStrings)
        {
            String[] currentParamater;
            //make new descriptor

            Noun result = new Noun();

            try
            {
                //start looping
                foreach (String element in subStrings)
                {
                    currentParamater = element.Split('=');

                    switch (currentParamater[0].Trim().ToLower())
                    {
                        case "noun":
                            //ignore
                            break;

                        case "singular":
                            result.setSingular(currentParamater[1].Trim());
                            break;

                        case "plural":
                            result.setPlural(currentParamater[1].Trim());
                            break;

                        case "countable":
                            result.setCountable(parseBoolean(currentParamater[1].Trim()));
                            break;

                        default:
                            throw new FileLoadException("Syntax error loading noun " + result.noun.ToUpper() + ".");

                    }
                }
            }//end try

            catch (FileLoadException ex)
            {
                throw ex; //throw it up to higher level
            }
            catch (ArgumentException ex)//thrown by incorrect enum parsing
            {
                throw ex; //throw it up
            }

            return result;
        }

        private Boolean parseBoolean(String value)
        {
            //first check what format the input is coming in
            String[] subStrings = value.Split('=');

            if (subStrings.Length > 1)
                value = subStrings[subStrings.Length - 1];
            value = value.Trim();

            //now parse input

            try
            {
                if (value.CompareTo("true") == 0 || value.CompareTo("1") == 0)
                    return true;
                else if (value.CompareTo("false") == 0 || value.CompareTo("0") == 0)
                    return false;
                else
                    throw new FileLoadException("Error parsing boolean: value must be one of \"true\" or \"false\", or \"0\" or \"1\"");
            }
            catch (FileLoadException ex)
            {
                throw ex;
            }
        }

        private Descriptor parseDescriptor(String[] subStrings)
        {
            String[] currentParamater;
            //make new descriptor

            Descriptor result = new Descriptor();

            try
            {
                //start looping
                foreach (String element in subStrings)
                {
                    currentParamater = element.Split('=');

                    switch (currentParamater[0].Trim().ToLower())
                    {
                        case "ability":
                            result.verb = "can";
                            break;

                        case "attribute":
                            result.verb = "is";
                            break;

                        case "trait":
                            result.trait = currentParamater[1];
                            break;

                        case "adjective":
                            result.trait = currentParamater[1];
                            break;

                        case "verb":
                            result.verb = currentParamater[1];
                            break;

                        case "adverb":
                            result.adverb = currentParamater[1];
                            break;

                        case "preposition":
                            result.preposition = (Preposition)Enum.Parse(typeof(Preposition), currentParamater[1]); //TODO catch argument exception in the case that an invalid string goes into these
                            break;

                        //case "location":
                        //    result.location = currentParamater[1];
                        //    break;

                        case "value":
                            result.value = parseBoolean(currentParamater[1]);
                            break;

                        case "tense":
                            result.tense = (Tense)Enum.Parse(typeof(Tense), currentParamater[1]);
                            break;

                        case "probability":
                            result.probability = Double.Parse(currentParamater[1]);
                            break;

                        default:
                            throw new FileLoadException("Syntax error loading Descriptor " + result.trait.ToUpper() + ".");

                    }
                }
            }//end try

            catch (FileLoadException ex)
            {
                throw ex; //throw it up to higher level
            }
            catch (ArgumentException ex)//thrown by incorrect enum parsing
            {
                throw ex; //throw it up
            }

            return result;

        }
    }
}
