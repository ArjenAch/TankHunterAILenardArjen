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

        public TankPatrol()
        {
            wanderBehaviour = new WanderBehaviour(20,10,5);
            seekBehaviour = new SeekBehaviour();
        }

        public void Enter(Tank tank)
        {
            // IDEA: Tank is in a good mood
        }

        public Vector Execute(Tank tank)
        {
            Vector steeringForce = new Vector(0, 0);
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

        public void Exit(Tank tank)
        {

        }
    }
}
