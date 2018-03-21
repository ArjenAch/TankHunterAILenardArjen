using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.States;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen
{
    public class Tank : Vehicle
    {
        // Base tank texture
        private float spriteAngle;
        private Vector2 origin;
        private Texture2D _texture;
        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                origin.X = _texture.Width / 2;
                origin.Y = _texture.Height / 2;
            }
        }

        private Vector2 tankTopOrigin;
        private Texture2D _tankTopTexture;

        public Texture2D TankTopTexture
        {
            get { return _tankTopTexture; }
            set
            {
                _tankTopTexture = value;
                tankTopOrigin.X = _tankTopTexture.Width / 2;
                tankTopOrigin.Y = _tankTopTexture.Height / 2;
            }
        }

        public float MaxTurnRateTurret { get; set; }
        private float angleTankTurret { get; set; }
        private ITankState State { get; set; }
        private Rectangle destinationSize;
        Vector steeringForce;

        public Tank(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(gameWorld, mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            this.angleTankTurret = 0;

            // Tank starts default with patrolling
            this.State = new TankPatrol();
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            // Color of the underlying tile shows the current state of the Tank
            // Blue: Patrol, Red: Attack enemy, Yellow: Search player, Green: Create Distance
            InCell.TileColor = State.GetColor();
            InCell.Render(spriteBatch);
            InCell.TileColor = Color.White;

            // Render base of the Tank
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, Position.ToVector2(), null, Color.White, spriteAngle, origin, 1.0f, SpriteEffects.None, 0f);

            //Render top of the Tank
            spriteBatch.Draw(_tankTopTexture, Position.ToVector2(), null, Color.White, angleTankTurret, tankTopOrigin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(int timeElapsed)
        {
            steeringForce = Calculate(timeElapsed);
            Vector acceleration = steeringForce / Mass;
            Velocity += acceleration * timeElapsed / 1000;
            Velocity.Truncate(MaxSpeed);
            Position += Velocity;

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

        public void ChangeState(ITankState newState)
        {
            State.Exit(this);
            State = newState;
            State.Enter(this);
        }

        public bool PlayerInAttackZone()
        {
            return false;
        }

        // Player is in the inner danger circle, tank should avoid player till attack circle
        public bool PlayerInDangerZone()
        {
            return false;
        }

        public bool PlayerNotSeenAtLastLocation()
        {
            return false;
        }

        public bool PlayerInSearchZone()
        {
            return false;
        }
    }
}
