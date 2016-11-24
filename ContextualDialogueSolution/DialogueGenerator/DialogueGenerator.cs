using ContextualDialogue.WorldManager;

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

        public void newConversation(ConversationalParamaters paramaters)
        {
            conversation = new Conversation(worldManager.world, vocabDictionary, paramaters);
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
