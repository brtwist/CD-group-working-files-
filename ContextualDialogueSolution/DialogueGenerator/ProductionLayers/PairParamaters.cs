using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator
{
    public class PairParamaters
    {
        public PhysicalEntity_Agent initiatingSpeaker;
        public PhysicalEntity_Agent respondingSpeaker;
        public int politeness;

        //constructor chain with default politeness value
        public PairParamaters(PhysicalEntity_Agent initiatingSpeaker, PhysicalEntity_Agent respondingSpeaker)
        : this(initiatingSpeaker, respondingSpeaker, 6) { } //default politeness 6

        public PairParamaters(PhysicalEntity_Agent initiatingSpeaker, PhysicalEntity_Agent respondingSpeaker, int politeness)
        {
            this.initiatingSpeaker = initiatingSpeaker;
            this.respondingSpeaker = respondingSpeaker;

        }

        public PairParamaters clone()
        {
            return new PairParamaters(initiatingSpeaker, respondingSpeaker, politeness);
        }

        //in case speakers need to be swapped. cant be done on a reference so a deep copy needs to be made
        public PairParamaters cloneAndSwapSpeakers()
        {
            return new PairParamaters(respondingSpeaker, initiatingSpeaker, politeness);
        }
    }
}
