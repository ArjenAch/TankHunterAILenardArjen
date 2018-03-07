using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public abstract class MovingEntity : BaseGameEntity //Chapter 3 pg 89
    {
        protected MovingEntity(float mass,  Vector side, float maxSpeed, float maxForce, float maxTurnRate, Vector position) : base(position)
        {
            Velocity = new Vector(0,0);
            Mass = mass;
            Heading = new Vector(0,0);
            Side = side;
            MaxSpeed = maxSpeed;
            MaxForce = maxForce;
            MaxTurnRate = maxTurnRate;
        }

        public override abstract void Update(int timeElapsed);
        public override abstract void Render();
        public Vector Velocity { get; set; }
        protected float Mass { get; }
        protected Vector Heading { get; set; }
        protected Vector Side { get; set; }
        public float MaxSpeed { get; set; }
        protected float MaxForce { get; }
        protected float MaxTurnRate { get; }
    }
}
