using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;
using TankHunterAiLenardArjen.GraphLogic;
using TankHunterAiLenardArjen.Support;
using TankHunterAiLenardArjen.Worldstructure;

namespace TankHunterAiLenardArjen.States
{
    public class TankSearchForPlayer : ITankState
    {
        private SeekBehaviour Seek { get; set; }
        private SearchAStar searchAStar;
        private List<Cell> path;
        private int i;

        public void Enter(Tank tank)
        {
            Seek = new SeekBehaviour();
            path = new List<Cell>();
            i = 0;
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {

            Vector steeringForce = new Vector(0, 0);

            //Keep following path until finished
            if (path.Count == 0)
            {

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
                            searchAStar = new SearchAStar(tank, entity.InCell);
                            searchAStar.Search();
                            path = searchAStar.GetPathToTarget();
                            //Path starts from end of list so the path following needs to be started at the end of the list
                            i = path.Count -1;
                            steeringForce = Seek.Execute(tank, path[i].Position) * GlobalVars.SeekingWeight;
                        }
                    }
                }
            }
            else
            {
                if (i > 0)
                {
                    //Seek current cell in path
                    steeringForce = Seek.Execute(tank, path[i].Position) * GlobalVars.SeekingWeight ;
                    
                    //go to next step in path if the tank approximatly reached the cell
                    if (tank.DistanceToPosition(path[i].Position) <= GlobalVars.cellSize /2)
                    {
                        i--;
                    }
                }
                else
                {
                    //Last location in path has been reached so the path is cleared and the cell collors are set to white
                    path.Clear();
                    foreach(Cell cell in searchAStar.ConsideredCells)
                    {
                        cell.TileColor = Color.White;
                    }
                    i = 0;
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
