using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public class Vehicle : MovingEntity
    {
        private World gameWorld;
        // private List<SteeringBehaviours> steering;
        public  Vehicle(World world)
        {
            gameWorld = world;
        }
        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Update(int timeElapsed)
        {
            Vector steeringForce = steering.Calulate();
            Vector acceleratioin = steeringForce / Mass;
            Velocity += acceleratioin * timeElapsed;
            Velocity.Truncate(MaxSpeed);
            Position += Velocity * timeElapsed;

            if(Velocity.LengthSq() > 0.00000001)
            {
                Heading = Vector.Normalize(Velocity);
                Side = Heading.Perp();
            }
            
        }
    }
}
