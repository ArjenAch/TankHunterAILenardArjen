using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.FuzzyLogic
{
    ///////////////////////////////////////////////////////////////////////////////
    //
    //  implementation of FzAND
    //
    ///////////////////////////////////////////////////////////////////////////////
    public class FzAND : FuzzyTerm, ICloneable
    {
        // Properties
        private List<FuzzyTerm> _terms { get; set; }

        // Constructors
        public FzAND(FuzzyTerm op1, FuzzyTerm op2)
        {
            _terms = new List<FuzzyTerm>();
            _terms.Add(op1.CloneFz());
            _terms.Add(op2.CloneFz());
        }

        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            _terms = new List<FuzzyTerm>();
            _terms.Add(op1.CloneFz());
            _terms.Add(op2.CloneFz());
            _terms.Add(op3.CloneFz());
        }

        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            _terms = new List<FuzzyTerm>();
            _terms.Add(op1.CloneFz());
            _terms.Add(op2.CloneFz());
            _terms.Add(op2.CloneFz());
            _terms.Add(op4.CloneFz());
        }

        // Functions
        public void ClearnDom()
        {
            foreach (FuzzyTerm term in _terms)
            {
                term.ClearnDom();
            }
        }

        public void ORwithDom(float val)
        {
            foreach (FuzzyTerm term in _terms)
            {
                term.ORwithDom(val);
            }
        }

        public float GetDom()
        {
            float smallest = float.PositiveInfinity;

            foreach (FuzzyTerm term in _terms)
            {
                if (term.GetDom() < smallest)
                {
                    smallest = term.GetDom();
                }
            }

            return smallest;
        }

        public FuzzyTerm CloneFz()
        {
            return (FzAND)this.Clone();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    //
    //  implementation of FzOR
    //
    ///////////////////////////////////////////////////////////////////////////////
    public class FzOR : FuzzyTerm, ICloneable
    {
        // Properties 
        private List<FuzzyTerm> _terms { get; set; }

        // Constructors
        public FzOR(FuzzyTerm op1, FuzzyTerm op2)
        {
            _terms = new List<FuzzyTerm>();
            _terms.Add(op1.CloneFz());
            _terms.Add(op2.CloneFz());
        }

        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            _terms = new List<FuzzyTerm>();
            _terms.Add(op1.CloneFz());
            _terms.Add(op2.CloneFz());
            _terms.Add(op3.CloneFz());
        }

        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            _terms = new List<FuzzyTerm>();
            _terms.Add(op1.CloneFz());
            _terms.Add(op2.CloneFz());
            _terms.Add(op2.CloneFz());
            _terms.Add(op4.CloneFz());
        }

        // Functions
        public void ClearnDom()
        {
            throw new NotImplementedException();
        }

        public float GetDom()
        {
            throw new NotImplementedException();
        }

        public void ORwithDom(float val)
        {
            throw new NotImplementedException();
        }

        public FuzzyTerm CloneFz()
        {
            return (FzOR)this.Clone();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
