using TankHunterAiLenardArjen.States;

namespace TankHunterAiLenardArjen.Enitities
{
    public class FlockingState : ITankState
    {
        private Vector steeringForce;
        private CohesionBehaviour cohesionBehaviour;
        private SeparationBehaviour separationBehaviour;
        private AlignmentBehaviour alignmentBehaviour;

        public FlockingState()
        {
            cohesionBehaviour = new CohesionBehaviour();
            separationBehaviour = new SeparationBehaviour();
            alignmentBehaviour = new AlignmentBehaviour();
        }

        public void Enter(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }

        public Vector Execute(Vehicle plane)
        {
            
            steeringForce = cohesionBehaviour.Execute(plane) +  alignmentBehaviour.Execute(plane) + separationBehaviour.Execute(plane);
             
            return steeringForce;

        }

        public void Exit(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }
    }
}