﻿using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.WorldManager
{
    //contains a relationship to another object within the knowable world
    //that relationship consists of two objects plus a prepositional relationship between them.
    [Serializable]
    public class SpatialLocation
    {
        public PhysicalEntity child;
        public Preposition preposition;
        public PhysicalEntity adult;
        //TODO add optional second preposition, i.e. make this bidirectional

        public SpatialLocation(PhysicalEntity c, Preposition p1, PhysicalEntity a)
        {
            child = c;
            preposition = p1;
            adult = a;
        }
    }
}
