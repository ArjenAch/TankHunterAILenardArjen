using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Enitities;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.BehaviourLogic
{
    public class ObstacleAvoidanceBehaviour
    {
        private List<Obstacle> obstaclesInRange;
        public Vector Execute(MovingEntity entity)
        {
            Vector steeringForce = new Vector(0, 0);
            double detectionBoxLength = GlobalVars.MinDetectionBoxLength + (entity.Velocity.Length() / entity.MaxSpeed) * GlobalVars.MinDetectionBoxLength;
            entity.gameWorld.GridLogic.CalculateObstaclesWithinRadius(entity, (int)detectionBoxLength);
            obstaclesInRange = entity.gameWorld.GridLogic.ObstaclesInRange;

            //this will keep track of the closest intersecting obstacle (CIB)
            Obstacle ClosestIntersectingObstacle = null;

            //this will be used to track the distance to the CIB
            double DistToClosestIP = double.MaxValue;

            //this will record the transformed local coordinates of the CIB
            Vector LocalPosOfClosestObstacle = new Vector(0,0);
            Vector LocalPos;

            foreach (Obstacle obstacle in obstaclesInRange)
            {
                //calculate this obstacle's position in local space
                LocalPos = HelpMethods.PointToWorldSpace(obstacle.Position, entity.Heading, entity.Side, entity.Position);


                //if the local position has a negative x value then it must lay
                //behind the agent. (in which case it can be ignored)
                if (LocalPos.X >= 0)
                {
                    //if the distance from the x axis to the object's position is less
                    //than its radius + half the width of the detection box then there
                    //is a potential intersection.
                    double ExpandedRadius = obstacle.Bradius + entity.Bradius;

                    if (Math.Abs(LocalPos.Y) < ExpandedRadius)
                    {
                        //now to do a line/circle intersection test. The center of the 
                        //circle is represented by (cX, cY). The intersection points are 
                        //given by the formula x = cX +/-sqrt(r^2-cY^2) for y=0. 
                        //We only need to look at the smallest positive value of x because
                        //that will be the closest point of intersection.
                        double cX = LocalPos.X;
                        double cY = LocalPos.Y;

                        //we only need to calculate the sqrt part of the above equation once
                        double SqrtPart = Math.Sqrt(ExpandedRadius * ExpandedRadius - cY * cY);

                        double ip = cX - SqrtPart;

                        if (ip <= 0.0)
                        {
                            ip = cX + SqrtPart;
                        }

                        //test to see if this is the closest so far. If it is keep a
                        //record of the obstacle and its local coordinates
                        if (ip < DistToClosestIP)
                        {
                            DistToClosestIP = ip;

                            ClosestIntersectingObstacle = obstacle;

                            LocalPosOfClosestObstacle = LocalPos;
                        }
                    }
                }
            }

            //if we have found an intersecting obstacle, calculate a steering 
            //force away from it
            if (ClosestIntersectingObstacle != null)
            {
                //the closer the agent is to an object, the stronger the 
                //steering force should be
                double multiplier = 1.0 + (detectionBoxLength - LocalPosOfClosestObstacle.X) /
                                    detectionBoxLength;

                //calculate the lateral force
                steeringForce.Y = (ClosestIntersectingObstacle.Bradius-
                                           LocalPosOfClosestObstacle.Y) * (float)multiplier;

                //apply a braking force proportional to the obstacles distance from
                //the vehicle. 
                const float BrakingWeight = 0.2f;

                steeringForce.X = (ClosestIntersectingObstacle.Bradius -
                                           LocalPosOfClosestObstacle.X) *
                                           BrakingWeight;
            }

            //finally, convert the steering vector from local to world space
            return HelpMethods.VectorToWorldSpace(steeringForce, entity.Heading, entity.Side);
        }

    }
}
