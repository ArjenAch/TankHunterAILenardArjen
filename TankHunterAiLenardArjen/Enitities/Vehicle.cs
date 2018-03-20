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
        private Texture2D _texture;
        public Texture2D TileDebugNeighborTexture { get; set; }
        public Texture2D TileDebugCenterTexture { get; set; }
        private Rectangle destinationSize;
        public int Radius { get; }
        public int TimeElapsed { get; set; }
        protected double rotation;
       // private Vector2 origin;

        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                //origin.X = _texture.Width / 4;
                //origin.Y = _texture.Height / 2;
            }
        }

        public Vehicle(World gameWorld, float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, double rotation ) : base (mass,side,maxSpeed,maxForce,maxTurnRate,position)
        {
            this.gameWorld = gameWorld;
            this.rotation = rotation;
            destinationSize = new Rectangle((int)Position.X , (int)Position.Y, GlobalVars.cellSize, GlobalVars.cellSize);
            Radius = 100; //TODO 
            TimeElapsed = 0;
            Heading = new Vector((float)Math.Sin(rotation), -(float)Math.Cos(rotation));
        }
        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, destinationSize, null, Color.White, (float)rotation, Vector2.Zero, SpriteEffects.None, 0);
           // spriteBatch.Draw(Texture, destinationSize,null ,Color.White);
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
            destinationSize.X = (int)Position.X - GlobalVars.cellSize /2;
            destinationSize.Y = (int)Position.Y - GlobalVars.cellSize / 2;
            rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
            gameWorld.GridLogic.UpdateEntity(this);
            Position.WrapAround(gameWorld.WorldWidth, gameWorld.WorldHeight);

            
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
