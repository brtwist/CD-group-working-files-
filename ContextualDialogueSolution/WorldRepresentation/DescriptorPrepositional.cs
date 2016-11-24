using EnumNamespace;
using System;
using System.Collections.Generic;

namespace ContextualDialogue.WorldManager
{
    /*e.g. "Tadpoles have tails" "Tadpoles have tails on their bodies"*/

    [Serializable]
    public class DescriptorPrepositional// : Descriptor
    {
        public Boolean traitIsPlural;//Unicorns have a horn. Rhinos have horns

        public Preposition preposition;
        public String location;

        public List<Descriptor> traitAdjective;//'unicorns have a golden horn'

        //empty constructor
        public DescriptorPrepositional()
        {
            //WARNING if you use this empty constructor you MUST set at minimum the verb and trait values manually
            //default values
            traitIsPlural = false;
        }
        /*
        //constructor chain overload
        public DescriptorPrepositional(String trait, Preposition preposition, String location) : this(trait, true, "has", preposition, location, Tense.present) { }//auto fill verb and tense
        public DescriptorPrepositional(String trait, String verb, Preposition preposition, String location) : this(trait, true, verb, preposition, location, Tense.present) { }//autofill tense

        //actual constructor
        public DescriptorPrepositional (String trait, Boolean value, String verb, Preposition preposition, String location, Tense tense) : base(trait, verb, value, tense)
        {
            this.preposition = preposition;
            this.location = location;
            traitIsPlural = false;
        }
        */
    }
}
