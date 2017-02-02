using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
using System;
using System.Collections;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes;


/*contains all the knowledge of the world, in searchable form*/
namespace ContextualDialogue.WorldManager
{
    [Serializable]
    public class World
    {
        private Random r;

        private TypeDictionary typeDictionary;
        private ArrayList worldArray;
        private ArrayList agentArray;
        //public ObjectDictionary objectDictionary;

        //private 
        //list sorted by preposition

        public String worldName;

        public World(String n, TypeDictionary typeDictionary)
        {
            worldName = n;
            this.typeDictionary = typeDictionary;
            worldArray = new ArrayList();

            agentArray = new ArrayList();

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

        public int add(PhysicalEntity_Agent k)//TODO definitely need a better ID function. e.g. once that actually gives unique id codes and one that always gives the same object the same ID
        {
            worldArray.Add(k);
            agentArray.Add(k);
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

        public PhysicalEntity_Agent findAgentByName(String NameToFind)
        {
            //TODO commmmee onn. this is a linear search
            foreach (PhysicalEntity_Agent i in agentArray)
            {

                    if (i.properNoun.CompareTo(NameToFind) == 0)
                        return i;
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

            //overloadded to not always need an exact person
        //public Opinion getOpinion(String personsName, object thingToOpine)
        //{
        //    //WARNING findAgentByName returns null if not found. would result in breaking
        //    return getOpinion(findAgentByName(personsName), thingToOpine);
        //}
        public Opinion getOpinion(PhysicalEntity_Agent person, object thingToOpine)
        {
            Opinion result;
            //first see if this agent already has an opinion on this topic (or on a related topic, e.g. a sibling, parent or child)
            //if yes, simply return it
            result = person.tryGetOpinion(thingToOpine);

            if (result != null)
                return result;


            /*else, make one*/
            else
            {
                //make a new opinion object
                if (thingToOpine.GetType() == typeof(PhysicalEntity))
                    result = new Opinion(person, (PhysicalEntity)thingToOpine);
                else if ((thingToOpine.GetType() == typeof(TypeDefinition)))
                    result = new Opinion(person, (TypeDefinition)thingToOpine);
                else
                    //TODO break

                //now we have a blank opinion object, we need to populate the fields

                /*TODO 
                 * algorithm for deciding whether we like or dislike something
                 * check if we already like the parent
                 * check if we already like one of the traits of this object in another object.
                 * 
                 * check probability of liking it or disliking it, also do the same for the parent
                 * check if it has a common trait/reason with any existing opinions. e.g. i dont like snakes because they're slimy, fish are also slimy
                 * if you make a contradictory opinion, add it as an exception to the existing opinion
                 * 
                 * 
                 * reason: a trait, liking/disliking the parent/liking/disliking a sibling with the same trait
                 * comparator: it's the cudliest animal, its the best cake in town, it's my favorite horse
                 * (some kind of fitness function that perfers some reasons and comparisons over others, e.g. not enough evidence)
                 */

                result.likes = true;

                //now create a reason for the opinion
                Descriptor myReason = new Descriptor();
                myReason.verb = "is";
                myReason.trait = "tasty";
                myReason.value = true;

                //add the reason to the opinion, and add the opinion to the person
                result.reason = myReason;

                person.addOpinion(result);

                return result;
            }
        }
    }
}