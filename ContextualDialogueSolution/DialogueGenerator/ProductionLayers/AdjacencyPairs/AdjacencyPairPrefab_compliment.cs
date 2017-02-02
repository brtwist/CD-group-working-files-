using ContextualDialogue.DialogueGenerator.LinguisticDictionary;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    class AdjacencyPairPrefab_compliment : AdjacencyPair
    {
        public AdjacencyPairPrefab_compliment(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q, TypeDefinition thingToCompliment)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe function
             *give a compliment on either
             * a person: you look good
             * a thing: your cake is really good
             * TODO an ability: you can cook really well
             * 
             *initiating part
             * compliment
             * 
             * respondong part
             * thanks
             */

            /*1. initiating options*/
            Action initiation;
            Action response;

            Verb itsreallygood = new Verb(thingToCompliment, "is");
            itsreallygood.adverb = "really good";//TODO definitely doesnt work

            initiation = new Action(new Conversation.MovesQueueItem("sense_compliment_give", new object[2] { q, itsreallygood }));
            response = new Action(new Conversation.MovesQueueItem("senseThanks", new object[2] { q.cloneAndSwapSpeakers(), conversationalParamaters}));


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
            base.init("compliment",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
