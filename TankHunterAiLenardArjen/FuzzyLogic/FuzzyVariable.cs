using System;
using System.Collections.Generic;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public class FuzzyVariable
    {
        // Properties
        private float _minRange { get; set; }
        private float _maxRange { get; set; }
        private Dictionary<String, FuzzySet> _memberSets { get; set; }

        // Constructor 
        public FuzzyVariable()
        {
            _minRange = 0.0f;
            _maxRange = 0.0f;
            _memberSets = new Dictionary<string, FuzzySet>();
        }

        //--------------------------- Fuzzify -----------------------------------------
        //
        //  takes a crisp value and calculates its degree of membership for each set
        //  in the variable.
        //-----------------------------------------------------------------------------
        internal void Fuzzify(float val)
        {
            if ((val >= _minRange) && (val <= _maxRange))
            {
                foreach (KeyValuePair<String, FuzzySet> set in _memberSets)
                {
                    set.Value.DOM = set.Value.CalculateDOM(val);
                }
            }
            else
            {
                foreach (KeyValuePair<String, FuzzySet> set in _memberSets)
                {
                    set.Value.DOM = set.Value.CalculateDOM(_maxRange);
                }
            }
        }

        //--------------------------- DeFuzzifyMaxAv ----------------------------------
        //
        // defuzzifies the value by averaging the maxima of the sets that have fired
        //
        // OUTPUT = sum (maxima * DOM) / sum (DOMs) 
        //-----------------------------------------------------------------------------
        public float DefuzzifyMaxAv()
        {
            float bottom = 0.0f;
            float top = 0.0f;

            foreach (KeyValuePair<String, FuzzySet> set in _memberSets)
            {
                bottom += set.Value.DOM;
                top += set.Value.RepresentativeValue * set.Value.DOM;
            }

            // Make sure bottoms is not 0
            if (bottom.Equals(0)) return 0.0f;

            return top / bottom;
        }

        //------------------------- AddTriangularSet ----------------------------------
        //
        //  adds a triangular shaped fuzzy set to the variable
        //-----------------------------------------------------------------------------
        public FzSet AddTriangularSet(string name, float minBound, float peak, float maxBound)
        {
            _memberSets.Add(name, new Triangle(peak, peak - minBound, maxBound - peak));

            // Adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_memberSets[name]);
        }

        //--------------------------- AddLeftShoulder ---------------------------------
        //
        //  adds a left shoulder type set
        //-----------------------------------------------------------------------------
        public FzSet AddLeftShoulderSet(string name, float minBound, float peak, float maxBound)
        {
            _memberSets.Add(name, new LeftShoulder(peak, peak - minBound, maxBound - peak));

            // Adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_memberSets[name]);
        }

        //--------------------------- AddRightShoulder ---------------------------------
        //
        //  adds a left shoulder type set
        //-----------------------------------------------------------------------------
        public FzSet AddRightShoulderSet(string name, float minBound, float peak, float maxBound)
        {
            _memberSets.Add(name, new RightShoulder(peak, peak - minBound, maxBound - peak));

            // Adjust range if necessary
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet (_memberSets[name]);
        }

        //---------------------------- AdjustRangeToFit -------------------------------
        //
        //  this method is called with the upper and lower bound of a set each time a
        //  new set is added to adjust the upper and lower range values accordingly
        //-----------------------------------------------------------------------------
        public void AdjustRangeToFit(float minBound, float maxBound)
        {
            if (minBound < _minRange) _minRange = minBound;
            if (maxBound > _maxRange) _maxRange = maxBound;
        }
    }
}