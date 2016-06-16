using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;

namespace WorldManagerNamespace
{
    [Serializable]
    abstract public class Drink : PhysicalEntity
    {
        public Drink()
        {
            this.descriptors.Add(new Descriptor(adjective.good, verb.taste, 1));
        }
    }
}
