using System;

namespace ContextualDialogue.WorldManager
{
    [Serializable]
    public class Range
    {
        //public Adjective adjective;
        public String verb;//e.g. is good. tastes good. sounds good.

        private static readonly Double HARDMAX = 2.0;
        private static readonly Double HARDMIN = 0.0;

        //these are the thresholds where a person would start to comment on that particular attribute. where it becomes noteworthy
        private Double noteworthyThresholdUpper { get; }
        private Double noteworthyThresholdLower { get; }


        public Range(Double noteworthyThresholdLower, Double noteworthyThresholdUpper)
        {
            this.noteworthyThresholdLower = noteworthyThresholdLower;
            this.noteworthyThresholdUpper = noteworthyThresholdUpper;
        }

        /*CONSTRUCTOR CHAINING*/
        //no values given, defaults substituted
        public Range() : this(0.5, 1.5) { }

    }
}
