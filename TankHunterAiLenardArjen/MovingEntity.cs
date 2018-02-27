using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public override abstract void  Update(int timeElapsed);
        public override abstract void Render();

        public Vector Velocity()
        {
            throw new NotImplementedException();
        }

        public float Mass()
        {
            throw new NotImplementedException();
        }

        public Vector Heading()
        {
            throw new NotImplementedException();
        }

        public float MaxSpeed()
        {
            throw new NotImplementedException();
        }

        public float MaxForce()
        {
            throw new NotImplementedException();
        }

        public float MaxTurnRate()
        {
            throw new NotImplementedException();
        }
    }
}
