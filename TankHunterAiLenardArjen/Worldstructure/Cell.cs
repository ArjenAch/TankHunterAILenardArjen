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

        public Cell (Vector pos, int id)
        {
            Position = pos;
            ID = id;
        }
    }
}
