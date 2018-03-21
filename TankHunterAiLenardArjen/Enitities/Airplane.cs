using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankHunterAiLenardArjen.States;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.Enitities
{
    public class Airplane : Vehicle
    {
        private Rectangle destinationSize;
        Vector steeringForce;
        public Texture2D PlaneTexture { get; set; }
        private ITankState State { get; set; }

        public Airplane(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(gameWorld, mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            State = new FlockingState();
            destinationSize = new Rectangle((int)Position.X, (int)Position.Y, GlobalVars.cellSize/2, GlobalVars.cellSize/2);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(PlaneTexture, destinationSize, null, Color.White);
            spriteBatch.End();
        }

        public override void Update(int timeElapsed)
        {
            base.Update(timeElapsed);
            steeringForce = Calculate(timeElapsed);
            Vector acceleration = steeringForce / Mass;
            Velocity += acceleration * timeElapsed / 1000;
            Velocity.Truncate(MaxSpeed);
            Position += Velocity * timeElapsed /1000;


            if (Velocity.LengthSq() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perp();
            }

            destinationSize.X = (int)Position.X;
            destinationSize.Y = (int)Position.Y;
        }

        public Vector Calculate(int timeElapsed)
        {
            steeringForce = State.Execute(this, timeElapsed);

            return steeringForce;
        }
    }
}
