using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary
{
    [Serializable]
    public class TypeDefinition : TreeNode
    {
      //  protected override TreeNode parentNode;
      //  protected override String nodeName;
        private Double typicallity { get; set; }

        /*ACCESS LEVEL*/


        //default access is that everybody knows everything unless otherwise specified
        public AccessLevel accessLevel;// { get; set; }

        /*END ACCESS LEVEL STUFF*/

        /*NOUNS*/
        /*this member doubles as the name of the node, for use in tree-related functions like searching*/
        public string typeName
        {
            get { return nodeName; }
            set { nodeName = value; }
        }

        /*if null, this member returns the typeName instead*/
        private List<Noun> nouns;
        public Boolean canUseParentNoun;// { get; set; }//e.g. REWE can be called Supermarket. Supermarket can be called shop. shop cannot be called building.

        public void addNoun(Noun newNoun) { nouns.Add(newNoun); }

        /*return a random (common) noun, unles no noun is specified*/
        public Noun getRandomCommonNoun()
        {
            //if no noun is set, return parent noun otherwise typename
            if (nouns.Count == 0 || nouns == null)
            {
                if (canUseParentNoun)
                    return ((TypeDefinition)this.parentNode).getRandomCommonNoun();
                else
                    return new Noun(typeName);
            }

            //else: return one of the provided nouns. hopefully nouns are always provided
            int index = this.r.Next(0, nouns.Count);

            return nouns[index];

        }

        //TODO get proper noun if it exists

        /*END NOUNS*/



        /*CLASS ADJECTIVES*/
        /*adjectives: scalar adjectives , values relative to this object's type*/
        /*each scalar also needs some way to be parsed into words. that should probably be defined here even if its just a property that does a dictionary search*/
        //you can do two things with a scalar adjective: you can parse  the scalar to a word e.g. big, or you can use it for comparitive descriptions e.g. bigger, biggest

        //TODO somehow divide the gradeables into different access levels
        Double gradableSize;
        Range rangeSize;

        Double gradableTasty;
        Range rangeTasty;

        Double gradableSpeed;
        Range rangeSpeed;
        /*
        * TODO good (good might be a special case. needs to be linked to other scalars, depending on type. maybe even linked to absolute descriptors. e.g. is the mango good? good=tasty+ripe
        */
        

        /*adjectives: absolute adjectives*/
        /*verbs, pairs of verbs with adjectives and values e.g. "is. red. true" "tastes. bitter. false"*/
        //TODO make this a sorted list
        //TODO make various access levels, probably various lists
        protected List<Descriptor> classAbilities;//list of verb, adjective, value pairs and sometimes prepositions e.g. rhinos have horns on their head
        protected List<Descriptor> classAttributes;

        public void addClassAbility(Descriptor input)
        {
            classAbilities.Add(input);
        }

        public void addClassAttribute(Descriptor input)
        {
            classAttributes.Add(input);
        }

        /*END ADJECTIVES*/



        /*MISCELANEOUS PROPERTIES*/
        //comes from
        //String comesFrom { get; set; }//he comes from
        //made of
        //String madeOf { get; set; }
        //lives in
        /*END MISCELANEOUS PROPERTIES*/


        /*FUNCTIONS*/

        /*constructors*/

        //creates typedefinition with default settings. this should only be called once, all others should come via copying this object
        public TypeDefinition(String name)
        {
            //TODO copy every member value from the input to this one
            this.typeName = name;

            /*DEFAULT SETTINGS*/
            //TODO initialise things
            accessLevel = AccessLevel.BLACKLIST;//by default everybody knows everything, unless explicitly blacklisted

            classAttributes = new List<Descriptor>();
            classAbilities = new List<Descriptor>();

        }


        //returns shallow copy of itself
        //used for copying
        public TypeDefinition shallowCopy()
        {
            TypeDefinition copy = (TypeDefinition)this.MemberwiseClone();
            copy.init();
            return copy;
        }

        private void init()
        {
            nouns = new List<Noun>(); //nouns are always reset, not inherited from parent type
            canUseParentNoun = false; //this is always reset to false; not inherited from parent type
        }


        //TODO some kind of true/false property for every as to whether this object has this adjective/property and get/sets
        //TODO get functions, search functions. get random descriptor etc. as well as a general describe function (getting comparers via gradables)

    }
}
