using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.States
{
    public class TankSearchForPlayer : ITankState
    {
        public void Enter(Tank tank)
        {
            // IDEA: show question mark on tank
        }

        public Vector Execute(Tank tank)
        {
            Vector steeringForce = new Vector(0, 0);
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else if (tank.PlayerNotSeenAtLastLocation())
            {
                tank.ChangeState(new TankPatrol());
            }
            else
            {
                // tank.A* to last seen location
            }

            return steeringForce;
        }

        public void Exit(Tank tank)
        {
            // IDEA: show something that player is found or not on tank
        }
    }
}
