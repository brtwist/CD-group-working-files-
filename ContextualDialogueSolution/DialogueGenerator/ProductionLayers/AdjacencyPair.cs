using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers
{
    /*This class is basically a paramater list for layer two functions
     * it contains at minimum one move to be executed
     * normally it contains a move + a series of possible responses to the first move
     * each response contains a probability, and a degree to which that response is preferd by the initiating speaker
     * 
     * if only one response is specified, it will always be taken
     * if no preferedness is specified, it is assumed neutral
     */
    class AdjacencyPair
    {
        public struct Action
        {
            String methodToCall;
            object[] paramaters;

            Double preferedness;
            Double probability;
        }

        public Action initiatingAction;
        public Action[] possibleRespondingActions;

        public AdjacencyPair(Action initiatingAction, Action[] possibleRespondingActions)
        {
            this.initiatingAction = initiatingAction;
            this.possibleRespondingActions = possibleRespondingActions;
        }

    }
}
