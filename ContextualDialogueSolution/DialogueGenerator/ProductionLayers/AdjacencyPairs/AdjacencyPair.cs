using System;
using System.Collections.Generic;

namespace ContextualDialogue.DialogueGenerator.ProductionLayers.AdjacencyPairs
{
    public class AdjacencyPair
    {
        /*TREE stuff: members relevant to the tree structure*/
        protected ConversationalParamaters conversationalParamaters;
        protected AdjacencyPair parent;//parent node for the tree structure

        //these are used keep track of which option was finally used, indirectly denoting the child nodes of this node
        public Action chosenInitiatingPart;//if called by parents or older siblings it wont be set yet
        protected Action chosenRespondingPart;//if called by children it wont be set yet
        private int chosenRespondingPart_index = -1;
        private string descriptionForDebugging;

        //these are used to keep track of which speaker is leading this adjacency pair
        //protected String firstSpeaker;
        //protected String secondSpeaker;

        /*END tree stuff*/

        /*define structs and enums*/

        public enum Preferredness { neutral, preferred, dispreferred, strongly_preferred, strongly_dispreferred }

        /*actions are used to encapsulate all the variables related to either an initiating or a responding move
         A pair consists of two arrays of Actions, one a list of possible initiating moves
         and one for possible responding moves

         NOTE: initiating actions may vary greatly in surface form but in meaning/truth values they must all be synonymous.
         responding actions however can be arbitrarily different*/
        public struct Action
        {
            /*an action must have one or both of a move and/or a pair
             (a move is something that is said, i.e. an utterance)
             (a pair is a new adjacency pair which will be played)
             (in most cases only one would be needed, not both)
             */
            public Conversation.MovesQueueItem move;//immediate response, i.e. an utterance
            public AdjacencyPair pair;//this is the action which will be played next

            /*conditions define when this action may be played. 
             * preferedness defines the tone of the response; whether the initiating speakers would find this utterance desirous
             */
            public Boolean condition;//mainly for use in response moves not initiating moves
            public Preferredness preferredness;//mainly for use in response moves not initiating moves

            /*used to tag the option in some way. the choosing function uses this info along with the DiscourseType of the conversation as 
            a whole in order to pick matching options where possible*/
            public ConversationalParamaters.DiscourseType discourseFunction;

            public Action(Conversation.MovesQueueItem move, AdjacencyPair pair)
            {
                this.move = move;
                this.pair = pair;
                this.condition = true;
                this.preferredness = Preferredness.neutral;
                discourseFunction = ConversationalParamaters.DiscourseType.none;
            }
            public Action(Conversation.MovesQueueItem move)
            {
                this.move = move;
                this.pair = null;
                this.condition = true;
                this.preferredness = Preferredness.neutral;
                discourseFunction = ConversationalParamaters.DiscourseType.none;
            }

            public Action(AdjacencyPair pair)
            {
                this.move = new Conversation.MovesQueueItem();//make empty move item to keep compiler happy
                this.pair = pair;
                this.condition = true;
                this.preferredness = Preferredness.neutral;
                this.discourseFunction = ConversationalParamaters.DiscourseType.none;
            }

        }

        /*END defining structs and params*/



        /*START defining class functions*/

        protected Action[] initiatingActionArray;
        protected Action[] respondingActionArray;



        /*CONSTRUCTORS*/
        public AdjacencyPair() { } //only for use as parent node and as default constructor for calling from derived classes

        /*Adjacency pair always has conversational paramaters + a first and a second part*/
        public AdjacencyPair(String descriptionForDebugging, AdjacencyPair parent, ConversationalParamaters conversationalParamaters, Action[] initiatingAction, Action[] respondingAction)
        {
            init(descriptionForDebugging, parent, conversationalParamaters, initiatingAction, respondingAction);
        }

        //this is effectively the constructors' body
        //it is here so that the constructors can be arbitrarily overloaded and then every constructor just calls this function
        //NOTE: when using the default constructor e.g. from a derived class, this function must be called manually
        virtual protected void init(String descriptionForDebugging, AdjacencyPair parent, ConversationalParamaters conversationalParamaters, Action[] initiatingAction, Action[] respondingAction)
        {
            //TODO error checking, make sure input arrays are non-empty
            this.descriptionForDebugging = descriptionForDebugging;

            this.parent = parent;
            this.conversationalParamaters = conversationalParamaters;

            this.initiatingActionArray = initiatingAction;
            this.respondingActionArray = respondingAction;
        }

        /*END CONSTRUCTORS*/

        /*
         * ENQUEUE is somewhat recursive and therefore has a long explanation:
         so theres queueing mode and this hypothetical searching mode where it checks the space and marks which path it took
         lets talk about the queueing mode:
         
            we have a hypothetical node, it has another pair on the left, stored as a pair not as an action, so we go into the pair and again look at the left side, 
         if the left side of the new node is a move instead of a pair, we queue it and then start checking the right hand sides
         on the way up. 
         
         as we do the right side we check whether a move is specified, else we do the 
         choosing function
         when we reach a 
         
         how does this function differ for inheriting pairs?
         well a normal pair doesnt hvae to do a choosing function, it only has one possibility on the RHS
             */

        /*ALGORITHM
         * 1 do left hand side, i.e. initiating part.
         * 1.1 if it has an utterance, enqueue it
         * 1.2 if it has a pair, recursively go inside
         * 1.2 left side is done, now repeat 1 for right hand side
         */

        public virtual int enqueueRecursively(Queue<Conversation.MovesQueueItem> MovesQueue)
        {
            selectionFunction();//sets the chosenAction variables

            //1.1 check if the left hand side is a pair, or whether its an utterance
            if (chosenInitiatingPart.move.methodToCall != null)//checks if the string inside is null, because structs themselves can never be null
            {
                //it's a conversation move, enqueue it
                MovesQueue.Enqueue(chosenInitiatingPart.move);
            }
            else if (chosenInitiatingPart.pair != null)//if pair is not null, enqueue it
            {
                //go recursive!
                chosenInitiatingPart.pair.enqueueRecursively(MovesQueue);
            }
            else
            {
                //error, abort
                //TODO
            }


            /*1.2 same again for the right hand side, the responding action*/

            //1.1 check if the right hand side is a pair, or whether its an utterance
            if (chosenRespondingPart.move.methodToCall != null)//checks if the string inside is null, because structs themselves can never be null
            {
                //it's a conversation move, enqueue it
                MovesQueue.Enqueue(chosenRespondingPart.move);
            }
            else if (chosenRespondingPart.pair != null)//if pair is not null, enqueue it
            {
                //go recursive!
                chosenRespondingPart.pair.enqueueRecursively(MovesQueue);
            }
            else
            {
                //error, abort
                //TODO
            }

            return 0;
        }


        /*chooses what will happen from the available options
         * first chooses an intiating action
         * then chooses a responding action
         * 
         * NOTE choosing an initiating action is probably redundant, mostly only one initiating choices should be given
         * 
         * Algorithm for initiating action:
         * 1 check there is more than one option. if only one, enqueue and break
         * 2 evaluate fitness of all options by comparing discourse function of each option with the discourse type of the conversationParamater object
         * 3 choose option with highest fitness, if multiple options with equal fitness choose randomly
         * 
         * Algorithm for responding action:
         * 1 check there is more than one option. if only one, enqueue and break
         * 2 evaluate fitness of all options by checking conditions and preferredness
         * 3 choose option with highest fitness, if multiple options with equal fitness choose randomly
         */
        protected virtual void selectionFunction()
        {
            Action choice = initiatingActionArray[0];
            int fitness = 0, bestFitnessSoFar = 0;//used as temp variables for measuring how good each option is
            SortedList<int, Action> choicesOrderedList;
            Action a;

            //choose first part
            //only try and choose if there is in fact more than one option
            if (initiatingActionArray.Length > 1)
            {
                choicesOrderedList = new SortedList<int, Action>();

                for (int i = initiatingActionArray.Length - 1; i >= 0; i--)
                {
                    fitness = 0;
                    a = initiatingActionArray[i];


                    if (a.condition) //check it's enabled
                    {
                        /*CALCULATE FITNESS - INITIATING PART*/

                        //check discourse function
                        if (a.discourseFunction == conversationalParamaters.discourseType)
                            fitness++;
                        //TODO needs better function here, e.g. even if its not an exact match a similar tag should still hold value

                        //check preferedness
                        //TODO does preferedness play a part in selecting a first part?

                        /*END CALCULATING FITNESS - INITIATING PART*/

                        //add to list if its good enough
                        if (fitness == bestFitnessSoFar)//add it
                        {
                            choicesOrderedList.Add(fitness, initiatingActionArray[i]);
                        }
                        else if (fitness >= bestFitnessSoFar)//reset the list and add it
                        {
                            bestFitnessSoFar = fitness;
                            choicesOrderedList.Clear();
                            choicesOrderedList.Add(fitness, initiatingActionArray[i]);
                        }
                    }
                }//end for loop


                //now the list consists of all the options with the equall best fitness, randomly pick one
                choice = choicesOrderedList.Values[conversationalParamaters.r.Next(0, choicesOrderedList.Count)];
            }
            //if there was only one choice it will simply have remained at 0
            chosenInitiatingPart = choice;

            /*END CHOOSING - INITIATING PART*/
            /*START CHOOSING - RESPONDING PART*/
            choice = respondingActionArray[0];
            //only try and choose if there is in fact more than one option
            if (respondingActionArray.Length > 1)
            {
                choicesOrderedList = new SortedList<int, Action>();//reset the list

                for (int i = respondingActionArray.Length - 1; i >= 0; i--)
                {
                    fitness = 0;
                    a = respondingActionArray[i];


                    if (a.condition) //check it's enabled
                    {
                        /*CALCULATE FITNESS*/

                        //positive preferredness improves fitness
                        //TODO should negative preferredness degrade fitness?
                        if (a.preferredness == Preferredness.preferred)
                            fitness += 1;
                        else if (a.preferredness == Preferredness.strongly_preferred)
                            fitness += 2;


                        /*END CALCULATING FITNESS*/

                        //add to list if its good enough
                        if (fitness == bestFitnessSoFar)//add it
                        {
                            choicesOrderedList.Add(fitness, initiatingActionArray[i]);
                        }
                        else if (fitness >= bestFitnessSoFar)//reset the list and add it
                        {
                            bestFitnessSoFar = fitness;
                            choicesOrderedList.Clear();
                            choicesOrderedList.Add(fitness, initiatingActionArray[i]);
                        }
                    }
                }//end for loop


                //now the list consists of all the options with the equall best fitness, randomly pick one
                choice = choicesOrderedList.Values[conversationalParamaters.r.Next(0, choicesOrderedList.Count)];
            }
            //if there was only one choice it will simply have remained at 0
            chosenRespondingPart = choice;

        }


        /*some kind of function that explores the conversation space virtually without enqueueing anything*/
        //public virtual void search();

    }
}
