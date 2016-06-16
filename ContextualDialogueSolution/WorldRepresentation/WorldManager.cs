using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WorldManagerNamespace
{
    public class WorldManager
    {
        private String FILE_NAME = "C:\\Users\\benjamint\\Documents\\visual studio 2015\\Projects\\ContextualDialogueSolution\\savedworld.world";
        public World world;

        public void newWorld(String worldName)
        {
            world = new World(worldName);
            save();//save newly created world
        }

        public WorldManager()
        {
            //load();
        }

        public void close()
        {
            save();
        }

        public String getWorldName()
        {
            if (world != null)
                return world.worldName;
            else return "none";
        }

        
        //save world
        public void save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, world);
            stream.Close();
        }

        //load world
        public void load()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FILE_NAME,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);

            world = (World) formatter.Deserialize(stream);
            stream.Close();

        }

        public Knowable[] getAll()
        {
            return world.getAll();
        }

    }
}
