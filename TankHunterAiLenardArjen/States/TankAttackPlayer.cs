using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;

namespace TankHunterAiLenardArjen.States
{
    public class TankAttackPlayer : ITankState
    {
        private SeekBehaviour Seek;

        public TankAttackPlayer()
        {
            Seek = new SeekBehaviour();
        }

        public void Enter(Vehicle tank)
        {
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
                tank.ChangeState(new TankPatrol(tank));
            }
            else
            {
                foreach (MovingEntity entity in tank.gameWorld.GridLogic.EntitiesInRange)
                {
                    if (entity is Player)
                    {
                        steeringForce = Seek.Execute(tank, entity.Position);
                        // Aim turret at player 
                        Vector playerTank = entity.Position - tank.Position;
                        tank.angleTankTurret = (float)Math.Atan2(playerTank.Y, playerTank.X);
                    }
                }
            }

            return steeringForce / 4;
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
