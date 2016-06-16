using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;
using System.Collections;
using EnumNamespace;

namespace WorldManagerNamespace
{
    [Serializable]
    public class PhysicalEntity : Knowable //extends knowable
    {

        public typeOfStuff TypeOfObject;

        public SpatialLocation spatialParent;
        public List<SpatialLocation> spatialChildren;

        //a list of traits (pairs of verbs and attributes
        public List<Descriptor> descriptors;
        public String distinguishingAdjective = ""; //e.g. "the RED apple" red is its distinguishing feature

        //public Ownership Ownerships;

        public PhysicalEntity()
        {
            descriptors = new List<Descriptor>();
            spatialChildren = new List<SpatialLocation>();
        }

        public void setSpatialParent(PhysicalEntity parentObject, Preposition preposition)
        {
            //add the preposition here
            spatialParent = new SpatialLocation(this, preposition, parentObject);
            //add the preposition in the parent
            parentObject.addSpatialChild(spatialParent);
        }

        public void addSpatialChild(SpatialLocation sl)
        {
            //if it dosnt already exist, add it
            if (!spatialChildren.Contains(sl))
                spatialChildren.Add(sl);
        }
        
        //remove exact match descriptor
        public void removeDescriptor(Descriptor n)
        {
             int result = descriptors.FindIndex(
            delegate (Descriptor o)
            {
                return (o.verb == n.verb && o.adjective == n.adjective);
            }
            );

            if (result != -1)
                descriptors.RemoveAt(result);
        }

    }
}
