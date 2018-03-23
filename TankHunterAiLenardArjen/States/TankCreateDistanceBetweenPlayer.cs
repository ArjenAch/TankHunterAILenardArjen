using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;

namespace TankHunterAiLenardArjen.States
{
    public class TankCreateDistanceBetweenPlayer : ITankState
    {
        private FleeBehaviour Flee { get; set; }

        public void Enter(Tank tank)
        {
            Flee = new FleeBehaviour();
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {
            Vector steeringForce = new Vector(0, 0);
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else if (tank.PlayerIsOutOfSeight())
            {
                tank.ChangeState(new TankPatrol(tank));
            }
            else
            {
                tank.gameWorld.GridLogic.CalculateNeighborsEntities(tank, Tank.TankIsInDangerDistance); 
                foreach (MovingEntity entity in tank.gameWorld.GridLogic.EntitiesInRange)
                {
                    if(entity is Player)
                    {
                        return Flee.Execute(tank, entity.Position);
                    }
                }
            }

            return steeringForce;
        }

        public void Exit(Tank tank)
        {

        }

        public Color GetColor()
        {
            return Color.Green;
        }
    }
}
