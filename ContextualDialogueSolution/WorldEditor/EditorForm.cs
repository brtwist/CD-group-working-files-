using ContextualDialogue.WorldManager;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using EnumNamespace;
using System;
using System.Windows.Forms;


namespace WorldEditorNamespace
{
    public partial class EditorForm : Form
    {
        private WorldEngine worldManager;
        private String FILE_NAME = "..\\savedworld.world";

        public EditorForm()
        {
            InitializeComponent();
            worldManager = new WorldEngine();
        }

        private void newWorld()
        {
            worldManager = new WorldEngine();
            worldManager.newWorld("Hello World");


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

            int osna = worldManager.world.add(pe3);//int is the ID of the object

            PhysicalEntity nrw = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("state"));
            nrw.properNoun = "North Rhine Westfalia";
            nrw.setSpatialParent(Preposition.@in, pe2);
            nrw.oneOfAKind = true;

            int nrwInt = worldManager.world.add(nrw);//int is the ID of the object

            PhysicalEntity pe4 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("area"));
            pe4.properNoun = "Westerberg Campus";
            pe4.setSpatialParent(Preposition.@in, pe3);
            pe4.oneOfAKind = true;

            int westerberg = worldManager.world.add(pe4);//int is the ID of the object

            PhysicalEntity pe5 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("area"));
            pe5.properNoun = "Schloss Campus";
            pe5.setSpatialParent(Preposition.@in, pe3);//in osnabrueck
            pe5.oneOfAKind = true;

            int schloss = worldManager.world.add(pe5);//int is the ID of the object

            PhysicalEntity pe6 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("area"));
            pe6.properNoun = "Mensa";
            pe6.setSpatialParent(Preposition.@on, pe4);
            pe6.oneOfAKind = false;

            int mensa = worldManager.world.add(pe6);//int is the ID of the object

            PhysicalEntity pe7 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("banana"));
            //pe4.properNoun = "Westerberg Campus";
            pe7.setSpatialParent(Preposition.@in, pe6);
            pe7.oneOfAKind = true;

            int banana = worldManager.world.add(pe7);//int is the ID of the object

            PhysicalEntity pe8 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("coffee"));
            //pe4.properNoun = "Westerberg Campus";
            pe8.setSpatialParent(Preposition.@in, pe6);
            pe8.oneOfAKind = true;

            int coffee = worldManager.world.add(pe8);//int is the ID of the object

            PhysicalEntity pe9 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("mango"));
            //pe4.properNoun = "Westerberg Campus";
            pe9.setSpatialParent(Preposition.@in, pe6);
            pe9.oneOfAKind = true;

            int mango = worldManager.world.add(pe9);//int is the ID of the object

            PhysicalEntity pe10 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("banana"));
            //pe4.properNoun = "Westerberg Campus";
            pe10.setSpatialParent(Preposition.@in, pe6);
            pe10.oneOfAKind = true;

            int banana2 = worldManager.world.add(pe10);//int is the ID of the object

            PhysicalEntity pe11 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("bar"));
            pe11.properNoun = "Sauber von Oz";
            pe11.setSpatialParent(Preposition.@in, worldManager.world.findByProperNoun("Osnabrück"));
            pe11.oneOfAKind = true;

            int svo = worldManager.world.add(pe10);//int is the ID of the object

            PhysicalEntity pe12 = new PhysicalEntity(worldManager.typeDictionary.tryGetValue("beer"));
            //pe11.properNoun = "Sauber von Oz";
            pe11.setSpatialParent(Preposition.@in, pe11);
            pe11.oneOfAKind = true;

            int thingy = worldManager.world.add(pe12);//int is the ID of the object
            /*END adding objects*/

            worldManager.saveWorldFile();
            loadWorld();
        }

        private void loadWorld()
        {
            worldManager.loadWorldFile();
            worldGroupBox.Enabled = true;
            worldGroupBox.Text = "World: " + worldManager.world.worldName;

            worldListBox.Items.Clear();
            worldListBox.Items.AddRange(worldManager.world.getAllphysicalEntitiesToString());
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
