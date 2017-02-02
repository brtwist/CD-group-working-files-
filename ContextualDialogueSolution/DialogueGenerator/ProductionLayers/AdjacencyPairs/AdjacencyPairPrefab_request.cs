using ContextualDialogue.DialogueGenerator.LinguisticDictionary;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    public class AdjacencyPairPrefab_request : AdjacencyPair
    {
        public AdjacencyPairPrefab_request(AdjacencyPair parent, ConversationalParamaters conversationalParamaters, PairParamaters q, Verb verb)
        {
            //init preset paramaters, then pass them up to the base class

            /*describe purpose of paramaters
             * 1. initiating part: request
             * 2. prefered response: grant request
             * 3. disprefered response: deny request
             * 4. default response: noncomittal
             * conditions:
             */

            /*1. initiating options*/
            //initiation 1: get straight to the point and request
            Action initiationDirect = new Action(new Conversation.MovesQueueItem("sense_request", new object[2] { q, verb }));

            //initation 2: first ask if its ok to make a request, then make the request (nests another request inside this request)
            //more polite, more long-winded, less direct
            Action[] initiatingActionArray;

            //if (not already a nested request) AND (random chance that increases with politeness)
            if ((this.parent.GetType() != typeof(AdjacencyPairPrefab_request)) && (conversationalParamaters.r.Next(4, 10) <= q.politeness))//becomes linearly more likely the higher the politeness is. minimum politeness 4
            {
                //make the adjacency pair then insert in action
                Verb askAQuestion = new Verb(q.initiatingSpeaker, "ask");
                askAQuestion.setMonoTransitive("", "a question");
                //put that into a moveItem
                Conversation.MovesQueueItem m = new Conversation.MovesQueueItem("sense_permission", new object[2] { 1, askAQuestion });

                //make that verb into an action which will consist of requesting to make a request, and it being either granted or denied

                Action initiationNestedRequest =
                    new Action(
                        new AdjacencyPair("request", this, conversationalParamaters,
                            new Action[1] { new Action(m) }, //initiating move
                            new Action[2] {                   //responding moves
                                    new Action(new Conversation.MovesQueueItem("sense_request_grantRequest", new object[2] { q, Preferredness.preferred })),
                                    new Action(new Conversation.MovesQueueItem("sense_request_denyRequest", new object[2] { q, Preferredness.dispreferred }))
                            }
                        )
                     );

                //put actions in the array
                initiatingActionArray = new Action[] { initiationDirect, initiationNestedRequest };
            }//end making the second option

            else //only one initiating action
            {
                initiatingActionArray = new Action[] { initiationDirect };
            }
            
            /*
             * END MAKING INITIATING OPTIONS
             *
             * START MAKING THE RESPONDING OPTIONS
             * 
             */

            /*prefered response: grant request*/
            Action preferredResponse = new Action(new Conversation.MovesQueueItem("sense_request_grantRequest", new object[] { q, Preferredness.preferred }));
            preferredResponse.preferredness = Preferredness.preferred;

            /*disprefered response: deny request*/
            Action dispreferredResponse = new Action(new Conversation.MovesQueueItem("sense_request_denyRequest", new object[] { q, Preferredness.dispreferred }));
            preferredResponse.preferredness = Preferredness.dispreferred;

            //now put all the responding actions in an array
            Action[] respondingActionArray = { preferredResponse, dispreferredResponse };

            /*
             * END MAKING RESPONDING OPTIONS
             * 
             * PUSH ALL TO BASE 
             *
             */

            //send these presets to the base
            base.init("request",
                parent,
            conversationalParamaters,
            initiatingActionArray,
            respondingActionArray
            );
        }
    }
}
