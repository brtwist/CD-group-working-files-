using System;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes
{
    [Serializable]
    public class Noun
    {
        public String noun;
        public String plural;
        public Boolean countable;

        //constructors. if no plural is specified, automatically uncountable
        public Noun() { this.countable = false; }

        public Noun(String noun)
        {
            this.noun = noun;
            this.countable = false;
        }

        public Noun(String noun, String plural)
        {
            this.noun = noun;
            this.plural = plural;
            countable = true;
        }


        /*returns pluralised version of noun if one exists*/
        //TODO not sure if this ever gets used because at the moment if you dont specify a plural manualy, it aut sets to uncountable
        public String getPlural()
        {
            if (countable)
            {

                if (plural != null)//irregular plural is already defined
                {
                    return plural;
                }


                else/*TODO code that auto-generates regular plurals*/
                {
                    Char[] lastLetter = new char[2];
                    lastLetter[0] = noun[noun.Length - 2];
                    lastLetter[1] = noun[noun.Length - 1];

                    if (lastLetter[1] == 's' || lastLetter[1] == 'x' || lastLetter[1] == 'z' || lastLetter.Equals("ch") || lastLetter.Equals("sh"))
                        return noun + "es";
                    else
                        return noun + "s";
                }
            }
            else //if its uncountable return it unpluralised
            {
                return noun;
            }
        }

        public void setPlural(String p)
        {
            this.plural = p;
            this.countable = true;
        }

        public void setSingular(String s)
        {
            this.noun = s;
        }

        public void setCountable(bool b)
        {
            countable = b;
        }




    }

}
