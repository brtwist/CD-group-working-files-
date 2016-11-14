using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary
{
    [Serializable]
    public class PhysicalEntity //: TypeDefinition 
    {
        public int ID; //unique ID of this object
        public TypeDefinition typeDefinition;

        /*NOUNS*/
        //e.g. John as opposed to 'person'. Aldi as opposed to 'Supermarket' proper nouns usually dont go with an article
        public String properNoun { get; set; }
        public Boolean oneOfAKind { get; set; }

        public Boolean hasProperNoun
        {
            get { return properNoun != null; }
        }

        public Noun getRandomCommonNoun() { return typeDefinition.getRandomCommonNoun();}

        //private Boolean isCountable { get; set; }//much vs many
        private Boolean isUnique { get; set; }//an Eiffel tower vs The Eifel tower
                                              /*END NOUNS*/

        /*POSITION: prepositions positioning the object in the world*/

        //main preposition, i.e. parentNode
        private SpatialLocation spatialParent;
        public SpatialLocation getSpatialparent() { return spatialParent;  }
        public Boolean hasSpatialParent() { return spatialParent == null; }

        //list of secondary prepositions e.g. 'b is ALONG the road from a'
        /*END POSITION*/

        /*OWNERSHIP*/
        //who owns this object
        //who/what is owned by this object

        /*ACCESS LEVEL STUFF*/
        //TODO LIST blacklist
        //TODO LIST friendlist
        //TODO LIST whitelist

        public void friend(/*agent x*/)
        {

        }
        public void unfriend(/*agent x*/)
        {

        }
        public void blacklist(/*agent x*/)
        {

        }
        public void unblacklist(/*agent x*/)
        {

        }
        public void whitelist(/*agent x*/)
        {

        }
        public void unwhitelist(/*agent x*/)
        {

        }
        /*END ACCESS LEVEL STUFF*/

        //constructor
        //public PhysicalEntity(string name) : base(name)
        public PhysicalEntity(TypeDefinition type)
        {
            typeDefinition = type;
            string test = type.typeName;
            
            //TODO??

        }

        public void setSpatialParent(EnumNamespace.Preposition preposition, PhysicalEntity parentEntity)
        {
            spatialParent = new SpatialLocation(this, preposition, parentEntity);
            //TODO add this as a child of parent object to make it a double linked tree
        }

        public override string ToString()
        {
            string result = "";

            if (hasProperNoun)
                result += properNoun + " ";

            result += "(Type: " + typeDefinition.typeName + "  ID: " +
                ID + ") ";

            if (this.spatialParent != null)
            {
                if(spatialParent.adult.hasProperNoun)
                    result += spatialParent.preposition.ToString() + " " +
                spatialParent.adult.properNoun;
                else
                    result += spatialParent.preposition.ToString() + " a/an " +
                spatialParent.adult.getRandomCommonNoun() + ")";
            }
            else
                result += "<no spatial parent>";

            return result;
        }
    }
}
