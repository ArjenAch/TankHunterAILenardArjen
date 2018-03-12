using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.States
{
    public class TankPatrol : TankState
    {
        public void Enter(Tank tank)
        {
            // IDEA: Tank is in a good mood
        }

        public void Execute(Tank tank)
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
                // tank.patrol();
            }
        }

        public void Exit(Tank tank)
        {

        }
    }
}
