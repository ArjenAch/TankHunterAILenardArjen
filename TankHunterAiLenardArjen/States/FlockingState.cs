using TankHunterAiLenardArjen.BehaviourLogic;
using TankHunterAiLenardArjen.States;

namespace TankHunterAiLenardArjen.Enitities
{
    public class FlockingState : ITankState
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
            wanderBehaviour = new WanderBehaviour(1.2, 2, 10);
        }

        public void Enter(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }

        public Vector Execute(Vehicle plane)
        {

            steeringForce = separationBehaviour.Execute(plane);
            steeringForce += alignmentBehaviour.Execute(plane);
            steeringForce += cohesionBehaviour.Execute(plane);
            //steeringForce += wanderBehaviour.Execute(plane);

            return steeringForce;

        }

        public void Exit(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }
    }
}