using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public class FuzzyModule
    {
        // Properties
        private Dictionary<String, FuzzyVariable> _varMap { get; set; }
        private List<FuzzyRule> _rules { get; set; }
        public const int NumSamples = 15;

        // Constructor 
        public FuzzyModule()
        {
            _varMap = new Dictionary<string, FuzzyVariable>();
            _rules = new List<FuzzyRule>();
        }

        // Functions
        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            _rules.Add(new FuzzyRule(antecedent, consequence));
        }

        public FuzzyVariable CreateFLV(string varName)
        {
            _varMap.Add(varName, new FuzzyVariable());
            return _varMap[varName];
        }

        public void Fuzzify(string nameOfFLV, float val)
        {
            FuzzyVariable result;
            if (_varMap.TryGetValue(nameOfFLV, out result))
            {
                result.Fuzzify();
            } 
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public float DeFuzzify(string nameOfFLV)
        {
            FuzzyVariable result;
            if (_varMap.TryGetValue(nameOfFLV, out result))
            {
                SetConfidencesOfConsequentsToZero();

                foreach (FuzzyRule rule in _rules)
                {
                    rule.Calculate();
                }

                return result.DefuzzifyMaxAv();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

        }

        private void SetConfidencesOfConsequentsToZero()
        {
            foreach(FuzzyRule rule in _rules)
            {
                rule.SetConfidencesOfConsequentsToZero();
            }
        }

    }
}
