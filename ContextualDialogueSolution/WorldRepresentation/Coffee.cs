using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;

namespace WorldManagerNamespace
{
    [Serializable]
    public class Coffee : Drink
    {
        public Coffee()
        {
            this.descriptors.Add(new Descriptor(adjective.good, verb.taste, 1));
            this.descriptors.Add(new Descriptor(adjective.nice, verb.taste, 1));
            this.descriptors.Add(new Descriptor(adjective.strong, 1));
            this.descriptors.Add(new Descriptor(adjective.hot, 1));
            this.descriptors.Add(new Descriptor(adjective.good, verb.smell, 1));

            this.countable = false;
        }
    }
}
