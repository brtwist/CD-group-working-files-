using System;
using ContextualDialogue.DialogueGenerator.LinguisticDictionary;
using ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs;

namespace ContextualDialogue.DialogueGenerator
{
    public partial class Conversation //UtteranceGenerator
    {
       /*REQUEST + REQUEST_RESPONSES*/

        /*SENSE REQUEST ASSISTANCE/HELP
        * minimum input: politeness int between 0 and 10
        * 
        * example output:
        * Help me!
        * Could you help me?
        * Could you help me VERB?
        * Could you help me VERB please?
        * Would it be possible for x to VERB if you dont mind?
		* 
		* TODO would you be so kind as to ... would you be able to ...
        */
		//the reason for this overload is that the user may choose to override politeness when needed
        public String sense_modal_request(PairParamaters q, Verb verb)
        {
            return sense_modal_request(q, verb, q.politeness);
        }
        public String sense_modal_request(PairParamaters q, Verb verb, int politeness)
        {
            String output = "";
            String modal = "";
            String end_fragment = "";
            String start_fragment = "";
            String punctuation = ".";

            String constituent_one = renderConstituent(verb.subject, false);
            String rendered_verb = renderVerb(verb, q.initiatingSpeaker, q.respondingSpeaker);

            //first check input is within bounds
            if (politeness < 1)
                politeness = 1;
            if (politeness > 10)
                politeness = 10;

            switch (politeness)
            {
                case 1:
                    modal = "";//dinner!
                    punctuation = "!";
                    break;
                case 2:
                    modal = "";//Ben, make dinner!
                    punctuation = "!";
                    start_fragment = q.respondingSpeaker.name(q.initiatingSpeaker) + ", ";
                    break;
                case 3:
                    modal = "";//you make dinner
                    break;
                case 4:
                    modal = "can";//Can you make dinner?
                    punctuation = "?";
                    break;
                case 5:
                    modal = "could";//could you make dinner
                    punctuation = "?";
                    break;
                case 6:
                    modal = "could";//could you make dinner, please?
                    end_fragment = "please";
                    punctuation = "?";
                    break;
                case 7:
                    modal = "could";//could you make dinner, please, if you have time?
                    end_fragment = "please, if you have time";
                    punctuation = "?";
                    break;
                case 8:
                    modal = "could";//could you make dinner, please, if you have time - if you don't mind?
                    end_fragment = "please. If you don't mind";
                    punctuation = "?";
                    break;
                case 9:
                    start_fragment = "I was wondering whether";
                    modal = "could";//I was wondering whether you could possibly make dinner, please, if you have time - if you don't mind?
                    end_fragment = "please. If it's okay, only if you wouldn't mind";
                    punctuation = ".";
                    break;
                case 10:
                    start_fragment = "I was wondering whether";
                    modal = "might possibly";//I was wondering whether you might possibly make dinner, please, if you have time - if you don't mind?
                    end_fragment = "please. If it's okay, only if you wouldn't mind";
                    punctuation = ".";
                    break;
                default:
                    modal = "POLITENESS VALUE ERROR: MUST BE BETWEEN 0 AND 10";
                    break;
            }

            /*ASSEMBLE*/
            //two production rules because the order changes slightly with the highest politeness levels
            if (q.politeness <= 8)
                output = start_fragment + " " + modal + " " + constituent_one + " " + rendered_verb + " " + end_fragment;
            else//two highest politenesses have different order
                output = start_fragment + " " + constituent_one + " " + modal + " " + rendered_verb + " " + end_fragment;

            return formatOutput(output) + punctuation;
        }

        /*SENSE RESPOND TO REQUEST - GRANT REQUEST
		 * takes paramater preferred response, designating whether the interlocutor would find this outcome disirable or not
		 * 
		 * Basically just two big politeness switch statements, one for prefferred and one for disprefferred
		 * desirable responses are more emphatic and positive
		 * undesirable responses are more apologetic
		 * 
		 * example output
		 * yes of course
		 * yes of course you/I can
		 * yes of course you/I can VERB
		 */
        //overload allows overriding default politeness
        public String sense_request_grantRequest(PairParamaters q, AdjacencyPair.Preferredness preferredness)
        {
            return sense_request_grantRequest(q, 6, preferredness);//default politeness 6
        }
        public String sense_request_grantRequest(PairParamaters q, int politeness, AdjacencyPair.Preferredness preferredness)
        {
            String output = "";
            String punctuation = ".";

			/*the only effect positive preferredness has is to boost the politeness slightly
			 * my theory is that this will make the response sound more emphatic*/
            if (preferredness == AdjacencyPair.Preferredness.preferred)
                politeness++;
            else if (preferredness == AdjacencyPair.Preferredness.strongly_preferred)
                politeness += 2;


            //first check input is within bounds
            if (politeness < 1)
                politeness = 1;
            if (politeness > 10)
                politeness = 10;

            switch (politeness)
            {
                case 1:
                    output = "yes ";
                    break;
                case 2:
                    output = "yes. ok";
                    break;
                case 3:
                    output = "yes, that's ok";
                    break;
                case 4:
                    output = "yes, no problem";
                    break;
                case 5:
                    output = "yes, of course";
                    break;
                case 6:
                    output = "yes, of course, please do";
                    break;
                case 7:
                    output = "yes, of course, " + q.initiatingSpeaker.name(q.respondingSpeaker) + "no problem, please do";
                    break;
                case 8:
                    output = "yes, of course, " + q.initiatingSpeaker.name(q.respondingSpeaker) + "no problem, please do";
                    punctuation = "!";
                    break;
                case 9:
                    output = "yes, of course! Please do, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", no problem at all";
                    punctuation = ".";
                    break;
                case 10:
                    output = "yes, of course! Please do, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", no problem at all";
                    punctuation = ".";
                    break;
                default:
                    output = "POLITENESS VALUE ERROR: MUST BE BETWEEN 0 AND 10";
                    break;
            }

			//dispreferred outcome
			//it wont happen very often that an accepted request is a dispreferred option
			if (preferredness == AdjacencyPair.Preferredness.dispreferred || preferredness == AdjacencyPair.Preferredness.strongly_dispreferred)
            {
                switch (politeness)
                {
                    case 1:
                        output = "yes";
                        punctuation = "!";
                        break;
                    case 2:
                        output = "yes";
                        break;
                    case 3:
                        output = "yes, sorry";
                        break;
                    case 4:
                        output = "yes, sorry";
                        break;
                    case 5:
                        output = "yes, unfortunately";
                        break;
                    case 6:
                        output = "yes, I'm afraid so";
                        break;
                    case 7:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker);
                        break;
                    case 8:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm very sorry";
                        break;
                    case 9:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm really sorry";
                        punctuation = "!";
                        break;
                    case 10:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm extremely sorry";
                        punctuation = "!";
                        break;
                    default:
                        output = "POLITENESS VALUE ERROR: MUST BE BETWEEN 0 AND 10";
                        break;
                }
            }

            /*ASSEMBLE*/
            //two production rules because the order changes slightly with the highest politeness levels
            output += punctuation;

            return formatOutput(output);

        }//end sense_grant_request

        /*SENSE RESPOND TO REQUEST - DENY REQUEST
		 * takes paramater preferred response, designating whether the interlocutor would find this outcome disirable or not
		 * 
		 * Basically just two big politeness switch statements, one for prefferred and one for disprefferred
		 * desirable responses are more emphatic and positive
		 * undesirable responses are more apologetic
		 * 
		 * example output
		 * no of course not
		 * 
		 * NOTE its expected that preferredness will usually be negative
		 * in fact positive preferredness has only one response: 'no.'
		 */
        //overload allows overriding default politeness
        public String sense_request_denyRequest(PairParamaters q, AdjacencyPair.Preferredness preferredness)
        {
            return sense_request_denyRequest(q, 6, preferredness);//default politeness 6
        }
        public String sense_request_denyRequest(PairParamaters q, int politeness, AdjacencyPair.Preferredness preferredness)
        {
            String output = "";
            String punctuation = ".";


            //first check input is within bounds
            if (politeness < 1)
                politeness = 1;
            if (politeness > 10)
                politeness = 10;


            switch (politeness)
            {
                case 1:
                    output = "no";
                    punctuation = "!";
                    break;
                case 2:
                    output = "no";
                    break;
                case 3:
                    output = "no, sorry";
                    break;
                case 4:
                    output = "no, sorry";
                    break;
                case 5:
                    output = "sorry, no";
                    break;
                case 6:
                    output = "I'm afraid not, no";
                    break;
                case 7:
                    output = "I'm afraid not, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm very sorry";
                    break;
                case 8:
                    output = "I'm sorry, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm afraid not";
                    break;
                case 9:
                    output = "I'm sorry, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm afraid not. I'm very sorry";
                    break;
                case 10:
                    output = "I'm sorry, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm afraid not. I'm truly very sorry";
                    break;
                default:
                    output = "POLITENESS VALUE ERROR: MUST BE BETWEEN 0 AND 10";
                    break;
            }

            //prefferred outcome
            //it wont happen very often that a denied request is a preferred option
            if (preferredness == AdjacencyPair.Preferredness.preferred || preferredness == AdjacencyPair.Preferredness.strongly_preferred)
            {
                    output = "no";
            }

            /*ASSEMBLE*/
            output += punctuation;

            return formatOutput(output);

        }//end sense_grant_request


        /*PERMISSION + PERMISSION_RESPONSES*/

        /*SENSE REQUEST PERMISSION
		* NOTE there is another premission modal which is asking whether something IS allowed, rather than requesting it to be allowed
		* 
        * minimum input: politeness int between 0 and 10
        * 
        * example output:
        * May !?
        * Can I VERB?
        * May I VERB?
        * May I VERB please?
        * Would it be possible for me to VERB if you dont mind?
		* 
		* TODO would you be so kind as to allow me to ... would you be able to ...
        */
        //the reason for this overload is that the user may choose to override politeness when needed
        public String sense_modal_permissionRequest(PairParamaters q, Verb verb)
        {
            return sense_modal_permissionRequest(q, verb, q.politeness);
        }
        public String sense_modal_permissionRequest(PairParamaters q, Verb verb, int politeness)
        {
            String output = "";
            String modal = "";
            String end_fragment = "";
            String start_fragment = "";
            String punctuation = "?";

            String constituent_one = renderConstituent(verb.subject, false);
            String rendered_verb = renderVerb(verb, q.initiatingSpeaker, q.respondingSpeaker);

            //first check input is within bounds
            if (politeness < 1)
                politeness = 1;
            if (politeness > 10)
                politeness = 10;

            switch (politeness)
            {
                case 1:
                    modal = "can ";// can I make dinner?
                    break;
                case 2:
                    modal = "can ";// can I make dinner?
                    break;
                case 3:
                    modal = "can ";// can I make dinner?
                    break;
                case 4:
                    modal = "can ";// can I make dinner?
                    break;
                case 5:
                    modal = "could ";//could I make dinner?
                    break;
                case 6:
                    modal = "may ";//may I make dinner, please?
                    end_fragment = ", please";
                    break;
                case 7:
                    modal = "may ";//may I make dinner, please, if its okay?
                    end_fragment = "please, if its okay";
                    break;
                case 8:
                    modal = "may ";//may i make dinner, please, if its okay - if you don't mind?
                    end_fragment = ", please. If its okay.  If you don't mind";
                    punctuation = "?";
                    break;
                case 9:
                    start_fragment = "I was wondering whether ";
                    modal = "could ";//I was wondering whether you could possibly make dinner, please, if you have time - if you don't mind?
                    end_fragment = "please. If it's okay, only if you wouldn't mind";
                    punctuation = ".";
                    break;
                case 10:
                    start_fragment = "I was wondering whether";
                    modal = "might possibly";//I was wondering whether you might possibly make dinner, please, if you have time - if you don't mind?
                    end_fragment = "please. If it's okay, only if you wouldn't mind of course";
                    punctuation = ".";
                    break;
                default:
                    modal = "POLITENESS VALUE ERROR: MUST BE BETWEEN 0 AND 10";
                    break;
            }

            //chance of putting persons name on the start of the start_fragment
            if (conversationalParamaters.r.Next(4, 10) <= politeness)//gets more likely the higher the politeness is
                start_fragment = q.respondingSpeaker.name(q.initiatingSpeaker) + ", " + start_fragment;

            /*ASSEMBLE*/
            output = start_fragment + " "  + constituent_one + " " + modal + " " + rendered_verb + " " + end_fragment;
            
            return formatOutput(output) + punctuation;
        }

        /*SENSE RESPOND TO REQUEST - GRANT REQUEST
		 * takes paramater preferred response, designating whether the interlocutor would find this outcome disirable or not
		 * 
		 * Basically just two big politeness switch statements, one for prefferred and one for disprefferred
		 * desirable responses are more emphatic and positive
		 * undesirable responses are more apologetic
		 * 
		 * example output
		 * yes of course
		 * yes of course you can
		 * yes of course you may
		 */
        //overload allows overriding default politeness
        public String sense_permission_grantPermission(PairParamaters q, AdjacencyPair.Preferredness preferredness)
        {
            return sense_permission_grantPermission(q, 6, preferredness);//default politeness 6
        }
        public String sense_permission_grantPermission(PairParamaters q, int politeness, AdjacencyPair.Preferredness preferredness)
        {
            String output = "";
            String punctuation = ".";

            String yes = "yes";
            String end_fragment = "of course ";

			/*go through all permutations of preferedness and register*/
			/*changes based on register
			 * formal register yes = "certainly"
			 * casual register yes = "yes"
			 * intimate register yes = "yeah"
			 */

			//NOTE disprefered end_fragments are dealt with later since theyre so unlikely
			 
			 if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.formal_register)
            {
                yes = "certainly ";
                end_fragment = "go ahead";
            }
            else if (conversationalParamaters.registerType == ConversationalParamaters.RegisterType.intimate_register)
            {
                yes = "yeah ";
                end_fragment = "no worries";
            }

			 /*end going through preferredness and register*/


            //first check input is within bounds
            if (politeness < 1)
                politeness = 1;
            if (politeness > 10)
                politeness = 10;

            switch (politeness)
            {
                case 1:
                    output = yes;
                    end_fragment = "";
                    break;
                case 2:
                    output = yes;
                    end_fragment = "";
                    break;
                case 3:
                    output = yes + ", that's ok";
                    end_fragment = "";
                    break;
                case 4:
                    output = yes + "," + end_fragment;
                    break;
                case 5:
                    output = yes + "," + end_fragment;
                    break;
                case 6:
                    output = yes + "," + end_fragment;
                    break;
                case 7:
                    output = yes + ", " + end_fragment + q.initiatingSpeaker.name(q.respondingSpeaker) + "no problem, please do";
                    break;
                case 8:
                    output = yes + ", " + end_fragment + q.initiatingSpeaker.name(q.respondingSpeaker) + "no problem, please do";
                    break;
                case 9:
                    output = yes + ", " + end_fragment + q.initiatingSpeaker.name(q.respondingSpeaker) + ", no problem at all";
                    break;
                case 10:
                    output = yes + ", " + end_fragment + q.initiatingSpeaker.name(q.respondingSpeaker) + ", no problem at all";
                    break;
                default:
                    output = "POLITENESS VALUE ERROR: MUST BE BETWEEN 0 AND 10";
                    break;
            }

            //dispreferred outcome
            //it wont happen very often that an accepted request is a dispreferred option
            if (preferredness == AdjacencyPair.Preferredness.dispreferred || preferredness == AdjacencyPair.Preferredness.strongly_dispreferred)
            {
                switch (politeness)
                {
                    case 1:
                        output = "yes";
                        punctuation = "!";
                        break;
                    case 2:
                        output = "yes";
                        break;
                    case 3:
                        output = "yes, sorry";
                        break;
                    case 4:
                        output = "yes, sorry";
                        break;
                    case 5:
                        output = "yes, unfortunately";
                        break;
                    case 6:
                        output = "yes, I'm afraid so";
                        break;
                    case 7:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker);
                        break;
                    case 8:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm very sorry";
                        break;
                    case 9:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm really sorry";
                        punctuation = "!";
                        break;
                    case 10:
                        output = "yes, I'm afraid so, " + q.initiatingSpeaker.name(q.respondingSpeaker) + ", I'm extremely sorry";
                        punctuation = "!";
                        break;
                    default:
                        output = "POLITENESS VALUE ERROR: MUST BE BETWEEN 0 AND 10";
                        break;
                }
            }

            /*ASSEMBLE*/
	
            return formatOutput(output) + punctuation;

        }//end sense_grant_permission


        /*SENSE RESPOND TO permission request - DENY PERMISSION
		 * takes paramater preferred response, designating whether the interlocutor would find this outcome disirable or not
		 * 
		 * Basically just two big politeness switch statements, one for prefferred and one for disprefferred
		 * desirable responses are more emphatic and positive
		 * undesirable responses are more apologetic
		 * 
		 * example output
		 * no of course not
		 * 
		 * NOTE its expected that preferredness will usually be negative
		 * in fact positive preferredness has only one response: 'no.'
		 */
        //overload allows overriding default politeness
        public String sense_request_denyPermission(PairParamaters q, AdjacencyPair.Preferredness preferredness)
        {
            return sense_permission_denyPermission(q, 6, preferredness);//default politeness 6
        }
        public String sense_permission_denyPermission(PairParamaters q, int politeness, AdjacencyPair.Preferredness preferredness)
        {
            return sense_request_denyRequest(q, politeness, preferredness);
        }//end sense_grant_request

    }
}
