using ContextualDialogue.WorldManager;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ContextualDialogue.DialogueGenerator
{

    public partial class Conversation
    {
        private World world;
        private LinguisticDictionary.LinguisticDictionary vocabDictionary;
        private ConversationalParamaters conversationalParamaters;
        private Random r;

        private Queue<Turn> outputQueue;//queue of utterances


        public Conversation(World w, LinguisticDictionary.LinguisticDictionary v, ConversationalParamaters paramaters)
        {
            world = w;
            vocabDictionary = v;
            conversationalParamaters = paramaters;

            r = new Random();
            QUD = new Queue<QUDitem>();
            MovesQueue = new Queue<MovesQueueItem>();
            outputQueue = new Queue<Turn>();

            initQUD();//place greeting sequence, if appropriate
            go();//immediately begin processing
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
            //if there are user specified conversation topics on the paramters object, parse one into a move
            if (QUD.Count == 0 && conversationalParamaters.hasQUDitem())
                generateMoves();

            //if run out of conversation topics, make new ones (if autogenerate is turned on)
            if (conversationalParamaters.autoGenerateQUDitems && QUD.Count == 0)
                autoMagicallySeedQUD();

            //here is the main loop, processing all the queued up methods ( moves queue )
            while (MovesQueue.Count > 0 /*TODO && !paused*/)
                step();
        }

        //process next MovesQueue item
        private void step()
        {

            if (MovesQueue.Count > 0)
            {
                Type t = typeof(Conversation);//necessary for invoke for some reason

                MovesQueueItem p = MovesQueue.Dequeue();


                //loop makes array of types from the paramaters
                //(necessary for the getmethod function in order to differentiate between overloaded methods)

                Type[] paramaterTypes = new Type[p.paramaters.Length];

                for (int i = 0; i < p.paramaters.Length; i++)
                {
                    //if an exception is ever thrown here it might be becuse the name or paramaters of a production rule was wrong
                    paramaterTypes[i] = p.paramaters[i].GetType();
                }

                //search for correct method
                MethodInfo methodInfo = t.GetMethod(p.methodToCall, paramaterTypes);

                /*INVOKE THE METHOD*/
                try
                {
                    /*The following if statement determines whether the method produces output or not
                     * methods producing output need to enqueue string return values onto the output buffer
                     * this is determined by the fact that only middlelayer functions take a quditem as paramater
                     */
                    if (paramaterTypes[0] == typeof(QUDitem))
                        methodInfo.Invoke(this/*utteranceGenerator*/, p.paramaters);
                    else
                        outputQueue.Enqueue(new Turn(p.paramaters[0], (string)methodInfo.Invoke(this/*utteranceGenerator*/, p.paramaters)));
                }
                catch (Exception e)
                {
                    outputQueue.Enqueue(new Turn("System", "Exception thrown in reflection invoke of " + p.methodToCall));
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

            //stop making new conversation
            conversationalParamaters.autoGenerateQUDitems = false;

            closeQUD();//farewell sequence and any remaining topics now processed
            go();//process all remaining items on all layers.

            //final output can now be collected off the output buffer, this conversation object is finished
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
                return new Turn("error: ", "output queue empty");//this code should be unreachable

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
