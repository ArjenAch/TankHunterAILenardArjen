using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.BehaviourLogic;

namespace TankHunterAiLenardArjen.States
{
    public class TankPatrol : ITankState
    {
        private WanderBehaviour wanderBehaviour;
        private SeekBehaviour seekBehaviour;
        private Vector steeringForce;

        public TankPatrol()
        {
            wanderBehaviour = new WanderBehaviour(300,0,50);
            seekBehaviour = new SeekBehaviour();
            steeringForce = new Vector(0, 0);
        }

        public void Enter(Vehicle tank)
        {
            // IDEA: Tank is in a good mood
           
        }

        public Vector Execute(Vehicle vehicle)
        {
            return Execute((Tank)vehicle);
        }

        public Vector Execute(Tank tank)
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
                steeringForce = seekBehaviour.Execute(tank, wanderBehaviour.Execute(tank));
            }

            return steeringForce;
        }

        public void Exit(Vehicle tank)
        {

        }
    }
}
