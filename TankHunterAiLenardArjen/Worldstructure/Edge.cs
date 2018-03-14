namespace TankHunterAiLenardArjen.Worldstructure
{
    public class Edge
    {
        public Cell Cell1 { get; }
        public Cell Cell2 { get; }

        public Edge(Cell c1, Cell c2)
        {
            Cell1 = c1;
            Cell2 = c2;
        }


    }
}