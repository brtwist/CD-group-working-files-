using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.DialogueGenerator.LinguisticDictionary
{
    class DictionaryEntry
    {
            public readonly string value;
            public Double salience;
            //public float politeness
            //public quote businessness

        public DictionaryEntry(String value)
        {
            this.value = value;
            salience = 1.0;
        }
    }
}
