using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunterAiLenardArjen
{
    public class Tank : Vehicle
    {
        private Texture2D tankTop;
        private float angleTankTop;

        public Tank(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, Texture2D texture, Texture2D top) : base(gameWorld, mass, side, maxSpeed, maxForce, maxTurnRate, position, texture)
        {
            this.tankTop = top;
            this.angleTankTop = 0;
        }

        public override void Render()
        {
            base.Render();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(int timeElapsed)
        {
            base.Update(timeElapsed);
        }
    }
}
