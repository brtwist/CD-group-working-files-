using System;
//using ContextualDialogue.DialogueGenerator.LinguisticDictionary;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    class AdjacencyPairPrefab_greeting : AdjacencyPair
    {
        public AdjacencyPairPrefab_greeting(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe purpose of paramaters
             * if 2-stroke greeting
             * 1. initiating part: hi
             * 2. response: hi
             * 
             * if 4-stroke greeting
             * 1. initiating part: PAIR(hi, hi) (recursive)
             * 2. responding part: PAIR(how are you exchange, how are you exchange)
             * etc.
             */

            /*1. initiating options*/
            //initiation 1: if its a two stroke just say hi
            Action initiation;
            Action response;

            Type typepoo = parent.GetType();
            Boolean poo = parent.GetType() == typeof(AdjacencyPairPrefab_greeting);
            Boolean poo2 = parent.Equals(typeof(AdjacencyPairPrefab_greeting));
            Boolean poo3 = parent.GetType().Equals(typeof(AdjacencyPairPrefab_greeting));

            if (conversationalParamaters.greetingMode == ConversationalParamaters.GreetingMode.twoTurn || parent is AdjacencyPairPrefab_greeting)
            {
                initiation = new Action(new Conversation.MovesQueueItem("senseGreeting", new object[2] { q, conversationalParamaters }));//hi
                response = new Action(new Conversation.MovesQueueItem("senseGreeting", new object[2] { q.cloneAndSwapSpeakers(), conversationalParamaters }));//hi
            }
            else //assume we're doing a four-stroke greeting. initation is a new normal greeting pair (recursive), response is a pair of howAreYou exchanges
            {
                initiation = new Action(new AdjacencyPairPrefab_greeting(this, conversationalParamaters, q));
                response = new Action(new AdjacencyPairPrefab_greeting_questionExchange(this, conversationalParamaters, q.cloneAndSwapSpeakers()));

            }

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
            base.init("greeting",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
