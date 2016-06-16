using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WorldManagerNamespace;
using EnumNamespace;


namespace WorldEditorNamespace
{
    public partial class EditorForm : Form
    {
        private WorldManager worldManager;
        private String FILE_NAME = "C:\\Users\\benjamint\\Documents\\visual studio 2015\\Projects\\ContextualDialogueSolution\\savedworld.world";

        public EditorForm()
        {
            InitializeComponent();
            worldManager = new WorldManager();
        }

        private void newWorld()
        {
            worldManager = new WorldManager();
            worldManager.newWorld("worldname");
            loadWorld();
        }

        private void loadWorld()
        {
            worldManager = new WorldManager();
            worldManager.load();
            worldGroupBox.Enabled = true;
            worldGroupBox.Text = "World: " + worldManager.getWorldName();


            Place g = new Place();
            g.typeOfPlace = typeofPlace.state;
            g.noun = "Lower Saxony";
            g.unique = true;

            Place k = new Place();
            k.typeOfPlace = typeofPlace.town;
            k.noun = "Osnabrück";
            k.unique = true;
            k.setSpatialParent(g, Preposition.@in);

            Place l = new Place();
            l.typeOfPlace = typeofPlace.house;
            l.noun = "Nikolai's house";
            l.unique = true;
            l.setSpatialParent(k, Preposition.@in);

            PhysicalEntity t = new PhysicalEntity();
            t.noun = "table";
            t.setSpatialParent(l, Preposition.@in);

            Place x = new Place();
            x.typeOfPlace = typeofPlace.house;
            x.noun = "house";
            x.setSpatialParent(k, Preposition.@in);

            Coffee c = new Coffee();
            c.setSpatialParent(l, Preposition.at);

            Apple a = new Apple();
            a.setSpatialParent(t, Preposition.on);

            worldManager.world.add(g);
            worldManager.world.add(k);
            worldManager.world.add(l);
            worldManager.world.add(x);
            worldManager.world.add(c);
            worldManager.world.add(a);

            worldListBox.Items.Clear();
            worldListBox.Items.AddRange(worldManager.getAll());
        }

        private void saveWorld()
        {
            worldManager.save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            loadWorld();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            newWorld();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveWorld();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            worldManager = null;
            worldListBox.Items.Clear();
            worldGroupBox.Enabled = false;
        }
    }
}
