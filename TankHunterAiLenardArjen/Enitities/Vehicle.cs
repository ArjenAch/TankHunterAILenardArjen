using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen
{
    public class Vehicle : MovingEntity //Chapter 3 pg 89
    {
        private World gameWorld;
        public SteeringBehavioursController steering;
        public Texture2D Texture { get; set; }
        private Rectangle destinationSize;

        public Vehicle(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position ) : base (mass,side,maxSpeed,maxForce,maxTurnRate,position)
        {
            this.gameWorld = gameWorld;
            this.steering = new SteeringBehavioursController();
            destinationSize = new Rectangle((int)Position.X , (int)Position.Y, GlobalVars.cellSize, GlobalVars.cellSize);
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationSize,null, Color.White);
            spriteBatch.End();
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
