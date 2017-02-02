using System;
using ContextualDialogue.WorldManager.TypeDefinitionDictionary;

namespace ContextualDialogue.DialogueGenerator.LinguisticDictionary
{
    /*This class encapsulates all the information for every permutation of a verbal phrase
     * Verb can be intransitive, monotransitive or ditransitive
     * monotransitive and ditransitive fields are named using the object_one_ and object_two_ naming convention
     * NOTE: currently only supports PhysicalEntities as objects, not Types, and not strings
     * 
     * NOTE: there are three variables for adverbs: adverb, object_one_adverb and object_two_adverb. 
     * it is up to the user not to misuse these fields
     * i.e. if you assign to all of them you will get weird sentences
     * e.g. John sat quickly down on the chair quickly for Mary quickly
     * 
     * NOTE: currently no nesting of verbal phrases inside verbal phrases
     * e.g. (he sat down to (read to mary)) currently impossible
     * 
     * Explanation of all fields in the form of an example sentence
     * "John quickly threw down the bucket to Mary"
     * <Subject> <adverb> <Verb> <intransitive_preposition> <object_one> <object_two_preposition> <object_two>
     */
    public class Verb
    {
        private Type subject_type;//can be a string, a typedefinition, a physical entity, or a nested verb object
        public object subject;//optional: object which executes the verb

        private String verb;//alternative tenses etc. can be looked up with wordnet
        public String _verbInfinitive
        {
            get { return verb;  }
        }
        public String _verbThirdPersonSingular
        {
            get { return verb + 's'; }//TODO look up in wordnet
        }

        public String adverb;//John sits quickly
        public String intransitive_preposition;//e.g. john sits down

        private Type object_one_type;//can be a string, a typedefinition, a physical entity, or a nested verb object
        public object object_one;//for monotransitive verbs
        public String object_one_preposition;//e.g. can you get object DOWN from the shelf. did you hand your documents IN
        public String object_one_adverb;

        private Type object_two_type;//can be a string, a typedefinition, a physical entity, or a nested verb object
        public object object_two;//for ditransitive verbs
        public String object_two_preposition;//only to/for e.g. John gave the cup TO Mary. Mary took the cup FOR John
        public String object_two_adverb;

        //intransitive constructor. 'John sits'
        public Verb(object subject, String verb)
        {
            subject_type = subject.GetType();
            this.subject = subject;
            this.verb = verb;
        }

        //intransitive constructor. 'John sits down'
        public Verb(object subject, String verb, String intransitive_preposition, String adverb)
        {
            subject_type = subject.GetType();
            this.subject = subject;
            this.verb = verb;
            this.intransitive_preposition = intransitive_preposition;
            this.adverb = adverb;
        }

        //intransitive constructor. 'John sits down quickly'
        public Verb(object subject, String verb, String adverb)
        {
            subject_type = subject.GetType();
            this.subject = subject;
            this.verb = verb;
            this.adverb = adverb;
        }

        //e.g. (sits) on the chair
        public void setMonoTransitive(String object_one_preposition, object object_one)
        {
            object_one_type = object_one.GetType();
            this.object_one_preposition = object_one_preposition;
            this.object_one = object_one;
        }

        //e.g. (sits) on the chair quickly
        public void setMonoTransitive(String object_one_preposition, object object_one, String object_one_adverb)
        {
            object_one_type = object_one.GetType();
            this.object_one_preposition = object_one_preposition;
            this.object_one = object_one;
            this.object_one_adverb = object_one_adverb;
        }

        //e.g. (john gets) down the book from the high shelf for Mary
        //note: any of these could be left blank, i.e. "". E.g. in the sentence 'can you read me a book' the preposition of the second object is left blank
        public void setDiTransitive(String object_one_preposition, object object_one, String object_two_preposition, object object_two)
        {
            object_one_type = object_one.GetType();
            object_two_type = object_two.GetType();

            this.object_one_preposition = object_one_preposition;
            this.object_one = object_one;

            this.object_two_preposition = object_two_preposition;
            this.object_two = object_two;
        }

        //e.g. (john gets) down the book from the high shelf for Mary quickly
        public void setDiTransitive(String object_one_preposition, PhysicalEntity object_one, String object_two_preposition, PhysicalEntity object_two, String object_two_adverb)
        {
            this.object_one_preposition = object_one_preposition;
            this.object_one = object_one;

            this.object_two_preposition = object_two_preposition;
            this.object_two = object_two;

            this.object_two_adverb = object_two_adverb;
        }
    }
}
