using System;
using System.Collections.Generic;
using System.Text;
using ContextualDialogue.WorldManager;
using ContextualDialogue.DialogueGenerator.LinguisticDictionary;
using EnumNamespace;

namespace ContextualDialogue.DialogueGenerator
{
    public class DialogueGenerator
    {
        private string WORLDFILEPATH;
        private string DICTIONARYDIRECTORY;

        private WorldEngine worldManager;
        private LinguisticDictionary.LinguisticDictionary vocabDictionary;

        public Conversation conversation;//may need to make this a list of conversations if multiples convos are going on at once
        //private list of recent conversations and utterances

        public DialogueGenerator(WorldEngine wm)
        {
            worldManager = wm;
            vocabDictionary = new LinguisticDictionary.LinguisticDictionary();
        }

        public void newConversation(String participantOne, String participantTwo/* , type, topic*/)
        {
            conversation = new Conversation(worldManager.world, vocabDictionary, participantOne, participantTwo/*Agent2, , type, topic*/);
            //conversation.getOutput();
        }

        public ContextualDialogue.DialogueGenerator.Turn getOutput()
        {
            return conversation.getNextOutput();
        }

        public bool hasNextOutput()
        {
            return conversation.hasNextOutput();
        }
    }
}
