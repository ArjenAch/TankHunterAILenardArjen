using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Support;
using TankHunterAiLenardArjen.Worldstructure;

namespace TankHunterAiLenardArjen
{
    public class Vehicle : MovingEntity //Chapter 3 pg 89
    {
        public World gameWorld { get; }
        public Texture2D TileDebugNeighborTexture { get; set; }
        public Texture2D TileDebugCenterTexture { get; set; }

        public int Radius { get; }

        public Vehicle(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
            this.gameWorld = gameWorld;
            Radius = 80; //TODO 
        }
        public override void Render(SpriteBatch spriteBatch)
        {

        }

        public override void Update(int timeElapsed)
        {
            gameWorld.GridLogic.UpdateEntity(this);

            Position.WrapAround(GlobalVars.worldWidth, GlobalVars.worldHeight);

            //Vector steeringForce = steering.Calculate(this);
            //Vector acceleration = steeringForce / Mass;
            //Velocity += acceleration * timeElapsed;
            //Velocity.Truncate(MaxSpeed);
            //Position += Velocity * timeElapsed;

            //if(Velocity.LengthSq() > 0.00000001)
            //{
            //    Heading = Vector.Normalize(Velocity);
            //    Side = Heading.Perp();
            //}

        }
    }
}
