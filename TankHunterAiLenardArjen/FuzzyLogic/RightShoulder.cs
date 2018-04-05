using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public class RightShoulder : FuzzySet
    {
        // Properties
        private float _peakPoint { get; set; }
        private float _leftOffset { get; set; }
        private float _rightOffset { get; set; }

        public RightShoulder(float peak, float leftOffset, float rightOffset) : base(((peak + rightOffset) + peak) / 2)
        {
            _peakPoint = peak;
            _leftOffset = leftOffset;
            _rightOffset = rightOffset;
        }

        public override float CalculateDOM(float val)
        {
            //test for the case where the left or right offsets are zero
            //(to prevent divide by zero errors below)
            if (_rightOffset.Equals( 0.0f) && _peakPoint.Equals( val) ||
                 _leftOffset.Equals(0.0f) && _peakPoint.Equals(val))
            { 
                return 1.0f;
            }

            //find DOM if left of center
            else if ((val <= _peakPoint) && (val > (_peakPoint - _leftOffset)))
            {
                float grad = 1.0f / _leftOffset;

                return grad * (val - (_peakPoint - _leftOffset));
            }
            //find DOM if right of center and less than center + right offset
            else if ((val > _peakPoint) && (val <= _peakPoint + _rightOffset))
            {
                return 1.0f;
            }

            else
            {
                return 0;
            }
        }
    }
}
