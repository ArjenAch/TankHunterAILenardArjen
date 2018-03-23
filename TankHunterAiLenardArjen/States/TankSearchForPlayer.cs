using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;

namespace TankHunterAiLenardArjen.States
{
    public class TankSearchForPlayer : ITankState
    {
        private SeekBehaviour Seek { get; set; }

        public void Enter(Tank tank)
        {
            Seek = new SeekBehaviour();
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {
            Vector steeringForce = new Vector(0, 0);
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else if (tank.PlayerNotSeenAtLastLocation())
            {
                tank.ChangeState(new TankPatrol(tank));
            }
            else
            {
                tank.gameWorld.GridLogic.CalculateNeighborsEntities(tank, Tank.MaxRadiusOfTankSeight);
                foreach (MovingEntity entity in tank.gameWorld.GridLogic.EntitiesInRange)
                {
                    if (entity is Player)
                    {
                        steeringForce = Seek.Execute(tank, entity.Position);
                    }
                }
            }

            return steeringForce;
        }

        public void Exit(Tank tank)
        {
            // IDEA: show something that player is found or not on tank
        }

        public Color GetColor()
        {
            return Color.Yellow;
        }
    }
}
