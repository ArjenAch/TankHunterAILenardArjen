using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public class FzSet : FuzzyTerm, ICloneable
    {
        //a reference to the fuzzy set this proxy represents
        private FuzzySet _set { get; set; }

        // Constructor
        public FzSet(FuzzySet Set)
        {
            _set = Set;
        }

        // Functions

        public void ClearnDom()
        {
            _set.ClearDom();
        }


        public float GetDom()
        {
            return _set.DOM;
        }

        public void ORwithDom(float val)
        {
            _set.ORwithDOM(val);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public FuzzyTerm CloneFz()
        {
            return (FzSet)this.Clone();
        }
    }
}
