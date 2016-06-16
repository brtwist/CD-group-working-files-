using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;

namespace WorldManagerNamespace
{
    [Serializable]
    public class Food : PhysicalEntity
    {
        public Food()
        {
            this.descriptors.Add(new Descriptor(adjective.good, verb.taste,1));
            this.descriptors.Add(new Descriptor(adjective.healthy, 1));
            this.descriptors.Add(new Descriptor(adjective.nice, verb.taste, 1));
            this.descriptors.Add(new Descriptor(adjective.tasty, 1));
            this.descriptors.Add(new Descriptor(adjective.nice, verb.smell, 1));
            this.descriptors.Add(new Descriptor(adjective.good, verb.look, 1));

            this.countable = false;
        }
    }
}
