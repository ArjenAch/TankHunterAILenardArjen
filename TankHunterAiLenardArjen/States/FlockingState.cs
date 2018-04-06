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
            steeringForce = separationBehaviour.Execute(plane) * GlobalVars.SeperationWeight;
            steeringForce += alignmentBehaviour.Execute(plane) * GlobalVars.AllignmentWeight;
            steeringForce += cohesionBehaviour.Execute(plane) * GlobalVars.CohesionWeight;
            return steeringForce;

        }

        public void Exit(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }
    }
}