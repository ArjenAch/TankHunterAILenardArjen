namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public interface FuzzyTerm
    {
        FuzzyTerm Clone();
        float GetDom();
        void ClearnDom();
        void ORwithDom(float val);
    }
}