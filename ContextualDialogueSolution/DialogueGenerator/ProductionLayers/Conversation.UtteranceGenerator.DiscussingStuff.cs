using ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes;
using System;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator
{
    /*QUESTIONS*/
        //how is x
        //do you like x
        //is x good
        //what is  an x


        /*ANSWERS*/
        //it is good
        //i like it
        //yes it was + it is good because
    public partial class Conversation //UtteranceGenerator
    {
        /*SENSE ASK INTERLOCUTOR HIS OPINION ABOUT TOPIC X
         * topic can be either a type or an entity (later events etc. can be introduced)
        * two main production rules
        * 1. is X good (only for entities, not for types)
        * 2. do you like x?
        * 
        * example output:
        * Do you like horses?
        * /TODO/Is the cake good?
        * Do you like your cake?
        * 
        * NOTES: currently unnafected by register or politeness. perhaps turn the 'isn't' into 'is not' in formal register?
		*/
        //the reason for this overload is that the user may choose to override politeness when needed
        //TODO doesnt take into acount tense. would actually be perhaps the easiest place in the whole system to implement tense

        public String sense_discuss_askFeelings(PairParamaters q, ConversationalParamaters conversationalParamaters, Opinion thingToAskAbout)
        {
            String output = "";
            //String declaration = "";//not actually verb, more like 'is good' 'are not good'
            String punctuation = "?";
            //String constituent_one = "";
            String constituent_two = "";
            //String rendered_reason = "";
            //String isAre = "";//auxilliary verb just to distringuish plural and singular

            /*ASSEMBLE*/
            //TODO gendered pronouns he and she, and plurals e.g. they/them
            output = "Do you like " + renderConstituent(thingToAskAbout._subject, thingToAskAbout._subject.GetType() == typeof(TypeDefinition));

            return formatOutput(output) + punctuation;
        }


        /*SENSE DECLARE FEELINGS
        * two main production rules
        * 1 I like x because.
        * 2.X is/are great because.
        * 
        * example output:
        * I like horses
        * Horses are great.
        * I like horses because x
        * I don't like horses because x
        * I like Harry because...
        * 
        * Horses are great because...
        * Yes, horses are great because
        * 
        * Horses are so cuddly. i like them.
        * 
        * NOTES: currently unnafected by register or politeness. perhaps turn the 'isn't' into 'is not' in formal register?
		*/
        //the reason for this overload is that the user may choose to override politeness when needed
        //TODO doesnt take into acount tense. would actually be perhaps the easiest place in the whole system to implement tense
        public String sense_discuss_giveFeelings(PairParamaters q, ConversationalParamaters conversationalParamaters, Opinion speakersOpinion, Opinion interlocutorsOpinion)
        {
            string output = "";
            //TODO put in a proper algorithm here, at least with registers, at best a special agree/disagree function that it can call
            if (speakersOpinion.likes == interlocutorsOpinion.likes)
                output += "yes, ";
            else
                output += "no, ";

            return sense_discuss_giveFeelings(q, conversationalParamaters ,speakersOpinion);
        }
        public String sense_discuss_giveFeelings(PairParamaters q, ConversationalParamaters conversationalParamaters, Opinion speakersOpinion)
        {
            String output =          "";
            String declaration =            "";//not actually verb, more like 'is good' 'are not good'
            String punctuation =    ".";
            String constituent_one = "";
            String constituent_two = "";
            String rendered_reason = "";
            String isAre =           "";//auxilliary verb just to distringuish plural and singular

            if (speakersOpinion._subject.GetType() == typeof(PhysicalEntity))//specific entity -> singular
                isAre = "is";
            else //type == 
                isAre = "are";

            int productionRule = conversationalParamaters.r.Next(1,2);
            if (productionRule == 1)
            {
                constituent_one = "I ";

                if (speakersOpinion.likes)
                    declaration = " like ";
                else
                    declaration = " don't like ";

                constituent_two = renderConstituent(speakersOpinion._subject, false);
            }
            else //if productoin rule == 2
            {
                //if talking about a type rather than a singular, render it in plural
                if (speakersOpinion._subject.GetType() == typeof(PhysicalEntity))
                    constituent_one = renderConstituent(speakersOpinion._subject, false);
                else
                    constituent_one = renderConstituent(speakersOpinion._subject, true);


                if (speakersOpinion.likes)
                    declaration = isAre + " good ";
                else //doesnt like
                    declaration = isAre + "n't good ";
            }

            //will almost always explain why they have an opinion
            if (speakersOpinion.reason != null)
                rendered_reason = " " + renderOpinionReason(speakersOpinion, q.initiatingSpeaker, q.respondingSpeaker);

            /*ASSEMBLE*/
            //reason can either come before or after the declaration. e.g. 'horses are cuddly. i love them' vs 'i love horses. they are cuddly'
            if ( productionRule == 2 )//I like horses because x
                output = constituent_one + declaration + rendered_reason;
            else //production rule == 1 //horses are great because x
                output = constituent_one + declaration + constituent_two + rendered_reason;
           
            return formatOutput(output) + punctuation;
        }

        public String sense_discuss_agreeDisagree(PairParamaters q, Opinion speakersOpinion, Opinion interlocutorsOpinion)
        {
            string output = "";

            //they both like the thing
            if (speakersOpinion.likes == true && interlocutorsOpinion.likes == true)
                output = "me too";
            //they both dislike the thing
            else if (speakersOpinion.likes == false && interlocutorsOpinion.likes == false)
                output = "me neither";
            //they disagree
            else
                output = "Oh, " + "<i need to find some polite way of disagreeing...>";
           
            return formatOutput(output) + ".";
        }

        private String renderOpinionReason(Opinion opinion, PhysicalEntity_Agent speaker, PhysicalEntity_Agent interlocutor)
        {
            return "because " + renderDescriptor(opinion.reason);
        }

    }
}
