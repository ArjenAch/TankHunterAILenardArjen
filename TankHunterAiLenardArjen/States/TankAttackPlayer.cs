using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankHunterAiLenardArjen.States
{
    public class TankAttackPlayer : ITankState
    {
        public void Enter(Vehicle tank)
        {
            // IDEA: tank shows signs of being mad
        }

        public Vector Execute(Vehicle vehicle, int timeElapsed)
        {
            return Execute((Tank)vehicle, timeElapsed);
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {
            Vector steeringForce = new Vector(0, 0);
            if (tank.PlayerInSearchZone())
            {
                tank.ChangeState(new TankSearchForPlayer());
            }
            else if (tank.PlayerInDangerZone())
            {
                tank.ChangeState(new TankCreateDistanceBetweenPlayer());
            }
            else if(tank.PlayerIsOutOfSeight())
            {
                tank.ChangeState(new TankPatrol());
            }
            else
            {
                // tank.AttackPlayer();
            }

            return steeringForce;
        }

        public void Exit(Vehicle tank)
        {
            
        }

        public Color GetColor()
        {
            return Color.Red;
        }
    }
}
