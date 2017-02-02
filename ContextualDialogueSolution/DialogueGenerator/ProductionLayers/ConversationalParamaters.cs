using System;
using System.Collections.Generic;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using ContextualDialogue.WorldManager;

namespace ContextualDialogue.DialogueGenerator
{
    public class ConversationalParamaters
    {
        public Random r;
        //ArrayList participantsArray;
        public PhysicalEntity_Agent participantOne;
        public PhysicalEntity_Agent participantTwo;
        public World world;

        public PhysicalEntity conversationLocation;

        //this one is used to define the type of convo
        public enum ConversationType { helloOnly, businessShortExchange } //SocialCatchUp, SocialGettingToKnowYou

        //used to define what register to talk in
        public enum RegisterType { casual_register, formal_register, intimate_register }
        public RegisterType registerType;

        //this one is used to define the style that the convo type is rendered? e.g. long-winded, to-the-point
        //this one is only used in choosing branches in adjacency pairs
        public enum DiscourseType { none, @default, direct, indirect /*passive, active, business, friendly, colloqual*/}
        public DiscourseType discourseType;


        //QUD item that will be discussed (inserted into body by default) 
        private Queue<Topic> userSpecifiedTopicsList;

        //mode: open-ended (keeps going till told to close) close-ended (parses whatever is initially pushed without extending) goal-orientated (keeps going till goal is reached)
        public Boolean autoGeneratePairParamaterss;

        public enum GreetingMode { none, twoTurn, fourTurn }
        public GreetingMode greetingMode;//none, 2stroke, 4stroke

        //int smallTalkMode; //none, smalltalk, catchup, gettingToKnowYou

        public enum FarewellMode { none, simple }
        public FarewellMode farewellMode;//none, 2stroke, 4stroke

        //public enum PolitenessMode { friendly, normal, polite }


        //constructor chooses default settings
        public ConversationalParamaters(ConversationType cType, PhysicalEntity_Agent participantOne, PhysicalEntity_Agent participantTwo, World world)
        {
            r = new Random();
            userSpecifiedTopicsList = new Queue<Topic>();
            //participantsArray = new ArrayList(2);//default only two participants
            this.participantOne = participantOne;
            this.participantTwo = participantTwo;
            this.world = world;

            greetingMode = GreetingMode.twoTurn;
            //smallTalkMode = 0;
            farewellMode = FarewellMode.simple;
            autoGeneratePairParamaterss = false;//off by default

            //TODO use this somehow
            discourseType = DiscourseType.@default;

            //TODO choose register in a more inteligent way
            registerType = RegisterType.casual_register;

            //contains one case for every type of conversation
            switch (cType)
            {
                case ConversationType.helloOnly:
                    setDefaultsForHelloOnly();
                    break;

                case ConversationType.businessShortExchange:
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
            autoGeneratePairParamaterss = false;
            //since no conversation was really had, no goodbye is necessary
            farewellMode = FarewellMode.none;

            //1/3 chance of giving a 4turn hello instead of default 2turn
            if (r.NextDouble() < 0.33)
                greetingMode = GreetingMode.fourTurn;
        }

        private void setDefaultsForBusinessShortExchange()
        {
            //intended to simulate ver short exchanges such as asking a question at the reception 
            //desk of a hotel. no 'how are you's' just short and to the point
            autoGeneratePairParamaterss = false;

            greetingMode = GreetingMode.twoTurn;
            farewellMode = FarewellMode.simple;


        }

        //sets default/random values for anything the user doesnt specify
        private void autoFillUnspecifiedParamaters()
        {
            //if no location was specified, simply guess that the conversation is probably taking place where the participants are
            //assumption only holds true if the user has been keeping the knowledge database up to date
            if (conversationLocation == null)
                conversationLocation = participantOne.getSpatialparent().adult;
        }

        public void addTopic(Topic t) { userSpecifiedTopicsList.Enqueue(t); }

        public Topic  getNextTopic()
        {
            //if (userSpecifiedPairParamatersList.Count > 0)
            //{
            return userSpecifiedTopicsList.Dequeue();
            //}
            //TODO breaks if queue is empty
        }

        public Topic peekNextTopic()
        {
            return userSpecifiedTopicsList.Peek();
        }

        public bool hasTopics()
        {
            return userSpecifiedTopicsList.Count > 0;
        }
    }


}
