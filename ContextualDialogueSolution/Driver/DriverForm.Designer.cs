namespace DriverNamespace
{
    partial class DriverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.worldNameLabel = new System.Windows.Forms.Label();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.EndConvoButton = new System.Windows.Forms.Button();
            this.conversationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.agent2omboBox2 = new System.Windows.Forms.ComboBox();
            this.agent1ComboBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.hashOutputLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.createConvoButton = new System.Windows.Forms.Button();
            this.outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.loadWorldButton = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.outputGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(406, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openToolStripMenuItem.Text = "&Load world";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.loadWorldButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(176, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveToolStripMenuItem.Text = "&Save Output";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Enabled = false;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Enabled = false;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "World: ";
            // 
            // worldNameLabel
            // 
            this.worldNameLabel.AutoSize = true;
            this.worldNameLabel.Location = new System.Drawing.Point(73, 34);
            this.worldNameLabel.Name = "worldNameLabel";
            this.worldNameLabel.Size = new System.Drawing.Size(43, 13);
            this.worldNameLabel.TabIndex = 4;
            this.worldNameLabel.Text = "<none>";
            // 
            // outputGroupBox
            // 
            this.outputGroupBox.Controls.Add(this.EndConvoButton);
            this.outputGroupBox.Controls.Add(this.conversationTypeComboBox);
            this.outputGroupBox.Controls.Add(this.label3);
            this.outputGroupBox.Controls.Add(this.agent2omboBox2);
            this.outputGroupBox.Controls.Add(this.agent1ComboBox);
            this.outputGroupBox.Controls.Add(this.button2);
            this.outputGroupBox.Controls.Add(this.label1);
            this.outputGroupBox.Controls.Add(this.hashOutputLabel);
            this.outputGroupBox.Controls.Add(this.label4);
            this.outputGroupBox.Controls.Add(this.createConvoButton);
            this.outputGroupBox.Controls.Add(this.outputRichTextBox);
            this.outputGroupBox.Enabled = false;
            this.outputGroupBox.Location = new System.Drawing.Point(12, 61);
            this.outputGroupBox.Name = "outputGroupBox";
            this.outputGroupBox.Size = new System.Drawing.Size(382, 488);
            this.outputGroupBox.TabIndex = 5;
            this.outputGroupBox.TabStop = false;
            this.outputGroupBox.Text = "conversationGroupBox";
            this.outputGroupBox.Enter += new System.EventHandler(this.outputGroupBox_Enter);
            // 
            // EndConvoButton
            // 
            this.EndConvoButton.Location = new System.Drawing.Point(284, 158);
            this.EndConvoButton.Name = "EndConvoButton";
            this.EndConvoButton.Size = new System.Drawing.Size(82, 23);
            this.EndConvoButton.TabIndex = 12;
            this.EndConvoButton.Text = "Farewell!";
            this.EndConvoButton.UseVisualStyleBackColor = true;
            this.EndConvoButton.Click += new System.EventHandler(this.EndConvoButton_Click);
            // 
            // conversationTypeComboBox
            // 
            this.conversationTypeComboBox.FormattingEnabled = true;
            this.conversationTypeComboBox.Location = new System.Drawing.Point(112, 83);
            this.conversationTypeComboBox.Name = "conversationTypeComboBox";
            this.conversationTypeComboBox.Size = new System.Drawing.Size(258, 21);
            this.conversationTypeComboBox.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Conversation Type:";
            // 
            // agent2omboBox2
            // 
            this.agent2omboBox2.FormattingEnabled = true;
            this.agent2omboBox2.Location = new System.Drawing.Point(221, 48);
            this.agent2omboBox2.Name = "agent2omboBox2";
            this.agent2omboBox2.Size = new System.Drawing.Size(149, 21);
            this.agent2omboBox2.TabIndex = 9;
            // 
            // agent1ComboBox
            // 
            this.agent1ComboBox.FormattingEnabled = true;
            this.agent1ComboBox.Items.AddRange(new object[] {
            "Agent 1"});
            this.agent1ComboBox.Location = new System.Drawing.Point(61, 48);
            this.agent1ComboBox.Name = "agent1ComboBox";
            this.agent1ComboBox.Size = new System.Drawing.Size(149, 21);
            this.agent1ComboBox.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(364, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Randomise";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Agents: ";
            // 
            // hashOutputLabel
            // 
            this.hashOutputLabel.AutoSize = true;
            this.hashOutputLabel.Location = new System.Drawing.Point(86, 458);
            this.hashOutputLabel.Name = "hashOutputLabel";
            this.hashOutputLabel.Size = new System.Drawing.Size(16, 13);
            this.hashOutputLabel.TabIndex = 5;
            this.hashOutputLabel.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 458);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Unique hash: ";
            // 
            // createConvoButton
            // 
            this.createConvoButton.Location = new System.Drawing.Point(6, 158);
            this.createConvoButton.Name = "createConvoButton";
            this.createConvoButton.Size = new System.Drawing.Size(272, 23);
            this.createConvoButton.TabIndex = 1;
            this.createConvoButton.Text = "Go!";
            this.createConvoButton.UseVisualStyleBackColor = true;
            this.createConvoButton.Click += new System.EventHandler(this.CreateConvoButton_Click);
            // 
            // outputRichTextBox
            // 
            this.outputRichTextBox.Location = new System.Drawing.Point(6, 187);
            this.outputRichTextBox.Name = "outputRichTextBox";
            this.outputRichTextBox.ReadOnly = true;
            this.outputRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.outputRichTextBox.Size = new System.Drawing.Size(364, 264);
            this.outputRichTextBox.TabIndex = 0;
            this.outputRichTextBox.Text = "";
            // 
            // loadWorldButton
            // 
            this.loadWorldButton.Location = new System.Drawing.Point(259, 29);
            this.loadWorldButton.Name = "loadWorldButton";
            this.loadWorldButton.Size = new System.Drawing.Size(123, 23);
            this.loadWorldButton.TabIndex = 6;
            this.loadWorldButton.Text = "Load World";
            this.loadWorldButton.UseVisualStyleBackColor = true;
            this.loadWorldButton.Click += new System.EventHandler(this.loadWorldButton_Click);
            // 
            // DriverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 561);
            this.Controls.Add(this.loadWorldButton);
            this.Controls.Add(this.outputGroupBox);
            this.Controls.Add(this.worldNameLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "DriverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driver Program";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.outputGroupBox.ResumeLayout(false);
            this.outputGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label worldNameLabel;
        private System.Windows.Forms.GroupBox outputGroupBox;
        private System.Windows.Forms.ComboBox agent1ComboBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label hashOutputLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button createConvoButton;
        private System.Windows.Forms.RichTextBox outputRichTextBox;
        private System.Windows.Forms.Button loadWorldButton;
        private System.Windows.Forms.ComboBox agent2omboBox2;
        private System.Windows.Forms.ComboBox conversationTypeComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button EndConvoButton;
    }
}

