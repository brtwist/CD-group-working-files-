using System;

namespace ContextualDialogue.DialogueGenerator.LinguisticDictionary
{
    partial class LinguisticDictionary
    {
        public static String getAorAN(String input)
        {
            //assumes adjective always comes before noun
            Char firstLetter = input[0];

            bool isVowel = "aeiou".IndexOf(firstLetter) >= 0;

            if (isVowel)
                return "an";
            else
                return "a";
        }

        static String getRegularPlural(String input)
        {
            //TODO (currently plurals must always be manualy speified
            return input + "<error: pluraliser is only stub function>";
        }
    }
}
