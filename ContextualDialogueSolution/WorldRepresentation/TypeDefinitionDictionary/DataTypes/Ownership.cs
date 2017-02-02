/*
 * this class represents the relationship of ownership.
 * the owner owns the owned.
 */

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes
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
