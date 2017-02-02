using ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    class AdjacencyPairPrefab_discuss_short : AdjacencyPair
    {
        public AdjacencyPairPrefab_discuss_short(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q, object thingToDiscuss)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe function
             *EITHER ask the other person a question about a thing/type and they respond
             * OR offer a comment about a thing/type and they agree/disagree
             * 
             * ASK-OPINE
             *initiating part
             * PAIR(whats your feeling on x)
             * 
             * respondong part
             * PAIR(give feeling about x)
             * 
             * 
             * OPINE-AGREE/DISAGREE
             * initiating part
             * PAIR(give feeling about x)
             * 
             * responding part
             * PAIR(agree/disagree)
             */

            /*1. initiating options*/
            Action initiation;
            Action response;

            //get each persons opinion
            Opinion opinion = conversationalParamaters.world.getOpinion(q.initiatingSpeaker, thingToDiscuss);
            Opinion answer = conversationalParamaters.world.getOpinion(q.respondingSpeaker, thingToDiscuss);


            if (conversationalParamaters.r.NextDouble() >= 0.5)
            {
                //Do you like x? Is X good?      
                initiation = new Action(new Conversation.MovesQueueItem("sense_discuss_askFeelings", new object[3] { q, conversationalParamaters, opinion }));

                //I love X. X is great because its so tasty.
                response = new Action(new Conversation.MovesQueueItem("sense_discuss_giveFeelings", new object[4] { q.cloneAndSwapSpeakers(), conversationalParamaters, answer, opinion }));
            }
            else
            {

                initiation = new Action(new Conversation.MovesQueueItem("sense_discuss_giveFeelings", new object[3] { q, conversationalParamaters, answer }));

                /*see if the interlocutor agrees or not*/
                //do stuff

                //pass result to the sense
                response = new Action(new Conversation.MovesQueueItem("sense_discuss_agreeDisagree", new object[3] { q.cloneAndSwapSpeakers(), opinion, answer }));
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
            base.init("compliment",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
