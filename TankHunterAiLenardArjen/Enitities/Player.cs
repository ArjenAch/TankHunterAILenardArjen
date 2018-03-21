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
        private World gameWorld;

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

        public Player(float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, World world) : base(mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            PlayerInputController = new InputController(this);
            gameWorld = world;
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

        private bool NextPositionIsPossible()
        {
            bool result = false;
            int n;

                try
                {
                    n = gameWorld.GridLogic.CalculateCell(Position + Velocity);
                    result = true;
                }
                catch (ArgumentOutOfRangeException) { result = false; };

            return result;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (Support.GlobalVars.playerDebug)
            {
                // Shows the Adjacent cells in player debug mode
                foreach (Edge edge in InCell.Adjecent)
                {
                    edge.Cell1.TileColor = edge.Cell2.TileColor = Color.Green;
                    edge.Cell1.Render(spriteBatch);
                    edge.Cell2.Render(spriteBatch);
                    edge.Cell1.TileColor = edge.Cell2.TileColor = Color.White;
                }

                InCell.TileColor = Color.Red;
                InCell.Render(spriteBatch);
                InCell.TileColor = Color.White;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(_playerTexture, Position.ToVector2(), null, Color.White, playerAngle, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override void Update(int timeElapsed)
        {
            PlayerInputController.Update(timeElapsed);
            playerAngle = (float)Math.Atan2(Velocity.Y, Velocity.X);
            if (!NextPositionIsPossible())
            {
                Velocity.X = Velocity.Y = 0;
                Position = InCell.Position;
            }
            else
            {
                Position += Velocity;
            }

            gameWorld.GridLogic.UpdateEntity(this);
        }
    }
}