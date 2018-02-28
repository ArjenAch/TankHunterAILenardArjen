using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public abstract class BaseGameEntity
    {
        public abstract void Update(int timeElapsed);
        public abstract void Render();
        public int ID { get; }
        public Vector Position { get; set; }
        public float Scale { get; }
        public float Bradius { get; }

    }
}
