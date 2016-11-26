using ContextualDialogue.WorldManager;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using EnumNamespace;
using System;

namespace ContextualDialogue.DialogueGenerator
{
    //namespace ContextualDialogue.DialogueGenerator.UtteranceGenerator
    //{
    public partial class Conversation //UtteranceGenerator
    {

        /*FUNCTION DESCRIPTION REQUEST FOR ACTION
         * paramater takes politeness and friendliness ... along with a delegate of the question itself?
         * actions can include give/take/tell/show/sell etc.
         * Production rules: 
         *ALL)could/would/can/<none> + you + (optional: be so kind as to/be able to)+ verb +...
         * 1)...  me/us + the/that/your thing
         * 2)... me/us <<wait for response>> MovesQueue.push(yes) MovesQueue.push(state your request)
         * 3)... with this/object/object + for me/us 
         *
         * NOTE: no tense. always present
         * TODO production rules 2 and 3
         */

        public String senseRequestAssistance(String utterer, String participantTwo, Verb v, PhysicalEntity e1)
        {
            String modal, optionalEmphasis, verb;// det1, np1, det2, np2, prep, /*definiteDeterminer,*/ verb = "";
            int productionRule = 1;
            String output = "";

            verb = v.ToString();

            //TODO if(....) choose between could, would, can or none, based on politeness
            modal = "would";

            //TODO if(extra polite) add optional emphasis: be so kind as to/be able to
            optionalEmphasis = "";


            ///*CHOOSE PRODUCTION RULE*/
            //productionRule = r.Next(1, 2);


            /*ASSEMBLE*/

            if (productionRule == 1)
                output = modal + " " + "you" + " " + optionalEmphasis + " " + verb + "?";


            //remove duplicate whitespace
            output = System.Text.RegularExpressions.Regex.Replace(output, @" {2,}", " ");

            //capitalise first letter
            output = char.ToUpper(output[0]) + output.Substring(1);

            return output;
        }



        /*FUNCTION DESCRIPRION ASK ABOUT WHERE (ask about where an object is)
         * Paramaters expects either a type or a specfici object. function assumes object has a spatial parent.
         * if type, should ask about an indefinite. if specific; should ask about definite
         * Asking about facts of a given object
         * 
         * SPECIFIC ENTITY
         * 1)Where + Aux + proper noun?   "where is osnabruck?" (constraints: unique, i.e. one of a kind)
         * 2)where + <modal: might/would/could/can> + I find noun?
         * x)TODO AUX + Pr + prep + NP? "is NP/PR in the house?"
         * 
         * TYPE
         * 4)Where + can i find a/some?   "where is osnabruck?" (constraints: unique, i.e. one of a kind)
         * 5)where + <modal: might/would/could/can> + I find noun?
         * x)TODO AUX + Pr + prep + NP? "is there a/some NP/PR in the house?"
         * 
         * first check whether its a physical entity or a type
         */
        public String senseAskAboutWhere(String utterer, object targetObject, Tense t)
        {
            Type type = targetObject.GetType();
            String subject, output = "";

            /*CHOOSE PRODUCTION RULE*/
            int productionRule;
            if (type == typeof(PhysicalEntity))
                productionRule = r.Next(1, 2);
            else if (type == typeof(TypeDefinition))
                productionRule = r.Next(4, 5);
            else //error
                return "error in senseAskAboutWhere: " + targetObject.ToString() + " neither a Physical Entity nor a Type Definition.";

            subject = renderConstituent(targetObject, false);

            if (productionRule == 1)

            output = "where is " + subject + "?";
            //remove duplicate whitespace
            output = System.Text.RegularExpressions.Regex.Replace(output, @" {2,}", " ");

            //capitalise first letter
            output = char.ToUpper(output[0]) + output.Substring(1);

            return output;
        }

        /*tells the location of an object, pragmatically taking into account the location of the conversation
         * 1)the x is in spatialGrandparent, in spatialParent
         */
        public String senseTellAboutWhere(String utterer, PhysicalEntity entitySubject, PhysicalEntity conversationLocation)
        {
            String subject, preposition, spatialParent;
            String output = "";

            subject = renderConstituent(entitySubject, false);

            
            
            PhysicalEntity[] path = world.getLocationARelativeToB(conversationLocation, entitySubject);

            /*at this point we have the direct path from the lowest common ancestor to the subject
             * now we will render that into a string*/
            if (path.Length <= 2)
                return "this is " + subject;

            int count = path.Length -2;
           while (count > 0)
            {
                output += " " + path[count].getSpatialparent().preposition.ToString() + " " + renderConstituent(path[count], false);
                count--;
            }

            //assemble output
            output = subject + " is " + output;

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
        public String senseAskAboutFacts(String utterer, PhysicalEntity o, Descriptor d, Tense t)
        {
            String aux, np, pr, adj, verb = "";
            int productionRule;
            String constituent;
            String output;

            /*CHOOSE PRODUCTION RULE*/
            if (t == Tense.future || d.verb.CompareTo("is") == 0/*ie descriptor has no special verb*/)//exclude rule 3 based on constraints
                productionRule = r.Next(1, 1 /*make it 3 to allow pronouns rule*/);//excludes rule 3
            else //tense != future
                productionRule = r.Next(1, 4);

            /*CHOOSE ADJECTIVE*/
            adj = d.trait.ToString();

            constituent = renderConstituent(o, false);

            /*RULE 1 or 2*/
            if (productionRule == 1 || productionRule == 2)
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
                        if (d.verb.CompareTo("is") == 0/*see if descriptor has a special verb defined*/)
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
                        if (d.verb.CompareTo("is") == 0/*see if descriptor has a special verb or just is*/)
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
                    output = aux + " " + constituent + verb + " " + adj + "?";
                }
                else
                {
                    if (productionRule == 1)
                        output = aux + " " + constituent + " " + adj + "?";
                    else /*Production rule 2*/
                        output = aux + " " + constituent + " " + adj + "?";
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

                output = aux + " " + constituent + " " + verb + " " + adj + "?";
            }

            //remove duplicate whitespace
            output = System.Text.RegularExpressions.Regex.Replace(output, @" {2,}", " ");

            //capitalise first letter
            output = char.ToUpper(output[0]) + output.Substring(1);

            return output;
        }

        /*render noun constituent
         * this function does two things
         * first checks what for the noun should be, e.g. plural uncountable
         * second checks what type of determiner should have, e.g. a/an/some/any
         */
        public String renderConstituent(object o, bool plural)
        {
            String output;

            //check whether its a type or an entity
            Type type = o.GetType();

            if (type == typeof(PhysicalEntity))
            {
                PhysicalEntity e = (PhysicalEntity)o;

                //first check whether itshould be a pronoun
                //todo, and if so what the pronoun should be
                //output = "it";//your, my, their, she, he, him
                //TODO add in other pronouns (yours, this, that, they etc.)


                /*CHOOSE NOUN*/
                

                if (e.hasProperNoun)//if it has a proper name, use it
                    output = e.properNoun;
                else
                {
                    Noun nounObject = e.getRandomCommonNoun();
                    output = "the" + nounObject.noun;

                    if (plural && nounObject.countable)
                        output = "the " + nounObject.plural;
                }
            }
            else if (type == typeof(TypeDefinition))
            {
                TypeDefinition t = (TypeDefinition)o;

                //first check whether itshould be a pronoun
                //todo, and if so what the pronoun should be
                //output = "it";//your, my, their, she, he, him
                //TODO add in other pronouns (yours, this, that, they etc.)


                /*CHOOSE NOUN*/

                //if (e.hasProperNoun)//if it has a proper name, use it
                //    output = e.properNoun; a type should probably never have a proper noun unless its something like a chain store, e.g. REWE
                
                    Noun nounObject = t.getRandomCommonNoun();
                if (!plural)
                {
                    //a horse
                    if (nounObject.countable)
                    output = LinguisticDictionary.LinguisticDictionary.getAorAN(nounObject.noun) + nounObject.noun;
                    else //not countable
                        output = "some " + nounObject.noun;
                }
                else //(plural)
                {
                    //some horse
                    if (!nounObject.countable)
                        output = "some " + nounObject.noun;
                    else //nounObject is countable
                        output = "some " + nounObject.plural;
                }
            }
            else
                output = "error parsing constituent in UtteranceGenerator.cs : " + o.ToString() + " is neither a Physical Entity nor a Type Definition";
            return output;

        }




        /*FUNCTION DESCRIPTION GREETING
         * paramater takes politeness and friendliness and either one or two participants
         */
        //TODO overloaded version that includes name or title of the person being greeted
        public string senseGreeting(String utterer, String otherPerson /*politeness etc.*/)
        {
            String output = "";
            int productionRule;


            output = vocabDictionary.generateRandom("hibye/greeting");

            //50% chance of using other persons name
            if (r.NextDouble() > 0.5f)
                output += " " + otherPerson;

            //50% chance of explamation point
            if (r.NextDouble() > 0.5f)
                output += "!";

            return output;
        }

        public string senseGreetingQuestion(String utterer /*politeness etc.*/)
        {
            String output = "";
            int productionRule;//todo politeness

            output = vocabDictionary.generateRandom("hibye/greetingQuestion");

            output += "?";

            return output;
        }

        public string senseGreetingQuestionReciprocating(String utterer /*politeness etc.*/)
        {
            String output = "";
            int productionRule;//todo implement the politness or whatever

            output = vocabDictionary.generateRandom("hibye/greetingQuestionReciprocating");

            output += "?";

            return output;
        }

        public string senseGreetingAnswer(String utterer /*politeness etc.*/)
        {
            String output = "";
            int productionRule;//todo politeness

            output = vocabDictionary.generateRandom("hibye/greetingAnswer");

            output += " " + senseThanks(utterer);//todo consider putting a comma or full stop or ! in here

            return output;
        }

        /*FUNCTION DESCRIPTION FAREWELL
         * paramater takes politeness and friendliness and either one or two participants
         */
        //TODO overloaded version that includes name or title of the person being greeted
        public string senseFarewell(String utterer, String otherPerson /*politeness etc.*/)
        {
            String output = "";
            int productionRule;

            /*QUERY DICTIONARY*/
            output = vocabDictionary.generateRandom("hibye/farewell");

            //50% chance of using other persons name
            if (r.NextDouble() > 0.5f)
                output += " " + otherPerson;

            //50% chance of explamation point
            if (r.NextDouble() > 0.5f)
                output += "!";

            return output;
        }

        /*FUNCTION DESCRIPTION RESPONSE
        * paramater takes politeness and friendliness and either one or two participants
        */
        //TODO may want to add "yes YOURE/HE IS right" some time
        //TODO add in no as an answer
        public string senseResponseInformative(String utterer /*politeness etc.*/)
        {
            String output = "";
            int productionRule;

            //50% chance of yes vs yeah
            if (r.NextDouble() > 0.5f)
                output += "yes, ";//businessy
            else
                output += "yeah, ";//friendly


            /*CHOOSE PRODUCTION RULE*/
            productionRule = r.Next(1, 3);

            switch (productionRule)
            {
                case 1:
                    output += "exactly";
                    break;

                case 2:
                    output += "correct";//polite
                    break;

                default:
                    output += "thats right";
                    break;
            }

            //50% chance of explamation point
            if (r.NextDouble() > 0.5f)
                output += "!";

            return output;
        }


        /*FUNCTION DESCRIPTION THANKS
        * paramater takes politeness and friendliness and either one or two participants
        */
        //TODO may want to add "yes YOURE/HE IS right" some time
        //TODO add in no as an answer
        public string senseThanks(String utterer /*politeness etc.*/)
        {
            String output = "";
            int productionRule;

            /*CHOOSE PRODUCTION RULE*/
            productionRule = r.Next(1, 2);

            switch (productionRule)
            {
                //case 1:
                //    output = "thankyou very much";//polite
                //    break;

                case 1:
                    output = "thanks";
                    break;

                default:
                    output = "thank you";//friendly
                    break;
            }

            //50% chance of explamation point
            if (r.NextDouble() > 0.5f)
                output += "!";

            return output;
        }


        /*FUNCTION DESCRIPTION RESPONSE
        * paramater takes politeness and friendliness and either one or two participants
        */
        //TODO add in no as an answer
        //TODO add in plural I/we can help with that
        //TODO add in no as an answer when busy or impolite
        public string senseResponseAssistance(String utterer /*politeness etc.*/)
        {
            String output = "";
            int productionRule;

            //50% chance of yes vs yeah
            if (r.NextDouble() > 0.5f)
                output += "yes, ";//businessy
            else
                output += "yeah, ";//friendly


            /*CHOOSE PRODUCTION RULE*/
            productionRule = r.Next(1, 3);

            switch (productionRule)
            {
                case 1:
                    output = "I can do that";
                    break;

                case 2:
                    output = "of course";
                    break;

                default:
                    output = "naturally";
                    break;
            }

            //50% chance of explamation point
            if (r.NextDouble() > 0.5f)
                output += "!";

            return output;
        }



        /*#########################################################
         *           Now begin the private functions
         *these are mainly partial patterns, for sentence fragments
         *#########################################################*/

        /*FUNCTION DESCRIPRION CLARIFICATION REQUEST
        * Paramaters expects either a type or a specfici object. plus an enum adjective
        * 
        * Asking for confirmation, by stating a defining characteristic
        * 
        * PRODUCTION RULES FOR SPECIFIC OBJECTS:
        * o1)PN   "John Bull?", for people only
        * o2)PN's NP? "John's dog?" for owned objects only
        * o3) {optional: does hesheit have/hesheit has/the one/the guygirl} with + {optional det} + Adj   "the one with red hair?" (for everything else. can also be concatenated to the first two optionally)
        * o4) is hesheit the one on 4th street?
        * 
        * TODO FIND A WAY TO PASS TYPES AS ARGUMENTS
        * FOLLOWING CURRENTLY NOT IMPLEMENTED
        * PRODUCTION RULES FOR TYPES
        * t1) do x have red wings?
        * t2) do they have red wings?
        * t3) are x the ones with red wings?
        * t4) Aux + {they/N} + the ones + Prepositional-adjective? "are they the ones with red wings?"
        * t5) Adj + N? "Red apples?" (if known options, such as red/green apples?
        */
        private String senseClarificationRequest(String utterer, Object subject)
        {
            //TODO if no adjective specified, auto grab one from the subject
            object descriptor = "placeholder";
            return senseClarificationRequest(utterer, subject, descriptor);
        }

        private String senseClarificationRequest(String utterer, Object subject, Object descriptor)
        {
            String aux, np, adj;
            int productionRule;
            String output = "";

            /*CHECK IF WE'RE DEALING WITH A TYPE OR AN OBJECT*/
            if (false)//TODO IF AN OBJECT
            {

                /*CHOOSE PRODUCTION RULE*/
                productionRule = r.Next(1, 4);

                /*CHOOSE ADJECTIVE*/
                //adj = d.adjective.ToString();
            }
            else if (false)//TODO type
            {

            }
            else //error, subject was not an object OR a type
            {
                output = "<type error in clarification>";
            }

            //push answer to question onto the MovesQueue

            return output;
        }

        //small function to extract the class name from the namespace name
        //TODO this function will become obsolete
        private String getType(Object o)
        {
            String s = o.GetType().ToString();
            return s.Substring(s.LastIndexOf('.') + 1).ToLower();
        }
    }
    //}
}
