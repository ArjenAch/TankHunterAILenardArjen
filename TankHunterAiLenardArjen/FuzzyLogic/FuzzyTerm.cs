namespace TankHunterAiLenardArjen.FuzzyLogic
{
    public interface FuzzyTerm
    {
        FuzzyTerm CloneFz();
        float GetDom();
        void ClearnDom();
        void ORwithDom(float val);
    }
}