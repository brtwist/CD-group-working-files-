using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;


/*contains all the knowledge of the world, in searchable form*/
namespace ContextualDialogue.WorldManager
{
    [Serializable]
    public class World
    {
        private Random r;

        private TypeDictionary typeDictionary;
        private ArrayList worldArray;
        //public ObjectDictionary objectDictionary;
        
        //private 
        //list sorted by preposition

        public String worldName;

        public World(String n, TypeDictionary typeDictionary)
        {
            worldName = n;
            this.typeDictionary = typeDictionary;
            worldArray = new ArrayList();

            //KnowableDictionary
            //where should events be stored. emotions. future. past. expected
            r = new Random();
        }

        public int add(PhysicalEntity k)//TODO definitely need a better ID function. e.g. once that actually gives unique id codes and one that always gives the same object the same ID
        {
            worldArray.Add(k);
            k.ID = k.GetHashCode()/*k.nodeName + r.NextDouble().ToString()*/;
            return k.ID;//now the game has a copy of the ID used to identify this object
        }
        
        /*SEARCH METHODS*/

        public PhysicalEntity findByID(int IDtoFind)
        {
            //TODO commmmee onnnn. this is a linear search
            foreach (PhysicalEntity i in worldArray)
            {
                if (i.ID == IDtoFind)
                    return i;
            }
            //else ID not found
            return null;
        }

        public PhysicalEntity getRandomEntity()
        {
            try
            {
                return (PhysicalEntity)worldArray[r.Next(worldArray.Count)];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new FileLoadException("Error: maybe you tried searching in an empty world? check that world data is all loaded before any conversations are created");
            }
        }

        
        /*GET_ALL FUNCTIONS*/
        //used as a kind of toString for displaying all world objects

        //returns every physical entity
        public Object[] getAllPhysicalEntities()
        {
            return worldArray.ToArray();
        }

        //returns an array of the tostring of every physical entity
        public String[] getAllphysicalEntitiesToString()
        {
            object[] objectarray = worldArray.ToArray();
            String[] stringarray = new String[objectarray.Length];

            for (int i = objectarray.Length - 1; i >= 0; i--)
            {
                PhysicalEntity pe = (PhysicalEntity)objectarray[i];
                stringarray[i] = pe.ToString();
            }
            return stringarray;
        }


        /*SEARCH FUNCTIONS*/
        //get all by type
        //search by preposition + child
        //search by preposition + parent
        //search by adjectives
    }
}