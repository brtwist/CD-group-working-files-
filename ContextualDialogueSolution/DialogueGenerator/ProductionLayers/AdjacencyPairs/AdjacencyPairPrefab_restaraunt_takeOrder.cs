using System;
using ContextualDialogue.DialogueGenerator.LinguisticDictionary;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    class AdjacencyPairPrefab_restaraunt_takeOrder : AdjacencyPair
    {
        //overloaded constructor default booleans
        public AdjacencyPairPrefab_restaraunt_takeOrder(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q, TypeDefinition thingToOrder)
        : this(parent, conversationalParamaters, q, thingToOrder, true/*default value TRUE*/) { }

        public AdjacencyPairPrefab_restaraunt_takeOrder(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q, TypeDefinition thingToOrder, Boolean isThatOrderPossible)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe function
             *check that waiter is the initiator, else swap
             * 
             * 
             *initiating part
             * can i take your order
             * 
             * respondong part
             * PAIR(place order...)
             * TODO "no, i need more time to decide" "ok ill come back soon"
             */

            /*1. initiating options*/
            Action initiation;
            Action response;

            //make the verb for the modal request
            Verb canITakeOrder = new Verb(q.initiatingSpeaker, "take");
            canITakeOrder.setMonoTransitive("", "your order");//the verb renderer should automatically render it as 'your X'

            initiation = new Action(new Conversation.MovesQueueItem("sense_modal_request", new object[2] { q, canITakeOrder }));
            response = new Action(new AdjacencyPairPrefab_restaraunt_placeOrder(this, conversationalParamaters, q.cloneAndSwapSpeakers(), thingToOrder));//hi


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
            base.init("restaraunt take an order (different to place an order)",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
