using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public class Player : MovingEntity
    {
        Texture2D playerTexture;

        protected Player(float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, Texture2D texture) : base(mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            playerTexture = texture;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Update(int timeElapsed)
        {
            throw new NotImplementedException();
        }
    }
}
