using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Worldstructure;

namespace TankHunterAiLenardArjen.GraphLogic
{
    public static class ManhattanHeuristic
    {
        //returns the distance between two cells based on the manhatan heuristic
        public static double Calculate(CellSpacePartition graph, Cell from, Cell to)
        {
            return Math.Abs(to.ID % graph.NumberOfCellsHeight - from.ID % graph.NumberOfCellsHeight) + Math.Abs(from.ID / graph.NumberOfCellsHeight - to.ID / graph.NumberOfCellsHeight);
        }
    }
}
