using System;

namespace ContextualDialogue.WorldManager.TypeDefinitionDictionary.DataTypes
{
    [Serializable]
    public class Opinion
    {
        PhysicalEntity_Agent opiner;

        private PhysicalEntity subjectEntity;
        private TypeDefinition subjectTypeDefinition;

        public object _subject
        {
            get
            {
                if (subjectTypeDefinition != null)
                    return subjectTypeDefinition;
                else
                    return subjectEntity;
            }
        }
        
        //reason (trait, e.g. i love fast things. i love it because its so fast)
        public Descriptor reason;
        //comparative (e.g. he's the fastest horse ive ever ridden, its the best cake in town)

        //i love all animals, except spiders. perhaps this would be a nested opinion.

        public Boolean likes;

        public Opinion(PhysicalEntity_Agent opiner, PhysicalEntity subject)
        {
            this.opiner = opiner;
            subjectEntity = subject;   
        }

        public Opinion(PhysicalEntity_Agent opiner, TypeDefinition subject)
        {
            this.opiner = opiner;
            subjectTypeDefinition = subject;
        }

    }
}
