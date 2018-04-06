using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public abstract class FuzzySet
    {
        // Degree of membership
        private float _dom;

        public float DOM
        {
            get { return _dom; }
            set
            {
                if (value <= 1 && value >= 0)
                {
                    _dom = value;
                }
                else
                {
                    throw new Exception("Invalid value");
                }
            }
        }

        //this is the maximum of the set's membership function. For instamce, if
        //the set is triangular then this will be the peak point of the triangular.
        //if the set has a plateau then this value will be the mid point of the 
        //plateau. This value is set in the constructor to avoid run-time
        //calculation of mid-point values.
        public float RepresentativeValue { get; set; }

        // Constructor
        public FuzzySet(float RepVal)
        {
            RepresentativeValue = RepVal;
            _dom = 0.0f;
        }

        // Functions

        //return the degree of membership in this set of the given value. NOTE,
        //this does not set m_dDOM to the DOM of the value passed as the parameter.
        //This is because the centroid defuzzification method also uses this method
        //to determine the DOMs of the values it uses as its sample points.
        public virtual float CalculateDOM(float val) { return 0; }

        //if this fuzzy set is part of a consequent FLV, and it is fired by a rule 
        //then this method sets the DOM (in this context, the DOM represents a
        //confidence level)to the maximum of the parameter value or the set's 
        //existing m_dDOM value
        public void ORwithDOM(float val) { if (val > DOM) DOM = val; }

        public void ClearDom() { DOM = 0.0f; }

    }
}
