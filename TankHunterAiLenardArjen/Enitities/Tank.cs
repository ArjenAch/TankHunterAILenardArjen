using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.States;
using TankHunterAiLenardArjen.Support;
using System.Diagnostics;

namespace TankHunterAiLenardArjen
{
    public class Tank : Vehicle
    {
        public float AngleTankTurret { get; set; }
        private ITankState State { get; set; }
        private Rectangle destinationSize;
        Vector steeringForce;

        // Player interaction variables
        public const int MaxRadiusOfTankSeight = 188 * 2;
        public const int TankIsInDangerDistance = 76 * 2;
        public const int TankAttackDistance = 132 * 2;
        private bool playerInSight;
        public float DistanceToPlayer;

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


        public Tank(float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, World world) : base(mass, side, maxSpeed, maxForce, maxTurnRate, position, world)
        {
            this.AngleTankTurret = 0;
            destinationSize = new Rectangle((int)Position.X, (int)Position.Y, (int)(GlobalVars.cellSize * 1.4), (int)(GlobalVars.cellSize * 1.4));
            DistanceToPlayer = 400;
            playerInSight = false;
            AngleTankTurret = 359;
            Bradius = GlobalVars.cellSize;
            // Tank starts default with patrolling
            this.State = new TankPatrol(this);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            // Color of the underlying tile shows the current state of the Tank
            // Blue: Patrol, Red: Attack enemy, Yellow: Search player, Green: Create Distance
            if (GlobalVars.debug)
            {
                InCell.TileColor = State.GetColor();
                InCell.Render(spriteBatch);
                InCell.TileColor = Color.White;
            }

            // Render base of the Tank
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, destinationSize, null, Color.White, spriteAngle, origin, SpriteEffects.None, 0f);

            //Render top of the Tank
            spriteBatch.Draw(_tankTopTexture, destinationSize, null, Color.White, AngleTankTurret, tankTopOrigin, SpriteEffects.None, 0f);
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

            CalculateDistanceToPlayer(MaxRadiusOfTankSeight);

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

        // Tries to find a player Object in Cells in a given radius
        private void CalculateDistanceToPlayer(int radius)
        {
            gameWorld.GridLogic.CalculateNeighborsEntities(this, radius);
            foreach (MovingEntity entity in gameWorld.GridLogic.EntitiesInRange)
            {
                if (entity is Player)
                {
                    DistanceToPlayer = Math.Abs((entity.Position - this.Position).Length());
                    playerInSight = false;
                    // If player is found, loop can stop
                    break;
                }
                else
                {
                    playerInSight = true;
                }
            }
        }

        public float DistanceToPosition(Vector position)
        {
                 return Math.Abs((Position - position).Length());
        }

        public bool PlayerInAttackZone()
        {
            return (DistanceToPlayer > TankIsInDangerDistance && DistanceToPlayer < TankAttackDistance);
        }

        // Player is in the inner danger circle, tank should avoid player till attack circle
        public bool PlayerInDangerZone()
        {
            return (DistanceToPlayer < TankIsInDangerDistance);
        }

        public bool PlayerNotSeenAtLastLocation()
        {
            return (DistanceToPlayer > 360);
        }

        public bool PlayerInSearchZone()
        {
            return (DistanceToPlayer > TankAttackDistance && DistanceToPlayer < MaxRadiusOfTankSeight);
        }

        public bool PlayerIsOutOfSeight()
        {
            return playerInSight;
        }
    }
}
