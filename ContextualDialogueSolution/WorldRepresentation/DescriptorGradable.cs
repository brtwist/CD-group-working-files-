using System;

namespace ContextualDialogue.WorldManager
{
    class DescriptorGradable
    {
        private Double grade;
        private String lookup;
        Range range;

        public DescriptorGradable()
        {
            grade = 1.0;
            range = new Range();
            //must always specify a lookup, otherwise stuff will break. TODO maybe the default could be 'good' or something
        }
    }
}
