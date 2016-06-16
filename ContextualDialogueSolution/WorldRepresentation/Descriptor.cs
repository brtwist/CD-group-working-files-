using System;
using System.Collections.Generic;
using System.Text;
using EnumNamespace;

namespace WorldManagerNamespace
{
    [Serializable]
    public class Descriptor
    {
        public adjective adjective;
        public verb verb;
        public int value;
        public Tense tense;

        public Descriptor(adjective a, verb b, int v)
        {
            adjective = a;
            verb = b;
            value = v;
            tense = Tense.present;
        }

        //no verb means default "is" "was" etc. will be subst.
        public Descriptor(adjective a, int v)
        {
            adjective = a;
            verb = verb.none;
            value = v;
        }

    }
}
