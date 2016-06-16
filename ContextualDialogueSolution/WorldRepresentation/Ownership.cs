using System;
using System.Collections.Generic;
using System.Text;

/*
 * this class represents the relationship of ownership.
 * the owner owns the owned.
 */

namespace WorldManagerNamespace
{
    class Ownership
    {
        public PhysicalEntity owner;
        public PhysicalEntity owned;

        public Ownership(PhysicalEntity p1, PhysicalEntity p2)
        {
            owner = p1;
            owned = p2;
        }
    }
}
