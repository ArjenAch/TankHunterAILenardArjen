using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public class Triangle : FuzzySet
    {
        private float _peakPoint { get; set; }
        private float _leftOffset { get; set; }
        private float _rightOffset { get; set; }

        public Triangle(float mid, float left, float right) : base(mid)
        {
            _peakPoint = mid;
            _leftOffset = left;
            _rightOffset = right;
        }

        public override float CalculateDOM(float val)
        {
            //test for the case where the triangle's left or right offsets are zero
            //(to prevent divide by zero errors below)
            if ((_rightOffset.Equals(0.0)) && (_peakPoint.Equals(val)) ||
                (_leftOffset.Equals(0.0)) && (_peakPoint.Equals(val)))
            {
                return 1.0f;
            }

            //find DOM if left of center
            if ((val <= _peakPoint) && (val >= (_peakPoint - _leftOffset)))
            {
                float grad = 1.0f / _leftOffset;

                return grad * (val - (_peakPoint - _leftOffset));
            }

            //find DOM if right of center
            else if ((val > _peakPoint) && (val < (_peakPoint + _rightOffset)))
            {
                float grad = 1.0f / -_rightOffset;

                return grad * (val - _peakPoint) + 1.0f;
            }

            //out of range of this FLV, return zero
            else
            {
                return 0.0f;
            }
        }
    }
}
