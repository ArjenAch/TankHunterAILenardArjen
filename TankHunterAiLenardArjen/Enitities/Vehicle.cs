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
        private World gameWorld;
        public Texture2D Texture { get; set; }
        public Texture2D TileDebugNeighborTexture { get; set; }
        public Texture2D TileDebugCenterTexture { get; set; }
        private Rectangle destinationSize;
        public Vector Target { get; set; }

        public Vehicle(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position ) : base (mass,side,maxSpeed,maxForce,maxTurnRate,position)
        {
            this.gameWorld = gameWorld;
            destinationSize = new Rectangle((int)Position.X , (int)Position.Y, GlobalVars.cellSize, GlobalVars.cellSize);

            Target = new Vector(Position.X +1, Position.Y + 1);
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationSize,null, Color.White);
            spriteBatch.End();
            if (GlobalVars.debug == true)
            {
                gameWorld.GridLogic.CalculateNeighborCells(InCell, 40);

                foreach (Cell cell in gameWorld.GridLogic.Neighbors)
                {
                    cell.Render(TileDebugNeighborTexture, spriteBatch);
                }
                InCell.Render(TileDebugCenterTexture, spriteBatch);
            }

        }
    
        public override void Update(int timeElapsed)
        {
            destinationSize.X = (int)Position.X;
            destinationSize.Y = (int)Position.Y;
            gameWorld.GridLogic.UpdateEntity(this);

            
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
