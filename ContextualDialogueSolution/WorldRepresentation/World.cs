using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using System;
using System.Collections;
using System.Collections.Generic;


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

        public PhysicalEntity findByProperNoun(String NounToFind)
        {
            //TODO commmmee onn. this is a linear search
            foreach (PhysicalEntity i in worldArray)
            {
                if (i.hasProperNoun)
                {
                    if (i.properNoun.CompareTo(NounToFind) == 0)
                        return i;
                }
            }
            //else ID not found
            return null;
        }

        
         /*returns the most specific location common to both entities
         * first find lowest common ancestor using LCA algorithm: 
         * first save the paths from the root node to each entity. 
         * second compare paths node by node, starting at root
         * when the paths are no longer the same, they have diverged and lowest common ancestor was reached        
         * 
         * now having found the LCA, return the path from there to entityB
         */
        public PhysicalEntity[] getLocationARelativeToB(PhysicalEntity entityA, PhysicalEntity entityB)
        {
            ArrayList pathAtoRoot = new ArrayList();
            ArrayList pathBtoRoot = new ArrayList();
            pathAtoRoot.Add(entityA);
            pathBtoRoot.Add(entityB);

            while (((PhysicalEntity)pathAtoRoot[pathAtoRoot.Count -1]).hasSpatialParent())
            {
                pathAtoRoot.Add(((PhysicalEntity)pathAtoRoot[pathAtoRoot.Count - 1]).getSpatialparent().adult );
            }

            while (((PhysicalEntity)pathBtoRoot[pathBtoRoot.Count - 1]).hasSpatialParent())
            {
                pathBtoRoot.Add(((PhysicalEntity)pathBtoRoot[pathBtoRoot.Count - 1]).getSpatialparent().adult);
            }

            //while the nodes are the same, keep popping off the head node
            //NOTE: will break if they dont share common ancestor
            while ((pathAtoRoot.Count != 0 && pathBtoRoot.Count != 0) && ((PhysicalEntity)pathAtoRoot[pathAtoRoot.Count - 1]).Equals(((PhysicalEntity)pathBtoRoot[pathBtoRoot.Count - 1])))
            {
                pathAtoRoot.RemoveAt(pathAtoRoot.Count - 1);
                pathBtoRoot.RemoveAt(pathBtoRoot.Count - 1);
            }
            //return B, unles A is 0
            if (pathAtoRoot.Count == 0)
                return (PhysicalEntity[])pathBtoRoot.ToArray(typeof(PhysicalEntity));
            else
                return (PhysicalEntity[]) pathBtoRoot.ToArray(typeof(PhysicalEntity));
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