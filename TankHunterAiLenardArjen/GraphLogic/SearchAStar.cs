using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Worldstructure;
using Priority_Queue;

namespace TankHunterAiLenardArjen.GraphLogic
{
    public class SearchAStar
    {
        Cell from;
        Cell to;
        CellSpacePartition graph;
        Dictionary<int, double> m_GCosts;
        Dictionary<int, double> m_FCosts;
        Dictionary<int, Edge> m_shortestPathTree;
        Dictionary<int, Edge> m_searchFrontier;
        Calculate calculate;

        public SearchAStar(MovingEntity entity, Cell goal)
        {
            graph = entity.gameWorld.GridLogic;
            from = entity.InCell;
            to = goal;
            m_GCosts = new Dictionary<int, double>(graph.TotalNumberOfCells);
            m_FCosts = new Dictionary<int, double>(graph.TotalNumberOfCells);
            m_shortestPathTree = new Dictionary<int, Edge>(graph.TotalNumberOfCells);
            m_searchFrontier = new Dictionary<int, Edge>(graph.TotalNumberOfCells);
            for(int i =0; i < graph.TotalNumberOfCells; i++)
            {
                m_GCosts[i] = 0.0;
                m_FCosts[i] = 0.0;
                m_searchFrontier[i] = null;
                m_shortestPathTree[i] = null;
            }
            calculate = ManhattanHeuristic.Calculate;
        }

        List<Edge> GetSPT()
        {
            return new List<Edge>();
        }

        public List<Cell> GetPathToTarget()
        {
            List<Cell> path = new List<Cell>();
            //just return an empty path if no target or no path found
          
            Cell nd = to;
                
            path.Add(nd);

            while ((nd != from) && (m_shortestPathTree[nd.ID] != null))
            {
                nd = m_shortestPathTree[nd.ID].Cell1;

                path.Add(nd);
            }

            return path;
        }

        double GetCostToTarget()
        {
            return 0.0;
        }

        public delegate double Calculate(CellSpacePartition graph, Cell from, Cell to);

        public void Search()
        {
            FastPriorityQueue<Cell> prioQueu = new FastPriorityQueue<Cell>(graph.TotalNumberOfCells);
            prioQueu.Enqueue(from, 1);

            while (prioQueu.Count != 0)
            {
                Cell nextCloseNode = prioQueu.Dequeue();

                m_shortestPathTree[nextCloseNode.ID] = m_searchFrontier[nextCloseNode.ID];

                if (nextCloseNode == to)
                    return;

                foreach (Edge edge in nextCloseNode.Adjecent)
                {
                    double hCost = calculate(graph, edge.Cell2, to);
                    double gCost = m_GCosts[nextCloseNode.ID] + edge.Cost;

                    try
                    {
                        Edge oldValue;
                        m_searchFrontier.TryGetValue(edge.Cell2.ID, out oldValue);

                        Edge value;
                        m_shortestPathTree.TryGetValue(edge.Cell2.ID, out value);

                        if (oldValue == null)
                        {
                            m_FCosts[edge.Cell2.ID] = gCost + hCost;
                            m_GCosts[edge.Cell2.ID] = gCost;

                            prioQueu.Enqueue(edge.Cell2, (float)m_FCosts[edge.Cell2.ID]);
                            m_searchFrontier[edge.Cell2.ID] = edge; // Cell2?

                        }
                        else if (gCost < m_GCosts[edge.Cell2.ID] && value == null)
                        {
                            m_FCosts[edge.Cell2.ID] = gCost + hCost;
                            m_GCosts[edge.Cell2.ID] = gCost;

                            prioQueu.UpdatePriority(edge.Cell2, (float)m_FCosts[edge.Cell2.ID]);

                            m_searchFrontier[edge.Cell2.ID] = edge; // Cell2?
                        }

                    }
                    catch (ArgumentOutOfRangeException) { }
                }
            }
        }
    }
}
