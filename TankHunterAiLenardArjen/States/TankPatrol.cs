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
            wanderBehaviour = new WanderBehaviour(40,0,10);
            seekBehaviour = new SeekBehaviour();
            
        }

        public void Enter(Vehicle tank)
        {
            // IDEA: Tank is in a good mood
            steeringForce = new Vector(tank.Position);

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
                steeringForce = wanderBehaviour.Execute(tank); //seekBehaviour.Execute(
            }

            return steeringForce;
        }

        public void Exit(Vehicle tank)
        {

        }
    }
}
