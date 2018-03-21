using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;

namespace TankHunterAiLenardArjen.States
{
    public class TankPatrol : ITankState
    {
        private WanderBehaviour wanderBehaviour;
        private Vector steeringForce;

        public TankPatrol()
        {
            wanderBehaviour = new WanderBehaviour(1.20, 2, 40);
            steeringForce = new Vector(2, 2);
        }

        public void Enter(Vehicle tank)
        {
            // IDEA: Tank is in a good mood
        }

        public Vector Execute(Vehicle vehicle, int timeElapsed)
        {
            return Execute((Tank)vehicle, timeElapsed);
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {
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
                steeringForce = wanderBehaviour.Execute(tank, timeElapsed); 
            }

            return steeringForce;
        }

        public void Exit(Vehicle tank)
        {

        }

        public Color GetColor()
        {
            return Color.Blue;
        }
    }
}
