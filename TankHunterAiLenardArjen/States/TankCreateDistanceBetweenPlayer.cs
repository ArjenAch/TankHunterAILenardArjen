using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.States
{
    public class TankCreateDistanceBetweenPlayer : ITankState
    {
        public void Enter(Tank tank)
        {
            // IDEA: tank shows signs of panic
        }

        public Vector Execute(Tank tank)
        {
            Vector steeringForce = new Vector(0, 0);
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else
            {
                // tank.Flee();
            }

            return steeringForce;
        }

        public void Exit(Tank tank)
        {

        }
    }
}
