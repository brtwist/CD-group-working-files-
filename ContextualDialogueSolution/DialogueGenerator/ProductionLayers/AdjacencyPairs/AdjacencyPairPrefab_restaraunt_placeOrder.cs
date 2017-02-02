using System;
using ContextualDialogue.DialogueGenerator.LinguisticDictionary;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    class AdjacencyPairPrefab_restaraunt_placeOrder : AdjacencyPair
    {
        //overloaded constructor defaults response to YES
        public AdjacencyPairPrefab_restaraunt_placeOrder(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q,TypeDefinition thingToOrder)
        : this(parent, conversationalParamaters, q, thingToOrder, true /*default value TRUE*/) { } 

        public AdjacencyPairPrefab_restaraunt_placeOrder(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q, TypeDefinition thingToOrder  , Boolean isThatOrderPossible)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe function
             *check that customer is the initiator, else swap
             * 
             *initiating part
             * can i have... please
             * I would like... please
             * a ... please 
             * 
             * respondong part
             * yes (preferred)
             * no (disprefferred)
             */

            /*1. initiating options*/
            Action initiation;
            Action response;

            //make the verb for the modal request
            Verb canIHaveX = new Verb(q.initiatingSpeaker, "have");
            canIHaveX.setMonoTransitive("", thingToOrder);

                initiation = new Action(new Conversation.MovesQueueItem("sense_modal_request", new object[2] { q, canIHaveX }));
                response = new Action(new Conversation.MovesQueueItem("sense_response_yesNo", new object[3] { q.cloneAndSwapSpeakers(), conversationalParamaters, isThatOrderPossible }));//hi
            

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
            base.init("restaraunt place an order",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
