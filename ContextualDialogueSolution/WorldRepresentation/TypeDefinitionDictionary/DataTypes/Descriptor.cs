using EnumNamespace;
using System;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes
{
    [Serializable]
    public class Descriptor
    {
        public String trait;
        public String adjective;//optional e.g. ducks have FLAT bills

        public String verb;//in abilities it defaults to can, in attributes it defaults to is/has
        public String adverb;//optional e.g. cheetahs can run FAST

        public Preposition preposition;

        public bool value;//the trait can also be negated. 'is NOT spicy'
        public double probability;//goes from 0 to 1 e.g. curry is usually spicy. (75% of the time). unicorns always have horns (100%) pokemon are occasionally shiny (0.1%)




        //optional:
        public Tense tense;//can use for this for e.g. 'frog was a tadpole' 'coffee was hot'

        //empty constructor
        public Descriptor()
        {
            //you'd better be sure and set at minimum a trait name and verb when creating this object
            //else you'll break stuff
            this.value = true;
            this.tense = Tense.presentContinuous;
            probability = 1.0;//1 means always
        }

        //this constructor isnt used, actually just creates empty class and fills in params one at a time

        //public Descriptor(String trait, String verb, bool value, Tense tense)
        //{
        //    this.trait = trait;
        //    this.verb = verb;
        //    this.value = value;
        //    this.tense = tense;
        //}

        ///*CONSTRUCTOR CHAINING*/
        //public Descriptor(String trait) : this(trait, true) { }
        ////no verb means default "is" "was" etc. will be subst.
        //public Descriptor(String trait, bool value) : this(trait, "is" , value, Tense.presentContinuous) { }
        ////no tense and presentContinuous will be subst
        //public Descriptor(String trait, String verb, bool value) : this(trait, verb, value, Tense.presentContinuous) { }


    }
}
