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
        public float MaxTurnRateTurret { get; set; }
        private float angleTankTurret { get; set; }
        private ITankState State { get; set; }
        private Rectangle destinationSize;

        public Tank(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(gameWorld, mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            this.angleTankTurret = 0;
            // Tank starts default with patrolling
            this.State = new TankPatrol();
            destinationSize = new Rectangle((int)Position.X, (int)Position.Y, GlobalVars.cellSize, GlobalVars.cellSize);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            //Render top of the tank
            //The bottom is rendered in vehicle
            spriteBatch.Begin();
            spriteBatch.Draw(TankTopTexture, destinationSize,null, Color.White);
            spriteBatch.End();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(int timeElapsed)
        {
            base.Update(timeElapsed);
        }

        public void ChangeState(ITankState newState)
        {
            State.Exit(this);
            State = newState;
            State.Enter(this);
        }

        public bool PlayerInAttackZone()
        {
            throw new NotImplementedException();
        }

        // Player is in the inner danger circle, tank should avoid player till attack circle
        public bool PlayerInDangerZone()
        {
            throw new NotImplementedException();
        }

        public bool PlayerNotSeenAtLastLocation()
        {
            throw new NotImplementedException();
        }

        public bool PlayerInSearchZone()
        {
            throw new NotImplementedException();
        }
    }
}
