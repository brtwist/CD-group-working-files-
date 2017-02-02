using System;
using System.Collections.Generic;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary
{
    [Serializable]
    public class PhysicalEntity_Agent : PhysicalEntity
    {
        /*NAME STUFF*/
        private String nameFirst;
        private  String nameLast;
        private String nameFull { get { return nameFirst + " " + nameLast; } }
        private String title;
        public String name(PhysicalEntity_Agent interlocutor)
        {
            string output = "";
            //gives a name based on who is speaking e.g. children call teacher Mr Box but his boss calls him Richard and his friends call him Rick
            //TODO algorithm

            //if(negative powerdifference)
            output += title + " ";

            output += nameFull;

            return output;
        }
        
        /*END NAME STUFF*/

        public enum Gender { neutral, male, female };
        Gender gender;

        //personality

        //attributes, abilities

        /*OPINIONS; LIKES; DISLIKES*/
        private List<Opinion> opinions;
        //searches list for any opinion the agent already has about that particular thing
        public Opinion tryGetOpinion(object thing)
        {
            if (opinions.Count >= 0)
            {
                foreach (Opinion element in opinions)
                {
                    if (element._subject.Equals(thing))
                        return element;
                }
            }
            return null;
        }
        public void addOpinion(Opinion opinion)
        {
            opinions.Add(opinion);
        }

        //relationships
        enum RelationshipDegree { normal, likes, dislikes, likes_strongly, dislikes_strongly }
        enum RelationshipType { aquaintance, friend, boss, employee, colleague } //etc. perhaps one for 1 degree of seperation. e.g. 'i know of him'
        enum PowerDifference { none, higherThanMe, lowerThanMe, extremelyHigherThanMe, ExtremelyLowerThanMe, totalPowerOverMe, totalSubmissionUnderMe }

        struct Relationship
        {
            PhysicalEntity_Agent subject;
            RelationshipDegree relationshipDegree;
            RelationshipType relationshipType;

        }


        //agents probably get defined in a similar way to typedefinitions. some kind of heirachy of groups where e.g. children like sweets
        //better not in a heirachical way but maybe just become members of groups which modify their attributes and abilities
        public PhysicalEntity_Agent(TypeDefinition ty, String nameFirst, String nameLast) : this(ty, nameFirst, nameLast, Gender.neutral)
        { }
        public PhysicalEntity_Agent(TypeDefinition ty, String nameFirst, String nameLast, Gender gender):base(ty)
        {
            this.nameFirst = nameFirst;
            this.nameLast = nameLast;
            this.gender = gender;

            base.properNoun = nameFull;

            if (gender == Gender.male)
                title = "Mr.";
            else if (gender == Gender.female)
                title = "Ms. ";
            else
                title = "";

            opinions = new List<Opinion>();
        }

        public void setTitle(String title)
        {
            this.title = title;
        }
    }
}
