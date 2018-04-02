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
        public float MaxTurnRateTurret { get; set; }
        public float angleTankTurret { get; set; }
        private ITankState State { get; set; }
        private Rectangle destinationSize;
        Vector steeringForce;

        // Player interaction variables
        public const int MaxRadiusOfTankSeight = 188 * 2;
        public const int TankIsInDangerDistance = 76 * 2;
        public const int TankAttackDistance = 132 * 2;
        private bool playerInSight;
        public float distanceToPlayer;

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
            this.angleTankTurret = 0;
            destinationSize = new Rectangle((int)Position.X, (int)Position.Y, (int)(GlobalVars.cellSize * 1.4), (int)(GlobalVars.cellSize * 1.4));
            distanceToPlayer = 400;
            playerInSight = false;
            angleTankTurret = 359;
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
            spriteBatch.Draw(_tankTopTexture, destinationSize, null, Color.White, angleTankTurret, tankTopOrigin, SpriteEffects.None, 0f);
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
                    distanceToPlayer = Math.Abs((entity.Position - this.Position).Length());
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

        public bool PlayerInAttackZone()
        {
            return (distanceToPlayer > TankIsInDangerDistance && distanceToPlayer < TankAttackDistance);
        }

        // Player is in the inner danger circle, tank should avoid player till attack circle
        public bool PlayerInDangerZone()
        {
            return (distanceToPlayer < TankIsInDangerDistance);
        }

        // TODO: implement A* check
        public bool PlayerNotSeenAtLastLocation()
        {
            return (distanceToPlayer > 360);
        }

        public bool PlayerInSearchZone()
        {
            return (distanceToPlayer > TankAttackDistance && distanceToPlayer < MaxRadiusOfTankSeight);
        }

        public bool PlayerIsOutOfSeight()
        {
            return playerInSight;
        }
    }
}
