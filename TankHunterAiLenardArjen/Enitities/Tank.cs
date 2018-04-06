﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.States;
using TankHunterAiLenardArjen.Support;
using System.Diagnostics;
using TankHunterAiLenardArjen.FuzzyLogic;

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
        public float angleTankTurret { get; set; }
        private ITankState State { get; set; }
        private Rectangle destinationSize;
        Vector steeringForce;

        // Fuzy props
        private FuzzyModule fm { get; set; }
        private FuzzyVariable DistToPlayer { get; set; }
        private FuzzyVariable RangeOfSeight { get; set; }
        private FuzzyVariable PlayerDistance { get; set; }
        private FzSet 
            Distance_Close, Distance_Medium, Distance_Far,
            Seight_Foggy, Seight_Dusty, Seight_Clear,
            Player_Is_ToClose, Player_Is_PerfectRange, Player_Is_FarAway;

        // Player interaction variables
        private const int maxRadiusOfTankSeight = 400;
        private const int tankIsInDangerDistance = 76 * 2;
        private const int tankAttackDistance = 132 * 2;
        private bool playerInSight;
        public float distanceToPlayer;
        public float fuzzyRangeOfSeight;


        public Tank(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position ) : base(gameWorld, mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            this.angleTankTurret = 0;
            destinationSize = new Rectangle((int)Position.X, (int)Position.Y, (int)(GlobalVars.cellSize *1.4), (int)(GlobalVars.cellSize * 1.4));
            distanceToPlayer = 400;
            fuzzyRangeOfSeight = 400;

            playerInSight = false;
            angleTankTurret = 359;
            // Tank starts default with patrolling
            this.State = new TankPatrol(this);
            InitFuzzySets();
        }

        private void InitFuzzySets()
        {
            fm = new FuzzyModule();
            DistToPlayer = fm.CreateFLV("DistanceToPlayer");
            Distance_Close = DistToPlayer.AddLeftShoulderSet("Close", 0, 40, 200);
            Distance_Medium = DistToPlayer.AddTriangularSet("Medium", 40, 200, 300);
            Distance_Far = DistToPlayer.AddRightShoulderSet("Far", 200, 300, 400);

            RangeOfSeight = fm.CreateFLV("RangeOfSeight");
            Seight_Foggy = RangeOfSeight.AddLeftShoulderSet("Foggy", 0, 1, 5);
            Seight_Dusty = RangeOfSeight.AddTriangularSet("Dusty", 1, 5, 9);
            Seight_Clear = RangeOfSeight.AddRightShoulderSet("Clear", 5, 9, 10);

            PlayerDistance = fm.CreateFLV("PlayerDistance");
            Player_Is_ToClose = PlayerDistance.AddLeftShoulderSet("ToClose", 0, 40, 200);
            Player_Is_PerfectRange = PlayerDistance.AddTriangularSet("Perfect", 40, 200, 360);
            Player_Is_FarAway = PlayerDistance.AddRightShoulderSet("Far", 200, 360, 400);

            // Without Combs method, therefore: 3^2 = 9
            fm.AddRule(new FzAND(Distance_Close, Seight_Foggy), Player_Is_ToClose);
            fm.AddRule(new FzAND(Distance_Medium, Seight_Foggy), Player_Is_FarAway);
            fm.AddRule(new FzAND(Distance_Far, Seight_Foggy), Player_Is_FarAway);

            fm.AddRule(new FzAND(Distance_Close, Seight_Dusty), Player_Is_ToClose);
            fm.AddRule(new FzAND(Distance_Medium, Seight_Dusty), Player_Is_PerfectRange);
            fm.AddRule(new FzAND(Distance_Far, Seight_Dusty), Player_Is_FarAway);

            fm.AddRule(new FzAND(Distance_Close, Seight_Clear), Player_Is_ToClose);
            fm.AddRule(new FzAND(Distance_Medium, Seight_Clear), Player_Is_PerfectRange);
            fm.AddRule(new FzAND(Distance_Far, Seight_Clear), Player_Is_PerfectRange);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            // Color of the underlying tile shows the current state of the Tank
            // Blue: Patrol, Red: Attack enemy, Yellow: Search player, Green: Create Distance
            if(GlobalVars.debug)
            {
                InCell.TileColor = State.GetColor();
                InCell.Render(spriteBatch);
                InCell.TileColor = Color.White;
            }

            // Render base of the Tank
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, destinationSize, null, Color.White, spriteAngle, origin,  SpriteEffects.None, 0f);

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

            CalculateDistanceToPlayer(maxRadiusOfTankSeight);
            fm.Fuzzify("DistanceToPlayer", distanceToPlayer);
            //fm.Fuzzify("RangeOfSeight",5.0f);

            //fuzzyRangeOfSeight =  fm.DeFuzzify("PlayerDistance");

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
                    playerInSight = true;
                    // If player is found, loop can stop
                    break;
                }
                else
                {
                    playerInSight = false;
                }
            }
        }

        public bool PlayerInAttackZone()
        {
            return (distanceToPlayer > tankIsInDangerDistance && distanceToPlayer < tankAttackDistance);
        }

        // Player is in the inner danger circle, tank should avoid player till attack circle
        public bool PlayerInDangerZone()
        {
            return (distanceToPlayer < tankIsInDangerDistance);
        }

        // TODO: implement A* check
        public bool PlayerNotSeenAtLastLocation()
        {
            return (distanceToPlayer > 360);
        }

        public bool PlayerInSearchZone()
        {
            return (distanceToPlayer > tankAttackDistance && distanceToPlayer < maxRadiusOfTankSeight);
        }

        public bool PlayerIsOutOfSeight()
        {
            return playerInSight;
        }
    }
}
