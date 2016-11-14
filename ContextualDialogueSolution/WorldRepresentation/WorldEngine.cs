using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

/*this is the main starting point for the knowledge manager. it provides a wrapper for loading and saving various parts of world knowledge*/

namespace ContextualDialogue.WorldManager
{
    public class WorldEngine
    {
        private String FILE_NAME = "C:\\Users\\benjamint\\Documents\\visual studio 2015\\Projects\\ContextualDialogueSolution\\savedworld.world";

        public World world;
        public TypeDictionary typeDictionary;

        public void newWorld(String worldName)
        {
            world = new World(worldName, typeDictionary);
            saveWorldFile();//save newly created world
        }

        public WorldEngine(/*file name typedictionary*/ /*filename world*/)
        {
            typeDictionary = new TypeDictionary();//contains its own load function
            //TODO try catch for no file or out of date file or corrupt file
            world = new World("empty world", typeDictionary);//contains its own load function
            //load();
        }

        //public void close()
        //{
        //    //save();
        //}

        //public String getWorldName()
        //{
        //    if (world != null)
        //        return world.worldName;
        //    else return "none";
        //}


        //save world
        public void saveWorldFile()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, world);
            stream.Close();
        }

        //load world
        public void loadWorldFile()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FILE_NAME,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);

            world = (World)formatter.Deserialize(stream);
            stream.Close();
        }

        ////return whole array of world objects
        //public PhysicalEntity[] getAll()
        //{
        //    return 
        //}
    }
}
