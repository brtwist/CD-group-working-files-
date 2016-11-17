using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.DialogueGenerator
{
    public class QUDitem
    {
        public String initiatingSpeaker;
        public String respondingSpeaker;
        public int subjectID;
        //verb

        public enum ExchangeTypeEnum { @default }
        public ExchangeTypeEnum exchangeType;
        //salience
        //lengthiness of exchange on this topic

            public QUDitem()
        {

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
