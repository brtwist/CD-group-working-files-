using ContextualDialogue.WorldManager.TypeDefinitionDictionary;
 /*instances of this class are placed on the outputQueue for use by the external program*/

namespace ContextualDialogue.DialogueGenerator
{
    public class Turn
    {
        private PhysicalEntity_Agent participant;

        //property masks error output from sight
        public string _speaker
        {
            get
            {
                if (participant != null)
                    return participant.properNoun;
                else
                    return "Error ";
             }
        } 

        public string utterance;

        public Turn (PhysicalEntity_Agent p, string s)
        {
            participant = p;
            utterance = s;
        }

        public Turn(string error)
        {
            utterance = error;
        }
    }
}
