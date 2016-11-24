using System;

namespace ContextualDialogue.DialogueGenerator
{
    public class QUDitem
    {
        public String initiatingSpeaker;
        public String respondingSpeaker;
        public Object subject;//can be an int ID, an entity, or a type
        //verb

        public enum ExchangeTypeEnum { @default, where }
        public ExchangeTypeEnum exchangeType;
        //salience
        //lengthiness of exchange on this topic

        public QUDitem() : this(ExchangeTypeEnum.@default) { }

        public QUDitem(ExchangeTypeEnum type)
        {
            exchangeType = type;
        }

        //in case the speakers need to be swapped
        public void swapSpeakers()
        {
            String temp = initiatingSpeaker;
            initiatingSpeaker = respondingSpeaker;
            respondingSpeaker = temp;
        }
    }
}
