using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using ContextualDialogue.WorldManager;
using EnumNamespace;


namespace WorldEditorNamespace
{
    public partial class EditorForm : Form
    {
        private WorldEngine worldManager;
        private String FILE_NAME = "C:\\Users\\benjamint\\Documents\\visual studio 2015\\Projects\\ContextualDialogueSolution\\savedworld.world";

        public EditorForm()
        {
            InitializeComponent();
            worldManager = new WorldEngine();
        }

        private void newWorld()
        {
            worldManager = new WorldEngine();
            worldManager.newWorld("test worldname");


            //insert objects
            PhysicalEntity pe0;

            pe0 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("area"));
            pe0.properNoun = "Europe";
            pe0.oneOfAKind = true;
            int europe = worldManager.world.add(pe0);//int is the ID of the object


            PhysicalEntity pe;

            pe = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("country"));
            pe.properNoun = "Germany";
            pe.oneOfAKind = true;
            pe.setSpatialParent(Preposition.@in, pe0);
            int germany = worldManager.world.add(pe);//int is the ID of the object

            PhysicalEntity pe2 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("state"));
            pe2.properNoun = "LowerSaxony";
            pe2.setSpatialParent(Preposition.@in, pe);
            pe2.oneOfAKind = true;

            int lowerSaxony = worldManager.world.add(pe2);//int is the ID of the object

            PhysicalEntity pe3 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("state"));
            pe3.properNoun = "Osnabrück";
            pe3.setSpatialParent(Preposition.@in, pe2);
            pe3.oneOfAKind = true;

            int NRW = worldManager.world.add(pe3);//int is the ID of the object

            PhysicalEntity pe4 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("area"));
            pe4.properNoun = "Westerberg Campus";
            pe4.setSpatialParent(Preposition.@in, pe3);
            pe4.oneOfAKind = true;

            int westerberg = worldManager.world.add(pe4);//int is the ID of the object



            /*
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
        */

            worldManager.saveWorldFile();
            loadWorld();
        }

        private void loadWorld()
        {

            worldManager.loadWorldFile();
            worldGroupBox.Enabled = true;
            worldGroupBox.Text = "World: " + worldManager.world.worldName;

            worldListBox.Items.Clear();
            worldListBox.Items.AddRange(worldManager.world.getAllphysicalEntitiesToString() );
        }

        private void saveWorld()
        {
            worldManager.saveWorldFile();
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
