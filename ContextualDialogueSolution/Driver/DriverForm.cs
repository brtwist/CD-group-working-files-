using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WorldManagerNamespace;
using DialogueGeneratorNamespace;

namespace DriverNamespace
{
    public partial class DriverForm : Form
    {
        private WorldManager worldManager;
        private DialogueGenerator dialogueGenerator;

        public DriverForm()
        {
            InitializeComponent();
            worldManager = new WorldManager();

            
        }

        private void loadWorld()
        {
            worldManager.load();//loads default world
            worldNameLabel.Text = worldManager.getWorldName();
            dialogueGenerator = new DialogueGenerator(worldManager);

            //now that a world is loaded we can enable the groupbox
            outputGroupBox.Enabled = true;
        }

        private void closeWorld()
        {
            worldNameLabel.Text = "<none>";

            //disable output field and re-enable loadbutton
            outputGroupBox.Enabled = false;
            loadWorldButton.Enabled = true;

        }

        private void updateOutput(String output)
        {
            outputRichTextBox.AppendText("Agent 1 says: " + output + "\n");
        }

        private void loadWorldButton_Click(object sender, EventArgs e)
        {
            loadWorld();
            loadWorldButton.Enabled = false;
        }

        private void CreateConvoButton_Click(object sender, EventArgs e)
        {
            dialogueGenerator.newConversation();
            updateOutput(dialogueGenerator.getOutput());
       
        }
    }
}
