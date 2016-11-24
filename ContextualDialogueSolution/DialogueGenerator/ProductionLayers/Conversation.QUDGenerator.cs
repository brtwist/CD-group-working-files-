using System;
using System.Collections.Generic;

namespace ContextualDialogue.DialogueGenerator
{
    partial class Conversation
    {
        public Queue<QUDitem> QUD;

        /*converts a QUD item into executable moves on the movie queue*/
        private void generateMoves(/*typeofexchange*/)
        {
            QUDitem q;
            //TODO make algorithm
            //currently just takes all user specified items before ever using its own
            //either take item from the user specified qud items or from the stack
            if (conversationalParamaters.hasQUDitem())
            {
                q = conversationalParamaters.getNextQUDitem();

                //else if (QUD.Count >= 0)
                //{
                //    q = QUD.Dequeue();
                //} 

                switch (q.exchangeType)
                {
                    case QUDitem.ExchangeTypeEnum.where:
                        generateExchangeWhere(q);
                        break;

                    default://should never get here, should throw error somehow
                        break;
                }
            }
        }

        //pushes some type of greeting to QUD, if appropriate
        private void initQUD()
        {
            QUDitem quditem = new QUDitem();
            quditem.initiatingSpeaker = conversationalParamaters.participantOne;
            quditem.respondingSpeaker = conversationalParamaters.participantTwo;

            //TODO develop algorithm to decide when and what type of greeting should be pushed


            /*ADD GREETING PHASE*/
            MovesQueueItem m = new MovesQueueItem();
            m.methodToCall = "parseGreetingPhase";
            m.paramaters = new object[1] { quditem };//dummy paramater to keep .invoke happy
            MovesQueue.Enqueue(m);

            if (conversationalParamaters.greetingMode == ConversationalParamaters.GreetingMode.fourTurn)
            {
                m = new MovesQueueItem();
                m.methodToCall = "parseGreetingPhaseQuestions";
                m.paramaters = new object[1] { quditem };//dummy paramater to keep .invoke happy
                MovesQueue.Enqueue(m);
            }
        }

        //pushes farewell to QUD, if appropriate
        private void closeQUD()
        {
            QUDitem quditem = new QUDitem();
            quditem.initiatingSpeaker = conversationalParamaters.participantOne;
            quditem.respondingSpeaker = conversationalParamaters.participantTwo;

            //TODO develop algorithm to decide what type and when to say farewell

            /*ADD FAREWELL PHASE*/
            MovesQueueItem m = new MovesQueueItem();
            m.methodToCall = "parseFarewellPhase";
            m.paramaters = new object[1] { quditem };//dummy paramater to keep .invoke happy
            MovesQueue.Enqueue(m);
        }

        /*ADD qud item - allows items to be manually pushed to qud list*/
        /*e.g. if there is a loud noise in the viscinity, that noise may be pushed into a priority position on queue*/
        private void addToQUD(QUDitem inItem)
        {
            //TODO allows new item to be pushed onto queue. possibly at different places on queue e.g. top or bottom
            QUD.Enqueue(inItem);
        }

        //TODO add a boolean that toggles different behaviour. maybe mode one, only seeds when explicitly requested. mode two auto seeds whenever qud gets empty
        //TODO method which magicaly seeds new topics of conversation. that function can get progressively smarter
        private void autoMagicallySeedQUD()
        {
            //check for salient topics. e.g. something close proximity, mutual interests or mutual friends
            //check for recently discussed things and use ontologies to generate new topic
            //check list of world-salient things. e.g. a kind of bulletin board that the game designer could post news items to
            //make sure that whatever is chosen hasnt already been said, i.e. keep a history of things pushed to moves

            //rank by saliency

            //generate random one. this is only a test case. algorithm needs to be developed
            QUDitem q = new QUDitem(QUDitem.ExchangeTypeEnum.where);
            q.subject = world.getRandomEntity().ID;
            q.initiatingSpeaker = randomInitiatingSpeaker();
            q.respondingSpeaker = randomRespondingSpeaker(q.initiatingSpeaker);

            //TODO pick random echange type (at the moment there are no exhange types
            //var enumValues = QUDitem.ExchangeTypeEnum.GetValues(typeof(QUDitem.ExchangeTypeEnum));
            //q.exchangeType = (QUDitem.ExchangeTypeEnum)enumValues.GetValue(r.Next(enumValues.Length));

            QUD.Enqueue(q);
        }

        /*GENERATING RULES*/
        /*section contains method for each possible exchange type
         * 
         * each method checks all necessary info is available for that exchange type, autogenerates anything thats not
         * then enqueues it as a phase to the movequeue
         */

        private void generateExchangeWhere(QUDitem quditem)
        {
            /*choose random variables where needed*/
            //minimum requirement for whereExchange: two speakers + object + conversationLocation

            //TODO this would break if a respondant was specified but not an initiator
            if (quditem.initiatingSpeaker == null)
                quditem.initiatingSpeaker = randomInitiatingSpeaker();

            if (quditem.respondingSpeaker == null)
                quditem.respondingSpeaker = randomRespondingSpeaker(quditem.initiatingSpeaker);

            //TODO should it choose a random salient object rather than completely random object?
            if (quditem.subject == null)
                quditem.subject = world.getRandomEntity();

            /*end choosing random variables*/
            if (quditem.subject.GetType() == typeof(int))
                quditem.subject = world.findByID((int)quditem.subject);

            MovesQueueItem m = new MovesQueueItem();
            m.methodToCall = "parseExchangeWhere";
            m.paramaters = new object[1] { quditem };
            MovesQueue.Enqueue(m);
        }

        //private void generateRestarauntTakeOrder(QUDitem q)
        //{

        //}


        /*GENERATING RANDOM VALUES
        * any time a move requres info not specified in a QUDitem, that info is randomly generated
        * by these methods
        * */

        //TODO extend for more than two participants
        String randomInitiatingSpeaker()
        {
            //TODO actual randomizing algorithm
            return conversationalParamaters.participantOne;
        }

        //TODO get randomOTHERparticipant(participantOne) returns a random participant other than the one passed in
        String randomRespondingSpeaker(String initiatingSpeaker)
        {
            //TODO actual algorithm that return random participant
            return conversationalParamaters.participantTwo;
        }
    }
}
