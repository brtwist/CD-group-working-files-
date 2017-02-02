using System.Collections.Generic;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs;

namespace ContextualDialogue.DialogueGenerator
{
    partial class Conversation
    {
        public Queue<Topic> topicQueue;
        public Queue<AdjacencyPair> adjacencyPairQueue;
        private AdjacencyPair adjacencyPairRoot;

        /*converts a QUD item into executable moves on the adjacency pair queue*/
        private void generateMoves(/*typeofexchange*/)
        {
            Topic t ;
            //TODO make algorithm
            //currently just takes all user specified items before ever using its own
            //either take item from the user specified qud items or from the stack
            if (conversationalParamaters.hasTopics())
            {
                t = conversationalParamaters.getNextTopic();
                
                //else if (QUD.Count >= 0)
                //{
                //    q = QUD.Dequeue();
                //} 

                switch (t.exchangeType)
                {
                    case Topic.ExchangeTypeEnum.where:
                        generateExchangeWhere(t);
                        break;

                    default://should never get here, should throw error somehow
                        break;
                }
            }
        }

        //pushes some type of greeting to QUD, if appropriate
        private void initQUD()
        {
            PairParamaters PairParamaters = new PairParamaters(conversationalParamaters.participantOne, conversationalParamaters.participantTwo);

            //TODO develop algorithm to decide when and what type of greeting should be pushed

            /*ADD GREETING PHASE*/
            if (conversationalParamaters.greetingMode != ConversationalParamaters.GreetingMode.none)
                adjacencyPairQueue.Enqueue(new AdjacencyPairPrefab_greeting(adjacencyPairRoot, conversationalParamaters, new PairParamaters(conversationalParamaters.participantOne, conversationalParamaters.participantTwo)));

        }

        //pushes farewell to QUD, if appropriate
        private void closeQUD()
        {
            //TODO develop algorithm to decide what type and when to say farewell

            /*ADD FAREWELL PHASE*/
            adjacencyPairQueue.Enqueue(new AdjacencyPairPrefab_farewell(adjacencyPairRoot, conversationalParamaters, new PairParamaters( conversationalParamaters.participantOne, conversationalParamaters.participantTwo)));
        }

        /*ADD qud item - allows items to be manually pushed to qud list*/
        /*e.g. if there is a loud noise in the viscinity, that noise may be pushed into a priority position on queue*/
        private void addToTopicQueue(Topic inItem)
        {
            //TODO allows new item to be pushed onto queue. possibly at different places on queue e.g. top or bottom
            topicQueue.Enqueue(inItem);
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
            Topic q = new Topic(Topic.ExchangeTypeEnum.where);
            q.subject = world.getRandomEntity().ID;
            q.initiatingSpeaker = randomInitiatingSpeaker();
            q.respondingSpeaker = randomRespondingSpeaker(q.initiatingSpeaker);

            //TODO pick random echange type (at the moment there are no exhange types
            //var enumValues = PairParamaters.ExchangeTypeEnum.GetValues(typeof(PairParamaters.ExchangeTypeEnum));
            //q.exchangeType = (PairParamaters.ExchangeTypeEnum)enumValues.GetValue(r.Next(enumValues.Length));

            topicQueue.Enqueue(q);
        }

        /*GENERATING RULES*/
        /*section contains method for each possible exchange type
         * 
         * each method checks all necessary info is available for that exchange type, autogenerates anything thats not
         * then enqueues it as a phase to the movequeue
         */

        private void generateExchangeWhere(Topic topic)
        {
            /*choose random variables where needed*/
            //minimum requirement for whereExchange: two speakers + object + conversationLocation

            //TODO this would break if a respondant was specified but not an initiator
            if (topic.initiatingSpeaker == null)
                topic.initiatingSpeaker = randomInitiatingSpeaker();

            if (topic.respondingSpeaker == null)
                topic.respondingSpeaker = randomRespondingSpeaker(topic.initiatingSpeaker);

            //if no topic specified
            //TODO should it choose a random salient object rather than completely random object?
            if (topic.subject == null)
                topic.subject = world.getRandomEntity();

            /*end choosing random variables*/
            if (topic.subject.GetType() == typeof(int))
                topic.subject = world.findByID((int)topic.subject);

            AdjacencyPairPrefab_discuss_short m = new AdjacencyPairPrefab_discuss_short(adjacencyPairRoot, conversationalParamaters,
                new PairParamaters(topic.initiatingSpeaker, topic.respondingSpeaker), topic.subject);
            adjacencyPairQueue.Enqueue(m);
        }

        /*GENERATING RANDOM VALUES
        * any time a move requres info not specified in a PairParamaters, that info is randomly generated
        * by these methods
        * */

        //TODO extend for more than two participants
        PhysicalEntity_Agent randomInitiatingSpeaker()
        {
            //TODO actual randomizing algorithm
            return conversationalParamaters.participantOne;
        }

        //TODO get randomOTHERparticipant(participantOne) returns a random participant other than the one passed in
         PhysicalEntity_Agent randomRespondingSpeaker(PhysicalEntity_Agent initiatingSpeaker)
        {
            //TODO actual algorithm that return random participant
            return conversationalParamaters.participantTwo;
        }
    }
}
