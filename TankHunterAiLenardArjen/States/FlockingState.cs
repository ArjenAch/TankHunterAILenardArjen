using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;
using TankHunterAiLenardArjen.States;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.Enitities
{
    public class FlockingState 
    {
        private Vector steeringForce;
        private CohesionBehaviour cohesionBehaviour;
        private SeparationBehaviour separationBehaviour;
        private AlignmentBehaviour alignmentBehaviour;
        private WanderBehaviour wanderBehaviour;

        public FlockingState()
        {
            cohesionBehaviour = new CohesionBehaviour();
            separationBehaviour = new SeparationBehaviour();
            alignmentBehaviour = new AlignmentBehaviour();
            wanderBehaviour = new WanderBehaviour(1.2, 2, 40);
        }

        public void Enter(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }

        public Vector Execute(Vehicle plane, int timeElapsed)
        {
            steeringForce = separationBehaviour.Execute(plane) * GlobalVars.SeperationWeight;
            steeringForce += alignmentBehaviour.Execute(plane) * GlobalVars.AllignmentWeight;
            steeringForce += cohesionBehaviour.Execute(plane) * GlobalVars.CohesionWeight;
            //steeringForce += wanderBehaviour.Execute(plane);
            return steeringForce;

        }

        public void Exit(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }

        public Color GetColor()
        {
            throw new System.NotImplementedException();
        }
    }
}