using System;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    internal class FuzzyRule
    {
        private FuzzyTerm antecedent;
        private FuzzyTerm consequence;

        public FuzzyRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            this.antecedent = antecedent;
            this.consequence = consequence;
        }

        // Functions
        public void SetConfidencesOfConsequentsToZero() { consequence.ClearnDom(); }

        internal void Calculate()
        {
            throw new NotImplementedException();
        }
    }
}