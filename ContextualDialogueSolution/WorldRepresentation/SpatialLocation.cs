using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;

namespace WorldManagerNamespace
{
    //contains a relationship to another object within the knowable world
    //that relationship consists of two objects plus a prepositional relationship between them.
    [Serializable]
    public class SpatialLocation
    {
        public Knowable child;
        public Preposition preposition;
        public PhysicalEntity adult;

        public SpatialLocation(Knowable c, Preposition p, PhysicalEntity a)
        {
            child = c;
            preposition = p;
            adult = a;
        }
    }
}
