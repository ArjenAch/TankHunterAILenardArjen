﻿using Microsoft.Xna.Framework;
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
        public Texture2D playerTexture { get; set; }

        public Player(float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(mass, side, maxSpeed, maxForce, maxTurnRate, position)
        {
        }

        internal void MoveRight()
        {
            this.Position.X += 1;
        }

        internal void MoveLeft()
        {
            this.Position.X -= 1;
        }

        internal void MoveDown()
        {
            this.Position.Y += 1;
        }

        internal void MoveUp()
        {
            this.Position.Y -= 1;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(playerTexture, new Vector2(Position.X, Position.Y), Color.White);
            spriteBatch.End();
        }

        public override void Update(int timeElapsed)
        {
            throw new NotImplementedException();
        }
    }
}
