using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.States
{
    public class TankSearchForPlayer : TankState
    {
        public void Enter(Tank tank)
        {
            // IDEA: show question mark on tank
        }

        public void Execute(Tank tank)
        {
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
        }

        public void Exit(Tank tank)
        {
            // IDEA: show something that player is found or not on tank
        }
    }
}
