using EnumNamespace;
using System.Collections.Generic;

namespace ContextualDialogue.DialogueGenerator
{
    partial class Conversation
    {
        //struct to hold MovesQueue items
        private struct MovesQueueItem
        {
            public string methodToCall;
            public /*params*/ object[] paramaters;
        }

        //queue of MovesQueue items for processing
        private Queue<MovesQueueItem> MovesQueue;

        public void parseGreetingPhase(QUDitem quditem)
        {
            //TODO check variables like what type of conversation is happening

            /*ADD GREETING SEQUENCE*/
            MovesQueueItem q = new MovesQueueItem();
            q.methodToCall = "senseGreeting";
            q.paramaters = new object[2] { quditem.initiatingSpeaker, quditem.respondingSpeaker };
            MovesQueue.Enqueue(q);

            q = new MovesQueueItem();
            q.methodToCall = "senseGreeting";
            q.paramaters = new object[2] { quditem.respondingSpeaker, quditem.initiatingSpeaker };
            MovesQueue.Enqueue(q);

        }

        /*this is used for the additional two turns in a fourTurn greeting*/
        public void parseGreetingPhaseQuestions(QUDitem quditem)
        {
            //TODO check variables like what type of conversation is happening

            /*ADD GREETING SEQUENCE*/
            MovesQueueItem q;

            q = new MovesQueueItem();
            q.methodToCall = "senseGreetingQuestion";
            q.paramaters = new object[1] { quditem.initiatingSpeaker };
            MovesQueue.Enqueue(q);

            q = new MovesQueueItem();
            q.methodToCall = "senseGreetingAnswer";
            q.paramaters = new object[1] { quditem.respondingSpeaker };
            MovesQueue.Enqueue(q);

            q = new MovesQueueItem();
            q.methodToCall = "senseGreetingQuestionReciprocating";
            q.paramaters = new object[1] { quditem.respondingSpeaker };
            MovesQueue.Enqueue(q);

            q = new MovesQueueItem();
            q.methodToCall = "senseGreetingAnswer";
            q.paramaters = new object[1] { quditem.initiatingSpeaker };
            MovesQueue.Enqueue(q);
        }

        /*used to ask about where something is*/
        public void parseExchangeWhere(QUDitem quditem)
        {
            MovesQueueItem q = new MovesQueueItem();
            q.methodToCall = "senseAskAboutWhere";
            q.paramaters = new object[3] { quditem.initiatingSpeaker, quditem.subject, Tense.present };
            MovesQueue.Enqueue(q);

            //if responding participant knows the answer
            q = new MovesQueueItem();
            q.methodToCall = "senseTellAboutWhere";
            q.paramaters = new object[3] { quditem.respondingSpeaker, quditem.subject, conversationalParamaters.conversationLocation };
            MovesQueue.Enqueue(q);

            //plus reaction
            //q = new MovesQueueItem();
            //q.methodToCall = ""

            //TODO if responding participant doesnt know the answer

            //TODO plus no problem for not knowing

            //now reactions

        }

        public void parseFarewellPhase(QUDitem quditem)
        {
            //check type of convo and what type of goodbye it requires
            if (conversationalParamaters.farewellMode == ConversationalParamaters.FarewellMode.simple)
            {
                MovesQueueItem q = new MovesQueueItem();
                q.methodToCall = "senseFarewell";
                q.paramaters = new object[2] { quditem.initiatingSpeaker, quditem.respondingSpeaker };
                MovesQueue.Enqueue(q);

                q = new MovesQueueItem();
                q.methodToCall = "senseFarewell";
                q.paramaters = new object[2] { quditem.respondingSpeaker, quditem.initiatingSpeaker };
                MovesQueue.Enqueue(q);
            }

        }


        /*
        //####################### Testing informational request
        PhysicalEntity f;
        do
        {
            f = (PhysicalEntity)world.getRandom();
        } while (f.spatialParent == null);//keep going if object has no parent

        Tense tense = Tense.present;

        //senseAskAboutFacts(f, f.descriptors[r.Next(1, f.descriptors.Count)]);
        //if (r.NextDouble() < 0.5 && f.descriptors.Count > 0)
        //    outputQueue.Enqueue(new Utterance(1, senseAskAboutFacts(f, f.descriptors[r.Next(1, f.descriptors.Count)], t)));///////////////////
        //else
        //    outputQueue.Enqueue(new Utterance(1, senseAskAboutWhere(f, t)));//////////////////////////
        //####################### end testing code


        //####################### push test MovesQueue items
        q = new MovesQueueitem();
        //q.methodToCall = "senseAskAboutFacts";
        //q.paramaters = new object[4] {participantOne, f, f.descriptors[r.Next(1, f.descriptors.Count)], tense };

        //MovesQueue.Enqueue(q);

        //push another item
        q = new MovesQueueitem();
        q.methodToCall = "senseRequestAssistance";
        q.paramaters = new object[4] { participantOne, participantOne, Verb.help , f };

        MovesQueue.Enqueue(q);

        //push another item
        q = new MovesQueueitem();
        q.methodToCall = "senseResponseAssistance";
        q.paramaters = new object[1] { participantTwo };

        MovesQueue.Enqueue(q);


        //push another item
        q = new MovesQueueitem();
        q.methodToCall = "senseAskAboutWhere";
        q.paramaters = new object[3] { participantOne, f, tense };

        MovesQueue.Enqueue(q);

        //####################### end MovesQueue entry

        //push another item
        q = new MovesQueueitem();
        q.methodToCall = "senseResponseInformative";
        q.paramaters = new object[1] { participantTwo };

        MovesQueue.Enqueue(q);

        //push another item
        q = new MovesQueueitem();
        q.methodToCall = "senseThanks";
        q.paramaters = new object[1] { participantOne };

        MovesQueue.Enqueue(q);

        //push another item
        q = new MovesQueueitem();
        q.methodToCall = "senseFarewell";
        q.paramaters = new object[1] { participantTwo };

        MovesQueue.Enqueue(q);

        //push another item
        q = new MovesQueueitem();
        q.methodToCall = "senseFarewell";
        q.paramaters = new object[1] { participantOne };

        MovesQueue.Enqueue(q);
        */
    }
}
