using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.States
{
    public class TankAttackPlayer : ITankState
    {
        public void Enter(Tank tank)
        {
            // IDEA: tank shows signs of being mad
        }

        public void Execute(Tank tank)
        {
            if (tank.PlayerInSearchZone())
            {
                tank.ChangeState(new TankSearchForPlayer());
            }
            else if (tank.PlayerInDangerZone())
            {
                tank.ChangeState(new TankCreateDistanceBetweenPlayer());
            }
            else
            {
                // tank.AttackPlayer();
            }
        }

        public void Exit(Tank tank)
        {
            
        }
    }
}
