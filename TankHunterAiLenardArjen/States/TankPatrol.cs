using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.States
{
    public class TankPatrol : ITankState
    {
        private WanderBehaviour wanderBehaviour;
        private ObstacleAvoidanceBehaviour avoid;
        private Vector steeringForce;
        private int TimeElapsed;

        public TankPatrol(Tank tank)
        {
            wanderBehaviour = new WanderBehaviour(210, 250, 200);
            steeringForce = new Vector(2, 2);
            TimeElapsed = GlobalVars.BehaviourDelay;
            avoid = new ObstacleAvoidanceBehaviour();
        }

        public void Enter(Tank tank)
        {
            // IDEA: Tank is in a good mood
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {
            TimeElapsed += timeElapsed;
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else if (tank.PlayerInDangerZone())
            {
                tank.ChangeState(new TankCreateDistanceBetweenPlayer());
            }
            else
            {
                if (TimeElapsed >= GlobalVars.BehaviourDelay)
                {
                    steeringForce += wanderBehaviour.Execute(tank, timeElapsed);
                    TimeElapsed = 0;
                }
                steeringForce += avoid.Execute(tank) * GlobalVars.ObstacleAvoidanceWeight;
            }
            return steeringForce;
        }

        public void Exit(Tank tank)
        {

        }

        public Color GetColor()
        {
            return Color.Blue;
        }
    }
}
