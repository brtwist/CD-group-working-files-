namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    class AdjacencyPairPrefab_greeting_questionExchange : AdjacencyPair
    {
        public AdjacencyPairPrefab_greeting_questionExchange(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe purpose of paramaters
             * nothing special
             * 
             * 1. initiating part: MOVE how are you
             * 2. responding part: MOVE i'm fine
             */

            /*1. initiating options*/
            //initiation 1: how are you
            Action initiation;
            Action response;

            //TODO determine if it should be a reciprocating question or not

            //if its parent is of type greeting_exchange then make moves. otherwise enqueue two more, one for each person to ask the other how they are
            if (parent is AdjacencyPairPrefab_greeting_questionExchange)
            {
                initiation = new Action(new Conversation.MovesQueueItem("senseGreetingQuestion", new object[2] { q, conversationalParamaters }));//hi
                response = new Action(new Conversation.MovesQueueItem("senseGreetingAnswer", new object[2] { q.cloneAndSwapSpeakers(), conversationalParamaters }));//hi
            }
            else //make one new pair for each person to ask the other how they are
            {
                initiation = new Action(new AdjacencyPairPrefab_greeting_questionExchange(this, conversationalParamaters, q));
                response = new Action(new AdjacencyPairPrefab_greeting_questionExchange(this, conversationalParamaters, q.cloneAndSwapSpeakers()));

            }
            
           

            //now put all the actions in arrays
            Action[] respondingActionArray = { response };
            Action[] initiatingActionArray = { initiation };

            /*
             * END MAKING RESPONDING OPTIONS
             * 
             * PUSH ALL TO BASE 
             *
             */

            //send these presets to the base
            base.init("greeting question",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
