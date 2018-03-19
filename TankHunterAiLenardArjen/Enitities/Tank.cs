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
        public Texture2D TankTopTexture { get; set; }
        public Texture2D TargetTexture { get; set; }
        public float MaxTurnRateTurret { get; set; }
        private float angleTankTurret { get; set; }
        private ITankState State { get; set; }
        private Rectangle destinationSize;
        private Rectangle destinationSizeTarget;
        Vector steeringForce;

        public Tank(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(gameWorld, mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            this.angleTankTurret = 0;
            // Tank starts default with patrolling
            this.State = new TankPatrol();
            destinationSize = new Rectangle((int)Position.X - GlobalVars.cellSize / 2, (int)Position.Y - GlobalVars.cellSize / 2, GlobalVars.cellSize, GlobalVars.cellSize);
            destinationSizeTarget = new Rectangle (0, 0, GlobalVars.cellSize/2, GlobalVars.cellSize/2);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            //Render top of the tank
            //The bottom is rendered in vehicle
            spriteBatch.Begin();
            spriteBatch.Draw(TankTopTexture, destinationSize,null, Color.White);
            if(GlobalVars.debug == true)
                spriteBatch.Draw(TargetTexture, destinationSizeTarget, null, Color.DarkBlue);
            spriteBatch.End();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(int timeElapsed)
        {
            steeringForce = Calculate();
            Vector acceleration = steeringForce / Mass;
            Velocity += acceleration * (timeElapsed / 10);
            Velocity.Truncate(MaxSpeed);
            Position += Velocity * (timeElapsed /10);

            if (Velocity.LengthSq() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perp();
            }

            destinationSize.X = (int) Position.X - GlobalVars.cellSize / 2;
            destinationSize.Y = (int) Position.Y - GlobalVars.cellSize / 2;
            destinationSizeTarget.X = (int)steeringForce.X;
            destinationSizeTarget.Y = (int)steeringForce.Y ;
            base.Update(timeElapsed);
        }

        public Vector Calculate()
        {
            steeringForce = State.Execute(this);
           
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
