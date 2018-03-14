using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public class Vehicle : MovingEntity //Chapter 3 pg 89
    {
        private World gameWorld;
        public SteeringBehavioursController steering;
        public Texture2D Texture;

        public Vehicle(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, Texture2D texture) : base (mass,side,maxSpeed,maxForce,maxTurnRate,position)
        {
            this.Texture = texture;
            this.gameWorld = gameWorld;
            this.steering = new SteeringBehavioursController();
        }
        public override void Render(SpriteBatch spriteBatch)
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
