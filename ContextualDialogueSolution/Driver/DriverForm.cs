using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ContextualDialogue.WorldManager;
using ContextualDialogue.DialogueGenerator;

namespace DriverNamespace
{
    public partial class DriverForm : Form
    {
        private WorldEngine worldManager;
        private DialogueGenerator dialogueGenerator;

        public DriverForm()
        {
            InitializeComponent();
            worldManager = new WorldEngine();

            
        }

        private void loadWorld()
        {
            //worldManager.load();//loads default world
            worldNameLabel.Text = worldManager.world.worldName;
            dialogueGenerator = new DialogueGenerator(worldManager);
            worldManager.loadWorldFile();

            //now that a world is loaded we can enable the groupbox
            outputGroupBox.Enabled = true;
            //disable till a convo has been started
            EndConvoButton.Enabled = false;
        }

        private void closeWorld()
        {
            worldNameLabel.Text = "<none>";

            //disable output field and re-enable loadbutton
            outputGroupBox.Enabled = false;
            loadWorldButton.Enabled = true;
            openToolStripMenuItem.Enabled = true;

        }

        private void updateOutput(ContextualDialogue.DialogueGenerator.Turn output)
        {
            
            int start = outputRichTextBox.TextLength;
            outputRichTextBox.AppendText(output.participant.ToString() + "| " + output.utterance + "\n");

            int end = outputRichTextBox.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            outputRichTextBox.Select(start, end);
            {
                if(output.participant.ToString().CompareTo("Agent 1") == 0)
                    outputRichTextBox.SelectionColor = Color.Blue;
                if (output.participant.ToString().CompareTo("Agent 2") == 0)
                    outputRichTextBox.SelectionColor = Color.Green;

            }
            outputRichTextBox.SelectionLength = 0; // clear
        }

        private void loadWorldButton_Click(object sender, EventArgs e)
        {
            loadWorld();
            loadWorldButton.Enabled = false;
            openToolStripMenuItem.Enabled = false;
        }

        private void CreateConvoButton_Click(object sender, EventArgs e)
        {
            ConversationalParamaters cParams = new ConversationalParamaters(ConversationalParamaters.conversationType.helloOnly, "Agent 1", "Agent 2");
            cParams.greetingMode = ConversationalParamaters.GreetingMode.fourTurn;

            if(dialogueGenerator.conversation == null)
                dialogueGenerator.newConversation(cParams);

            //get everything off the output buffer
            while(dialogueGenerator.hasNextOutput())
                updateOutput(dialogueGenerator.getOutput());

            EndConvoButton.Enabled = true;
       
        }

        private void EndConvoButton_Click(object sender, EventArgs e)
        {
            dialogueGenerator.conversation.closeConversation();

            //get everything off the output buffer
            while (dialogueGenerator.hasNextOutput())
                updateOutput(dialogueGenerator.getOutput());

            outputRichTextBox.AppendText("------------------------------------------------------------------------");

            loadWorld();//reset the world and wipe the existing convo object
        }
    }
}
