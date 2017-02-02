using System;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator
{
    public class Topic
    {
        public PhysicalEntity_Agent initiatingSpeaker;
        public PhysicalEntity_Agent respondingSpeaker;

        public Object subject;//can be an int ID, an entity, or a type
        public String verb;

        public int politeness;//int between 0 and 10

        public enum ExchangeTypeEnum { @default, where }
        public ExchangeTypeEnum exchangeType;
        //salience
        //lengthiness of exchange on this topic

        public Topic() : this(ExchangeTypeEnum.@default) { }

        public Topic(ExchangeTypeEnum type)
        {
            exchangeType = type;
            politeness = 6;
        }

        //in case the speakers need to be swapped
        public void swapSpeakers()
        {
            PhysicalEntity_Agent temp = initiatingSpeaker;
            initiatingSpeaker = respondingSpeaker;
            respondingSpeaker = temp;
        }
    }
}
