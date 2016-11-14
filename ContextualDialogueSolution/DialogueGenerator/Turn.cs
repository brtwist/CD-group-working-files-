/*instances of this class are placed on the outputQueue for use by the external program*/

namespace ContextualDialogue.DialogueGenerator
{
    public class Turn
    {
        public object participant;
        public string utterance;

        public Turn(object p, string s)
        {
            participant = p;
            utterance = s;
        }
    }
}
