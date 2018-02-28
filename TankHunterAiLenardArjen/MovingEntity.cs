using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public override abstract void Update(int timeElapsed);
        public override abstract void Render();
        protected Vector Velocity { get; set; }
        protected float Mass { get; }
        protected Vector Heading { get; set; }
        protected Vector Side { get; set; }
        protected float MaxSpeed { get; }
        protected float MaxForce { get; }
        protected float MaxTurnRate { get; }
    }
}
