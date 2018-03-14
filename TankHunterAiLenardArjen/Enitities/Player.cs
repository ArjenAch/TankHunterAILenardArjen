using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.PlayerInput;
using TankHunterAiLenardArjen.Worldstructure;

namespace TankHunterAiLenardArjen
{
    public class Player : MovingEntity
    {
        private Texture2D _playerTexture;
        private InputController PlayerInputController;
        private Vector2 origin;
        float playerAngle;

        public Texture2D PlayerTexture
        {
            get { return _playerTexture; }
            set
            {
                _playerTexture = value;
                origin.X = _playerTexture.Width / 2;
                origin.Y = _playerTexture.Height / 2;
            }
        }

        public Player(float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            PlayerInputController = new InputController(this);
        }

        internal void MoveRight(int timeElapsed)
        {
            Accelerate(new Vector(MaxForce / Mass, 0), timeElapsed);
        }

        internal void MoveLeft(int timeElapsed)
        {
            Accelerate(new Vector(-MaxForce / Mass, 0), timeElapsed);
        }

        internal void MoveDown(int timeElapsed)
        {
            Accelerate(new Vector(0, MaxForce / Mass), timeElapsed);
        }

        internal void MoveUp(int timeElapsed)
        {
            Accelerate(new Vector(0, -MaxForce / Mass), timeElapsed);
        }

        private void Accelerate(Vector acceleration, int timeElapsed)
        {
            Velocity += acceleration * timeElapsed / 1000;
            Velocity.Truncate(MaxSpeed);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_playerTexture, Position.ToVector2(), null, Color.White, playerAngle, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override void Update(int timeElapsed)
        {
            PlayerInputController.Update(timeElapsed);
            Position += Velocity;
            playerAngle = (float)Math.Atan2(Velocity.Y, Velocity.X);
        }
    }
}