using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using ContextualDialogue.DialogueGenerator.LinguisticDictionary;
using EnumNamespace;
using System;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes;

namespace ContextualDialogue.DialogueGenerator
{
    //namespace ContextualDialogue.DialogueGenerator.UtteranceGenerator
    //{
    public partial class Conversation //UtteranceGenerator
    {
        /*helper function renders verb object
         * paramaters: a verb object to render. A physicalEntity which is the speaker
         * knowing who the speaker is is important, we check the speaker against the subject and objects of the verb
         * for conjugation and pronouns
         * 
         * this function does three things
         * 1. first render the base verb
         * 2. then check if there is an object, if there is, render that too
         * 3. then check if there is a second object, if there is render it as well
         * each object can be either an entity or a type of entity. 
         */
         private String renderVerb(Verb v, PhysicalEntity_Agent utterer, PhysicalEntity_Agent interlocutor)
        {
            String output = "";

            /*first - base verb*/
            output += v.adverb + " ";//TODO in some sentences the adverb should come after the verb. He quickly sits down. He sits down quickly

            if (v.subject != utterer)
                output += v._verbThirdPersonSingular + " ";
            else//subject is same as speaker
                output += v._verbInfinitive + " ";

            output += v.intransitive_preposition + " ";

            if (v.object_one != null)
            {
                /*check for any pronoun possibilities
                 * first check for you/me/he/she
                 * then check for reflexives
                 * then check for ownership
                 * 
                 * TODO any kind of plural
                 */

                //first check if its you or me, but not reflexive
                if (v.object_one == utterer && v.subject != utterer)
                    output += "me ";

                else if (v.object_one == interlocutor && v.subject != interlocutor)
                    output += "you ";
                
                //second check for reflexives
                else if (v.object_one == v.subject)
                {
                    /*
                    * if the subject is the same as the object it needs to be reflexive
                    * there are 3 possibilities for singular
                    * 1 the subject and object are me
                    * 2 the subject and object are you
                    * 3 the subject and object are sombody else
                    * 
                    * TODO plurals
                    */

                    //possibility 1
                    if (v.subject == utterer)
                        output += "myself ";

                    //possibility 2
                    else if (v.subject == interlocutor)
                    {
                        //check if it has gender or not
                        output += "yourself ";
                    }

                    //possiblity 3
                    else//we already know theyre the same object so no need to check again
                    {
                        //if its a typedefinition, THEMSELVES
                        if (v.object_one.GetType() == typeof(TypeDefinition))
                            output += "themselves ";

                        //else its a PhysicalEntity, get it to tell its own reflexive pronoun
                        else if (v.object_one.GetType() == typeof(PhysicalEntity))
                            output += ((PhysicalEntity)v.object_one).reflexivePronoun + " ";
                        else
                        {
                            //TODO plurals OURSELVES THEMSELVES
                        }
                    }
                }//end doing pronouns, just render it as a constituent as normal
                else
                    output += v.object_one_preposition + renderConstituent(v.object_one, false) + " ";//TODO only supports singular objects

                output += v.object_one_adverb + " ";

                //repeat process for object 2
                //TODO copy above code and just tweak it to apply to object two instead


                //if (v.object_two != null)
            }

            return output;
        }

        /*helper function renders noun constituent
         * this function does two things
         * first checks what for the noun should be, e.g. plural uncountable
         * second checks what type of determiner should have, e.g. a/an/some/any
         */
        private String renderConstituent(object o, bool plural)
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

        //render a descriptor (currently used for rendering the reason for an opinion
        //may need to keep in mind that descriptors perform two almost entirely different functions whether they be for traits or abilities
        //note it probably only renders traits at the moment, to render abilities it might need to be reworked as well as used differently
        private String renderDescriptor(Descriptor descriptor)
        {
            String output = "";
            //probability should be something like always, or usually
            //trait should be something like 'fluffy'. e.g. baby cats are fluffy.
            //value is whether its true or false, e.g. baby snakes are NOT fluffy.

            output += "they are/it is ";

            //give some hint about probability //0.0 means no probbaility was specified, because double doesnt have null
            if (descriptor.probability != 0.0)
            {
                if (descriptor.probability <= 0.5)
                    output += " sometimes ";
                else if (descriptor.probability <= 0.75)
                    output += " usually ";
                else if (descriptor.probability < 1.0)
                    output += " nearly always ";
            }

            if (!descriptor.value)
                output += "not ";

            output += descriptor.trait;

            return output;
        }


        /*helper function should be called at end of every sense to format the output*/
        private String formatOutput(String output)
        {
            //remove duplicate whitespace
            output = System.Text.RegularExpressions.Regex.Replace(output, @" {2,}", " ");

            //capitalise first letter
            output = char.ToUpper(output[0]) + output.Substring(1);

            return output.Trim();
        }

        /*### END HELPER FUNCTIONS ###*/

        /*##### SENSE FUNCTIONS #####*/

        /*BEGIN GREETING AND FAREWELL SENSES*/

        //Hi!
        public string senseGreeting(PairParamaters q, ConversationalParamaters conversationalParamaters)
        {
            String output = "";

            if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
            {
                output = vocabDictionary.generateRandom("hibye/greeting/formal");

                //% chance of using the interlocutors name
                if (conversationalParamaters.r.Next(3, 7) <= q.politeness)//greater the politeness the greater the chance of using their name/honorific
                    output += " ," + senseThanks(q, conversationalParamaters);
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.casual_register)
            {
                output = vocabDictionary.generateRandom("hibye/greeting/casual");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
            {
                output = vocabDictionary.generateRandom("hibye/greeting/intimate");
            }

            //50% chance of using other persons name/title
            if (r.NextDouble() > 0.5f)
                output += " " + q.respondingSpeaker.name(q.initiatingSpeaker);

            //50% chance of explamation point. if its not formal
            if (r.NextDouble() > 0.5f && conversationalParamaters.registerType != ConversationalParamaters.RegisterType.formal_register)
                output += "!";

            return output;
        }


    /*greeting question. e.g. how are you?*/
    public string senseGreetingQuestion(PairParamaters q, ConversationalParamaters conversationalParamaters)
        {
            String output = "";

            if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingQuestion/formal");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.casual_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingQuestion/casual");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingQuestion/intimate");
            }

            return output + "?";
        }

        //and how are you then?
        public string senseGreetingQuestionReciprocating(PairParamaters q, ConversationalParamaters conversationalParamaters)
        {
            String output = "";

            if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingQuestionReciprocating/formal");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.casual_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingQuestionReciprocating/casual");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingQuestionReciprocating/intimate");
            }

            return output + "?";
        }

        //i'm well thanks
        public string senseGreetingAnswer(PairParamaters q, ConversationalParamaters conversationalParamaters)
        {
            String output = "";

            if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingAnswer/formal");

                //% chance of using the interlocutors name
                if (conversationalParamaters.r.Next(3,7) <= q.politeness)//greater the politeness the greater the chance of using their name/honorific
                    output += " ," + senseThanks(q, conversationalParamaters);
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.casual_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingAnswer/casual");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
            {
                output = vocabDictionary.generateRandom("hibye/greetingAnswer/intimate");
            }

            return output;
        }

        //Goodbye Mr. Mawson!
        public string senseFarewell(PairParamaters q, ConversationalParamaters conversationalParamaters)
        {
            String output = "";

            if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
            {
                output = vocabDictionary.generateRandom("hibye/farewell/formal");

                //% chance of using the interlocutors name
                //if (conversationalParamaters.r.Next(3, 7) <= q.politeness)//greater the politeness the greater the chance of using their name/honorific
                //    output += " ," + senseThanks(q, conversationalParamaters);
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.casual_register)
            {
                output = vocabDictionary.generateRandom("hibye/farewell/casual");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
            {
                output = vocabDictionary.generateRandom("hibye/farewell/intimate");
            }

            //50% chance of using other persons name/title
            if (r.NextDouble() > 0.5f)
                output += " " + q.respondingSpeaker.name(q.initiatingSpeaker);

            //50% chance of explamation point
            if (r.NextDouble() > 0.5f &&
                (conversationalParamaters.registerType != ConversationalParamaters.RegisterType.formal_register))
                output += "!";

            return formatOutput( output);
        }

        /*give compliment sense
         * could be complimenting:
         *  appearance of the interlocutor (entity to compliment is the interlocutor)
         *  ability of the interlocutor (something madeby/done by interlocutor)
         *  thing owned or made or chosen by the interlocutor (something owned by interlocutor)
         * 
         * e.g. the cake is so good! (which you made)
         * e.g. you make great cakes! you make cakes well
         * e.g. your dog is so good!
         * e.g. you look good today!
         * 
         * TODO: I like your scarf
         */
         public string sense_compliment_give(PairParamaters q, ConversationalParamaters conversationalParamaters, PhysicalEntity entityToCompliment)
        {

            String output = "";
            String libraryDirectory = "compliments";

            //check whether we're complimenting a thing or a person
            if (entityToCompliment == q.respondingSpeaker)
            {
                libraryDirectory += "/person";
                //TODO check if its a man and woman etc.
                //TODO check if they're in love
                output = "you look good today";
            }
            else
            {
                //TODO check what the relationship is. e.g. does the interlocutor own it or did he/she make it
                //output = the/your thingToCompliment.noun is good
                //output = you thingToCompliment.verbOrigin really well (you bake cakes really well)
                output = "the " + entityToCompliment.getRandomCommonNoun() + "is really good";
            }
            /*
            if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
            {
                output = vocabDictionary.generateRandom(libraryDirectory + "/formal");

                //% chance of using the interlocutors name
                if (conversationalParamaters.r.Next(3, 7) <= q.politeness)//greater the politeness the greater the chance of using their name/honorific
                    output += " ," + senseThanks(q.respondingSpeaker.name(q.initiatingSpeaker));
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.casual_register)
            {
                output = vocabDictionary.generateRandom(libraryDirectory + "/casual");
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
            {
                output = vocabDictionary.generateRandom(libraryDirectory + "/intimate");
            }

            //50% chance of using other persons name/title
            if (r.NextDouble() > 0.5f)
                output += " " + q.respondingSpeaker.name(q.initiatingSpeaker);

            //50% chance of explamation point
            if (r.NextDouble() > 0.5f)
                output += "!";
*/
            return output;

        }

        /*FUNCTION DESCRIPTION YES*/
        public string sense_response_yesNo(PairParamaters q, ConversationalParamaters conversationalParamaters, Boolean condition)
        {
            
            String output = "";

            if (condition)
            {

                if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
                    output = "yeah";
                else
                    output = "yes";
            }
            else
            {

                if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
                    output = "nah";
                else
                    output = "no";
            }

            return formatOutput( output);
        }


        /*FUNCTION DESCRIPTION THANKS
        * simply says thanks based on which register theyre talking in
        */
        public string senseThanks(PairParamaters q, ConversationalParamaters conversationalParamaters)
        {
            String output = "";

            if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
                output = "thankyou";
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
                output = "cheers!";
            else //casual
                output = "thanks";

            //80% chance of adding name/title
            //if (r.NextDouble() > 0.8f)
            //    output += q.respondingSpeaker.name(q.initiatingSpeaker)

            //80% chance of explamation point
            if (r.NextDouble() > 0.8f)
                output += "!";

            return formatOutput(output);
        }


        /*######################################################
         *######### Up till here is pretty final code ##########
         *######################################################*/

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
