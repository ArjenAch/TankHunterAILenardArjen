using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.Worldstructure
{
    public class Cell // Chapter 3 pg 127
    {
        public int ID { get; }
        public Vector Position { get; set; } // Middle of the cell
        public List<BaseGameEntity> Members { get; set; }
        public List<Edge> Adjecent { get; set; }
        public bool Visited { get; set; }

        public Cell (Vector pos, int id)
        {
            Adjecent = new List<Edge>();
            Members = new List<BaseGameEntity>();
            Position = pos;
            ID = id;
        }
    }
}
