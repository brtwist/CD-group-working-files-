using System;
using System.Collections.Generic;
using System.Text;
using WorldManagerNamespace;
using EnumNamespace;
using System;

namespace DialogueGeneratorNamespace
{
    //participant 1
    //participant 2
    //type
    //QUD
    //politeness
    //amiability
    //tense

    

    class Conversation
    {

        private World world;
        public String output;
        private Random r;

        public Conversation(World w)
        {
            world = w;
            r = new Random();

            //####################### Testing code
            PhysicalEntity f;
            do
            {
                f = (PhysicalEntity)w.getRandom();
            } while (f.spatialParent == null);//keep going if object has no parent

            Tense t = Tense.present;

            //senseAskAboutFacts(f, f.descriptors[r.Next(1, f.descriptors.Count)]);
            if (r.NextDouble()<0.5 && f.descriptors.Count > 0)
            output = senseAskAboutFacts(f, f.descriptors[r.Next(1, f.descriptors.Count)], t);///////////////////
            else
                output = senseAskAboutWhere(f, t);//////////////////////////
            //####################### end testing code

            //type = 
            //participants = 
            //location = 
            //open stream to output

        }

        public void start()
        {
            //greet

            //while qud not empty
            //step

            //end while

            //bye
        }

        /*FUNCTION DESCRIPTION REQUEST FOR ACTION
         * paramater takes politeness and friendliness ... along with a delegate of the question itself?
         * actions can include give/take/tell/show/sell etc.
         * Production rules: 
         *1)could/would/can/<none> + you + (optional: be so kind as to/be able to) + verb + me/us + the/that/your thing
         *2)could/would/can/<none> + you + (optional: be so kind as to/be able to) + verb + me/us <<wait for response>>
         *
         */



        /*FUNCTION DESCRIPRION ASK ABOUT WHERE (ask about where an object is)
         * Paramaters expects either a type or a specfici object. function assumes object has a spatial parent.
         * if type, should ask about an indefinite. if specific; should ask about definite
         * Asking about facts of a given object
         * 1)AUX + Pr + prep + NP? "is NP/PR in the house?"
         * 2)Where + Aux + proper noun?   "where is osnabruck?" (constraints: unique)
         */
        public String senseAskAboutWhere(PhysicalEntity e1, Tense t)
        {
            return senseAskAboutWhere(e1, e1.spatialParent.adult, t);
        }
        public String senseAskAboutWhere(PhysicalEntity e1, PhysicalEntity e2, Tense t)
        {
            String aux, det1, np1, det2, np2, prep, definiteDeterminer, verb = "";
            int productionRule;
            String output;

            /*CHOOSE PRODUCTION RULE*/
            if (e1.unique)
                productionRule = 2;
            else //tense != future
                productionRule = r.Next(1, 2);

            prep = e1.spatialParent.preposition.ToString();

            np1 = e1.noun;
            np2 = e2.noun;

            //choose det
            det1 = "the";
            if (e1.unique)
                det1 = "";
 
            det2 = "the";
            if (e2.unique)
                det2 = "";


            /*CHOOSE AUX*/
            if (t == Tense.pastSimple)
            {
                aux = "was";
                //assumes singular: were
            }
            else if (t == Tense.future)
            {
                if (r.NextDouble() > 0.5)//50% chance either way between going and will
                {
                    aux = "will";
                    verb = "be";
                }
                else
                {
                    aux = "is";
                    verb = "going to be";
                }

            }
            else /*t == Tense.present*/
            {
                aux = "is";
                //assumes singular: are
            }

            /*ASSEMBLE*/
            if (t == Tense.future)
            {
                if (productionRule == 1)
                    output = aux + " " + det1 + " " + np1 + " " + verb + " " + prep + " " + det2 + " " + np2 + " " + "?";
                else /*Production rule 2*/
                    output = "Where" + " " + aux + " " + det1 + " " + np1 + " " + verb + "?";
            }
            else
            {
                if (productionRule == 1)
                    output = aux + " " + det1 + " " + np1 + " " + prep + " " + det2 + " " + np2 + " " + "?";
                else /*Production rule 2*/
                    output = "Where" + " " + aux + " " + det1 + " " + np1 + "?";
            }


            //remove duplicate whitespace
            output = System.Text.RegularExpressions.Regex.Replace(output, @" {2,}", " ");

            //capitalise first letter
            output = char.ToUpper(output[0]) + output.Substring(1);

            return output;
        }


         /*FUNCTION DESCRIPRION ASK ABOUT FACTS (ask about descriptors)
         * Paramaters expects either a type or a specfici object. plus an enum adjective
         * 
         * Asking about facts of a given object
         * 1)Aux + NP + Adj   "is it nice?", are they nice?, are cakes nice?
         * 2)AUX + Pr + Adj NP? "is it a tasty apple?"
         * 3)Aux + NP + verb   "Does it taste nice?" (constraints: not future tense, has verb) 
         */
        public String senseAskAboutFacts(Knowable o, Descriptor d, Tense t)
        {
            String aux, np, pr, adj, indefiniteDeterminer, definiteDeterminer, verb = "";
            int productionRule;
            String output;

            /*CHOOSE PRODUCTION RULE*/
            if (t == Tense.future || d.verb == EnumNamespace.verb.none)//exclude rule 3 based on constraints
                productionRule = r.Next(1, 1 /*make it 3 to allow pronouns rule*/);//excludes rule 3
            else //tense != future
                productionRule = r.Next(1, 4);

            /*CHOOSE ADJECTIVE*/
            adj = d.adjective.ToString();

            /*CHOOSE NP / PRONOUN*/
            pr = "it";
            //TODO add in other pronouns (yours, this, that, they etc.)

            /*CHOOSE DETERMINERS*/
            //choose indefinite determiner: between a and an
            if (o.countable == true)
            {
                //assumes adjective always comes before noun
                Char firstLetter = d.adjective.ToString()[0];
                bool isVowel = "aeiou".IndexOf(firstLetter) >= 0;

                if (isVowel)
                    indefiniteDeterminer = "an";
                else
                    indefiniteDeterminer = "a";
            }
            else //noun is uncountable
                indefiniteDeterminer = "";

            //choose definite determiner
            definiteDeterminer = "the";
            //assumes singular

            /*CHOOSE NOUN*/
            np = o.getNoun();

           /*RULE 1 or 2*/
            if ( productionRule == 1 || productionRule == 2)
            {
                
                
                /*CHOOSE AUX*/
                if (t == Tense.pastSimple)
                {
                    aux = "was";
                    //assumes singular: were
                }
                else if (t == Tense.future)
                {
                    if (r.NextDouble() > 0.5)//50% chance either way between going and will
                    {
                        aux = "will";
                        if (d.verb == EnumNamespace.verb.none)
                        {
                            verb = "be";
                        }
                        else
                        {
                            verb = d.verb.ToString();
                        }
                    }
                    else
                    {
                        aux = "is";
                        if (d.verb == EnumNamespace.verb.none)
                        {
                            verb = "going to be";
                        }
                        else
                        {
                            verb = "going to " + d.verb.ToString();
                        }
                    }

                }
                else /*t == Tense.present*/
                {
                    aux = "is";
                    //assumes singular: are
                }


                if (t == Tense.future)
                {
                    output = aux + " " + definiteDeterminer + " " + np + " " + verb + " " + adj + "?";
                }
                else
                {
                    if (productionRule == 1)
                        output = aux + " " + definiteDeterminer + " " + np + " " + adj + "?";
                    else /*Production rule 2*/
                        output = aux + " " + pr + " " + adj + "?" + " ( " + pr + " -> " + definiteDeterminer + " " + np + " )";
                }

                //return aux + pr + adj + np

            }
            else //RULE 3
            {
                //3)Aux + Pr + verb   "Does it taste nice?"

                if (t == Tense.pastSimple)
                    aux = "did";
                else //tense == present
                    aux = "does";
                //assumes singular: do

                /*CHOOSE VERB*/
                verb = d.verb.ToString();

                output = aux + " " + " " + definiteDeterminer + " " + np + " " + verb + " " + adj + "?";
            }

            //remove duplicate whitespace
            output = System.Text.RegularExpressions.Regex.Replace(output, @" {2,}", " ");

            //capitalise first letter
            output = char.ToUpper(output[0]) + output.Substring(1);

            return output;
        }

        //public void end()
        //{
        //this function should end the conversation regardless of whats left in the qud
        //takes parameter based on how urgently it should end
        //and maybe a qud paramater such as 'im hungry. need to go'
        //}

        //small function to extract the class name from the namespace name
        private String getType(Object o)
        {
            String s = o.GetType().ToString();
            return s.Substring(s.LastIndexOf('.') + 1).ToLower();
        }
    }
}
