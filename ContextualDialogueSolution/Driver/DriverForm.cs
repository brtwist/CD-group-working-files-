using ContextualDialogue.DialogueGenerator;
using ContextualDialogue.WorldManager;
using System;
using System.Drawing;
using System.Windows.Forms;

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
                if (output.participant.ToString().CompareTo("Agent 1") == 0)
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

        /*this is the entry point for the dialogue manager*/
        private void CreateConvoButton_Click(object sender, EventArgs e)
        {
            ConversationalParamaters cParams = 
                new ConversationalParamaters(ConversationalParamaters.conversationType.helloOnly, "Agent 1", "Agent 2");
            cParams.greetingMode = ConversationalParamaters.GreetingMode.fourTurn;
            cParams.farewellMode = ConversationalParamaters.FarewellMode.simple;//by default conversation type helloOnly doesnt have a farewell
            cParams.conversationLocation = worldManager.world.findByProperNoun("Germany");

            QUDitem q = new QUDitem(QUDitem.ExchangeTypeEnum.where);
            q.subject = worldManager.world.findByProperNoun("Westerberg Campus");

            cParams.addQUDitem(q);



            if (dialogueGenerator.conversation == null)
                dialogueGenerator.newConversation(cParams);

            //get everything off the output buffer
            while (dialogueGenerator.hasNextOutput())
                updateOutput(dialogueGenerator.getOutput());

            EndConvoButton.Enabled = true;

        }

        private void EndConvoButton_Click(object sender, EventArgs e)
        {
            dialogueGenerator.conversation.closeConversation();

            //get everything off the output buffer
            while (dialogueGenerator.hasNextOutput())
                updateOutput(dialogueGenerator.getOutput());

            outputRichTextBox.AppendText("------------------------------------------------------------------------\n");

            loadWorld();//reset the world and wipe the existing convo object
        }
    }
}
