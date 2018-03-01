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
        public SteeringBehavioursController steering;

        public Vehicle(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base (mass,side,maxSpeed,maxForce,maxTurnRate,position)
        {
            this.gameWorld = gameWorld;
            this.steering = new SteeringBehavioursController();
        }
        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Update(int timeElapsed)
        {
            Vector steeringForce = steering.Calculate(this);
            Vector acceleration = steeringForce / Mass;
            Velocity += acceleration * timeElapsed;
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
