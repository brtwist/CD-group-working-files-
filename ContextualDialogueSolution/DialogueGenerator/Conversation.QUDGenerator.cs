using System;
using System.Collections.Generic;
using System.Text;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator
{
    partial class Conversation
    {
        
        //struct to hold QUD items
        public struct QUDitem
        {
            public String initiatingSpeaker;
            public String respondingSpeaker;
            public int objectID;
            //verb
            public ExchangeTypeEnum exchangeType;
            //salience
            //lengthiness of exchange on this topic
        }

        public enum ExchangeTypeEnum { QuestionWhere }//, SocialCatchUp, SocialGettingToKnowYou, MiscGiveCompliment }//do you like. how far
        public Queue<QUDitem> QUD;


        /*converts a QUD item into executable moves on the movie queue*/
        private void generateMoves(/*typeofexchange*/)
        {
            if (QUD.Count >= 0)
            {
                QUDitem q = QUD.Dequeue();
                //TODO make algorithm

            switch (q.exchangeType)
                {
                    case ExchangeTypeEnum.QuestionWhere:
                       generateQuestionWhere(q);
                        break;

                    default://should never get here, should throw error somehow
                        break;
                }
            }
        }

        //pushes appropriate greeting to QUD, if appropriate
        private void initQUD()
        {
            //TODO develop algorithm to decide when and what type of greeting should be pushed
             

            /*ADD GREETING PHASE*/
            MovesQueueItem m = new MovesQueueItem();
            m.methodToCall = "parseGreetingPhase";
            m.paramaters = new object[1] { -1 };//dummy paramater to keep .invoke happy
            MovesQueue.Enqueue(m);
        }

        //pushes farewell to QUD, if appropriate
        private void closeQUD()
        {
            //TODO develop algorithm to decide what type and when to say farewell

            /*ADD FAREWELL PHASE*/
            MovesQueueItem m = new MovesQueueItem();
            m.methodToCall = "parseFarewellPhase";
            m.paramaters = new object[1] { -1 };//dummy paramater to keep .invoke happy
            MovesQueue.Enqueue(m);
        }

        private void pushToQUD(QUDitem inItem)
        {
            //TODO allows new item to be pushed onto queue. possibly at different places on queue e.g. top or bottom
            QUD.Enqueue(inItem);
        }
        
        //TODO add a boolean that toggles different behaviour. maybe mode one, only seeds when explicitly requested. mode two auto seeds whenever qud gets empty
        //TODO method which magicaly seeds new topics of conversation. that function can get progressively smarter
        private void seedQUD()
        {
            //check for salient topics. e.g. something close proximity, mutual interests or mutual friends
            //check for recently discussed things and use ontologies to generate new topic
            //check list of world-salient things. e.g. a kind of bulletin board that the game designer could post news items to
            //make sure that whatever is chosen hasnt already been said, i.e. keep a history of things pushed to moves

            //rank by saliency

            //generate random one. this is only a test case. algorithm needs to be developed
            QUDitem q = new QUDitem();
            q.objectID = world.getRandomEntity().ID;

            var enumValues = ExchangeTypeEnum.GetValues(typeof(ExchangeTypeEnum));
            //var random = new Random();
            q.exchangeType = (ExchangeTypeEnum)enumValues.GetValue(r.Next(enumValues.Length));

            QUD.Enqueue(q);
        }

        /*GENERATING RULES*/
        /*section contains method for each possible exchange type
         * 
         * each method checks all necessary info is available for that exchange type, autogenerates anything thats not
         * then enqueues it as a phase to the movequeue
         */

        private void generateQuestionWhere(QUDitem q)
        {
            /*choose random variables where needed*/
            //minimum requirement for whereExchange: two speakers + object + conversationLocation

            //TODO this would break if a respondant was specified but not an initiator
            if (q.initiatingSpeaker == null)
                randomInitiatingSpeaker();

            if (q.respondingSpeaker == null)
                randomRespondingSpeaker(q.initiatingSpeaker);

            //TODO should it choose a random salient object rather than completely random object?
            if (q.objectID == 0)
                q.objectID = world.getRandomEntity().ID;

            /*end choosing random variables*/

             PhysicalEntity subjectOfEnquiry = world.findByID(q.objectID);

            MovesQueueItem m = new MovesQueueItem();
            m.methodToCall = "phaseExchangeWhere";
            m.paramaters = new object[3] { q.initiatingSpeaker, q.respondingSpeaker, subjectOfEnquiry };
            MovesQueue.Enqueue(m);
        }

        
         /*GENERATING RANDOM VALUES
         * any time a move requres info not specified in a QUDitem, that info is randomly generated
         * by these methods
         * */
         
            //TODO extend for more than two participants
        String randomInitiatingSpeaker()
        {
            //TODO actual randomizing algorithm
            return participantOne;
        }

        //TODO get randomOTHERparticipant(participantOne) returns a random participant other than the one passed in
        String randomRespondingSpeaker(String initiatingSpeaker)
        {
            //TODO actual algorithm that return random participant
            return participantTwo;
        }
    }
}
