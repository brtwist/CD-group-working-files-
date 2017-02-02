//using ContextualDialogue.DialogueGenerator.LinguisticDictionary;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    class AdjacencyPairPrefab_farewell : AdjacencyPair
    {
        public AdjacencyPairPrefab_farewell(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe purpose of paramaters
             * if farewell mode simple
             * 1. initiating part: MOVE(bye)
             * 2. response: MOVE(bye)
             * 
             * TODO other farewell modes. e.g. none. thanks. 
             */

            /*1. initiating options*/
            //initiation 1: bye
            Action initiation;
            Action response;

 
                initiation = new Action(new Conversation.MovesQueueItem("senseFarewell", new object[2] { q, conversationalParamaters }));

                response = new Action(new Conversation.MovesQueueItem("senseFarewell", new object[2] { q.cloneAndSwapSpeakers(), conversationalParamaters }));

            //now put all the parts into arrays
            Action[] initiatingActionArray = { initiation };
            Action[] respondingActionArray = { response };

            /*
             * END MAKING OPTIONS
             * 
             * PUSH ALL TO BASE 
             *
             */

            //send these presets to the base
            base.init("farewell",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
