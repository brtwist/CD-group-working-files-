using System;
using System.Collections.Generic;
using System.Text;

/*abstract class  designed to represent all knowable things*/

namespace WorldManagerNamespace
{
    [Serializable]
    abstract public class Knowable
    {
        public String noun;

        public String plural;//for irregular plurals

        public Boolean unique = false;
        public Boolean countable = true;

        public String ID;

        /*when given no noun as paramater, automatically takes the class name*/
        public Knowable()
        {
            noun = this.GetType().ToString();
            noun = noun.Substring(noun.LastIndexOf('.') + 1).ToLower();

        }

        /*constructor takes string noun*/
        public Knowable(String n)
        {
            noun = n;
        }

        /*constructor takes string noun and boolean to show proper noun*/
        public Knowable(String n, Boolean pn)
        {
            noun = n;
            unique = pn;
        }

        /*constructor takes singular and irregulr plural*/
        public Knowable(String singular, String p)
        {
            noun = singular;
            plural = p;
        }

        public String getNoun()
        {
            return noun;
        }

        /*returns pluralised version of noun if one exists*/
        public String getPlural()
        {
            if (countable == true)
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




        //public TemporalLocation temporalLocation; should temporal be an enum or a class? or an int?

    }
}
