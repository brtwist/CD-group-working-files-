using System;
using System.Collections.Generic;
using System.Text;
using ContextualDialogue.DialogueGenerator;
using System.Collections;

namespace ContextualDialogue.DialogueGenerator
{
    public class ConversationalParamaters
    {
        private Random r;
        //ArrayList participantsArray;
        public String participantOne;
        public String participantTwo;

        //tone/type e.g. heloOnly, friendly, business, restaraunt
        public enum conversationType { helloOnly, businessShortExchange } //SocialCatchUp, SocialGettingToKnowYou

        //QUD item that will be discussed (inserted into body by default) 
        public QUDitem userSpecifiedQUDitem;

        //mode: open-ended (keeps going till told to close) close-ended (parses whatever is initially pushed without extending) goal-orientated (keeps going till goal is reached)
        public Boolean autoGenerateQUDitems;

        public enum GreetingMode { none, twoTurn, fourTurn }
        public GreetingMode greetingMode;//none, 2stroke, 4stroke

        //int smallTalkMode; //none, smalltalk, catchup, gettingToKnowYou

        public enum FarewellMode { none, simple }
        public FarewellMode farewellMode;//none, 2stroke, 4stroke

        //public enum PolitenessMode { friendly, normal, polite }
        

        //constructor chooses default settings
        public ConversationalParamaters(conversationType cType, String pOne, String pTwo)
        {
            r = new Random();
            //participantsArray = new ArrayList(2);//default only two participants
            participantOne = pOne;
            participantTwo = pTwo;

            greetingMode = GreetingMode.twoTurn;
            //smallTalkMode = 0;
            farewellMode = FarewellMode.simple;
            autoGenerateQUDitems = false;//off by default


            //contains one case for every type of conversation
            switch (cType)
                {
                case conversationType.helloOnly:
                    setDefaultsForHelloOnly();
                    break;

                case conversationType.businessShortExchange:
                    setDefaultsForBusinessShortExchange();
                    break;

                default:
                    break;
                }
            //autoFillUnspecifiedParamaters();
        }

        //functions for initialising default settings
        private void setDefaultsForHelloOnly()
        {
            //intended to simulate passing greeting where neither speaker really stops walking
            autoGenerateQUDitems = false;
            //since no conversation was really had, no goodbye is necessary
            farewellMode = FarewellMode.simple;//TODO change this to none one day

            //1/3 chance of giving a 4turn hello instead of default 2turn
            if (r.NextDouble() < 0.33)
                greetingMode = GreetingMode.fourTurn;
        }

        private void setDefaultsForBusinessShortExchange()
        {
            //intended to simulate ver short exchanges such as asking a question at the reception 
            //desk of a hotel. no 'how are you's' just short and to the point
            autoGenerateQUDitems = false;
            
            greetingMode = GreetingMode.twoTurn;
            farewellMode = FarewellMode.simple;

            
        }

        //sets default/random values for anything the user doesnt specify
        private void autoFillUnspecifiedParamaters()
        {

        }
    }

    
}
