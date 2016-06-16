using System;
using System.Collections.Generic;
using System.Text;
using WorldManagerNamespace;
using EnumNamespace;

namespace DialogueGeneratorNamespace
{
    public class DialogueGenerator
    {
        private WorldManager worldManager;
        private Conversation conversation;//may need to make this a list of conversations if multiples convos are going on at once
        //private list of recent conversations and utterances

        public DialogueGenerator(WorldManager wm)
        {
            worldManager = wm;
        }

        public void newConversation(/*Agent1, Agent2, , type, topic*/)
        {
            conversation = new Conversation(worldManager.world/*Agent1, Agent2, , type, topic*/);
            //conversation.getOutput();
        }

        public String getOutput()
        {
            return conversation.output;
        }
    }
}
