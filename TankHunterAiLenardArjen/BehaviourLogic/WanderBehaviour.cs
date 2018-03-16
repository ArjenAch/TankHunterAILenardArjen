using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.BehaviourLogic
{
    public class WanderBehaviour
    {
        private double wanderRadius;
        private double wanderDistance;
        private double wanderJitter;
        private Random random;
        private double theta;

        //create a vector to a target position on the wander circle
        private Vector WanderTarget ;

        public WanderBehaviour(double wanderRadius, double wanderDistance, double wanderJitter)
        {
            this.wanderRadius = wanderRadius;
            this.wanderDistance = wanderDistance;
            this.wanderJitter = wanderJitter;
            random = new Random();
            theta = random.NextDouble() * (Math.PI * 2);
            WanderTarget = new Vector((float)(wanderRadius * Math.Cos(theta)), (float)(wanderRadius * Math.Sin(theta)));
        }

        public Vector Execute(Vehicle vehicle)
        {
            WanderTarget += new Vector((float)(RandomClamped() * wanderJitter), (float)(RandomClamped() * wanderJitter));
            WanderTarget = Vector.Normalize(WanderTarget);
            WanderTarget = WanderTarget * wanderRadius;

            Vector targetLocal = WanderTarget + new Vector((float)wanderDistance, 0);
            Vector targetWorld = HelpMethods.ToWorldSpace(targetLocal, vehicle.Heading, vehicle.Side, vehicle.Position);
           
            return targetWorld - vehicle.Position;
        }


        //tODO
        private float RandomClamped()
        {
            //should return value between -1 and 1
            float x = (float)random.NextDouble();
            return x;
        }
    }
}
