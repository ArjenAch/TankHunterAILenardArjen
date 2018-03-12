using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using TankHunterAiLenardArjen.States;

namespace TankHunterAiLenardArjen
{
    public class Tank : Vehicle
    {
        private ITankState State { get; set; }

        private Texture2D tankTop;
        private float angleTankTop;

        public Tank(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, Texture2D texture, Texture2D top) : base(gameWorld, mass, side, maxSpeed, maxForce, maxTurnRate, position, texture)
        {
            this.tankTop = top;
            this.angleTankTop = 0;

            // Tank starts default with patrolling
            this.State = new TankPatrol();
        }

        public override void Render(SpriteBatch spriteBatch)
        {

        }

        public override void Update(int timeElapsed)
        {
            base.Update(timeElapsed);
            State.Execute(this);
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

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
