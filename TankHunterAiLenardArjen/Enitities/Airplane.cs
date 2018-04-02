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
        private Texture2D _planeTexture;
        private Vector2 origin;
        private float spriteAngle;
        private FlockingState State { get; set; }
        public Texture2D PlaneTexture
        {
            get { return _planeTexture; }
            set
            {
                _planeTexture = value;
                origin.X = _planeTexture.Width / 2;
                origin.Y = _planeTexture.Height / 2;
            }
        }

        public Airplane(float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, World world) : base(mass, side, maxSpeed, maxForce, maxTurnRate, position, world)
        {
            State = new FlockingState();
            spriteAngle = 0;
            destinationSize = new Rectangle((int)Position.X, (int)Position.Y, GlobalVars.cellSize/2, GlobalVars.cellSize/2);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_planeTexture, destinationSize, null, Color.White, spriteAngle, origin, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public override void Update(int timeElapsed)
        {
            steeringForce = Calculate(timeElapsed);
            Vector acceleration = steeringForce / Mass;
            Velocity += acceleration * timeElapsed / 1000; 
            Velocity.Truncate(MaxSpeed);
            Position += Velocity ; 


            if (Velocity.LengthSq() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perp();
            }

            spriteAngle = (float)Math.Atan2(Velocity.Y, Velocity.X);

            destinationSize.X = (int)Position.X;
            destinationSize.Y = (int)Position.Y;

            base.Update(timeElapsed);
        }

        public Vector Calculate(int timeElapsed)
        {
            steeringForce = State.Execute(this, timeElapsed);

            return steeringForce;
        }
    }
}
