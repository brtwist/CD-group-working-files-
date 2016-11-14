using System;
using System.Collections.Generic;
using ContextualDialogue.WorldManager;
using System.Reflection;
using ContextualDialogue.DialogueGenerator.LinguisticDictionary;

namespace ContextualDialogue.DialogueGenerator
{

    public partial class Conversation
    {
        //TODO make participants actual agent objects
        private String participantOne, participantTwo;

        //mode: open-ended (keeps going till told to close) close-ended (parses whatever is initially pushed without extending) goal-orientated (keeps going till goal is reached)
        Boolean autoGenerateQUDitems;

        //type e.g. business, friendly
        //politeness
        //amiability
        //tense


        private World world;
        private LinguisticDictionary.LinguisticDictionary vocabDictionary;
        //private UtteranceGenerator utteranceGenerator;
        private Random r;

        //public String output;
        private Queue<Turn> outputQueue;//queue of utterances


        public Conversation(World w, LinguisticDictionary.LinguisticDictionary v, String p1, String p2 /*TODO QUDitem*//*TODO boolean autogeneratequsitems*/)
        {
            //init
            world = w;
            vocabDictionary = v;
            r = new Random();
            
            QUD = new Queue<QUDitem>();

            MovesQueue = new Queue<MovesQueueItem>();

            outputQueue = new Queue<Turn>();

            //type = 
            participantOne = p1;
            participantTwo = p2;
            //location = 
            autoGenerateQUDitems = true;//turn on by default
            
            initQUD();
            go();
        }



        /*MAIN UPDATE LOOP*/
        /*this function is the heartbeat of the whole system
         * first it checks if it needs to generate new topics of conversation to the QUD
         * second it processes all available material
         * last it closes the conversation gracefully if needed
         * 
         * special notes on algorithm
         * the magic part about this is that new topics can only be generated when the go loop is called,
         * otherwise the conversation would a) infinitely loop b) always have a huge buffer built up
         * instead with this implementation the conversation always finishes one topic cleanly and waits
         * to see if its supposed to go to FarewellPhase, be given a topic, generate a topic etc.
         * */
        public void go()
        {
            //if its run out of conversation topics, make new ones (if autogenerate is turned on)
            if (autoGenerateQUDitems && QUD.Count == 0)
                seedQUD();
            

            while (MovesQueue.Count > 0 /*TODO && !paused*/)
                step();

            ////this next bit of code would allow it to autoclose. without it, designed must manually close every conversation
            ////if autogenerate is off and qud is empty, go to farewellPhase
            //if (!autoGenerateQUDitems && QUD.Count == 0)
            //{
            //    closeQUD();

            //    //process out the farewell phase that was just added to the empty QUD
            //    while (MovesQueue.Count > 0 /*TODO && !paused*/)
            //        step();
            //}

        }

        //process next MovesQueue item
        private void step()
        {
            
            if (MovesQueue.Count > 0)
            {
                Type t = typeof(Conversation);

                MovesQueueItem p = MovesQueue.Dequeue();
 

                    //loop makes array of types from the paramaters
                    //(necessary for the getmethod function in order to differentiate between overloaded methods)

                    Type[] paramaterTypes = new Type[p.paramaters.Length];

                    for (int i = 0; i < p.paramaters.Length; i++)
                    {
                        paramaterTypes[i] = p.paramaters[i].GetType();
                    }

                    //search for correct method
                    MethodInfo methodInfo = t.GetMethod(p.methodToCall, paramaterTypes);

                    //invoke, add result to outputQueue 
                    try
                    {
                    //this if statement checks whether the paramater type is of type 'this'. if it is, this MovesQueueitem is probably a phase not a move
                    if (paramaterTypes[0] == typeof(int)) //string passed as dummy paramater for phaseparsing methods
                        methodInfo.Invoke(this/*utteranceGenerator*/, p.paramaters);
                    
                    else
                        outputQueue.Enqueue(new Turn(p.paramaters[0], (string)methodInfo.Invoke(this/*utteranceGenerator*/, p.paramaters)));
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                        {
                            string err = e.InnerException.Message;//get the inner exception
                            throw;//rethrow the exception
                        }
                    }
            }
        }

        public void closeConversation()
        {
            //TODO add paramater that specifies how urgently it must close
            //TODO maybe specify reason such as 'im hungry. need to go soon' to be inserted now, and the actual farewell phase inserted on end of QUD
            //TODO turn off its ability to keep making new conversation
            closeQUD();
            //closing sequence now on the queue, read to process to output buffer
            go();
            //every queue should now be empty except output buffer
        }

       //TODO public void flush()
       //flushes QUD, MovesQueue and output queue in order to restart convo, if needed
       
       //TODO public void pause() //use go() again to resume
       //pauses the conversation from taking any more steps
       //might be useful if a participant is in multiple conversations at once and needs to pause one while the other gets updated

        public Turn getNextOutput()
        {
            //test();
            step();

            if (hasNextOutput())
                return outputQueue.Dequeue();
            else
                return new Turn("error: ", "output queue empty");

        }

        public Turn peekOutput()
        {
            if (hasNextOutput())
                return outputQueue.Peek();
            else
                return new Turn("error: ", "output queue empty");
            
        }

        public bool hasNextOutput()
        {
            return outputQueue.Count > 0;
        }
    }
}
