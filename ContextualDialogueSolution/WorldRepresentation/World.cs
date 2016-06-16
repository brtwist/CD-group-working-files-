using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

/*contains all the knowledge of the world, in searchable form*/
namespace WorldManagerNamespace
{
    [Serializable]
    public class World
    {
        private Random r;
        //unsorted list
        private List<Knowable> worldArray;
        //list sorted by preposition

        public String worldName;

        public World(String n)
        {
            worldName = n;
            worldArray = new List<Knowable>();
            r = new Random();
        }

        public void add(Knowable k)
        {
            worldArray.Add(k);
            k.ID = k.getNoun() + r.NextDouble().ToString();
        }

        public List<Knowable> findAll(Type t)
        {
            return worldArray.FindAll(
            delegate (Knowable k)
            {
                return k.GetType() == t;
            }
            );
        }

        public List<Knowable> find(String IDToFind)
        {
            return worldArray.FindAll(
            delegate (Knowable k)
            {
                return k.ID == IDToFind;
            }
            );
        }

        public Knowable[] getAll()
        {
            return worldArray.ToArray();
        }

        public Knowable getRandom()
        {
            
            return worldArray[r.Next(worldArray.Count)];
        }



        //public String toString()
        //{
        //return string of whole database
        //}

        //search by preposition + child
        //search by preposition + parent
        //search by adjectives
    }
}