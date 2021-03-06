﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunterAiLenardArjen
{
    public abstract class MovingEntity : BaseGameEntity //Chapter 3 pg 89
    {
        protected MovingEntity(float mass, Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position, World world) : base(position)
        {
            Velocity = new Vector(0, 0);
            Mass = mass;
            Heading = new Vector(1, 1);
            Side = side;
            MaxSpeed = maxSpeed;
            MaxForce = maxForce;
            MaxTurnRate = maxTurnRate;
            gameWorld = world;
        }
        public World gameWorld { get; }
        public override abstract void Update(int timeElapsed);
        public override abstract void Render(SpriteBatch spriteBatch);
        public Vector Velocity { get; set; }
        protected float Mass { get; }
        public Vector Heading { get; set; }
        public Vector Side { get; set; }
        public float MaxSpeed { get; set; }
        public float MaxForce { get; }
        protected float MaxTurnRate { get; }
    }
}