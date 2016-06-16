using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;

namespace WorldManagerNamespace
{
    [Serializable]
    public class Apple : Food
    {
        public Apple()
        {
            this.descriptors.Add(new Descriptor(adjective.ripe, 1));
            this.countable = true;
        }
    }
}
