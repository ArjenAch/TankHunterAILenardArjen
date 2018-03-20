using Microsoft.Xna.Framework;
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
            wanderBehaviour = new WanderBehaviour(500, 500, 50);
        }

        public void Enter(Vehicle plane)
        {
            throw new System.NotImplementedException();
        }

        public Vector Execute(Vehicle plane, int timeElapsed)
        {
            
            steeringForce = separationBehaviour.Execute(plane)  + alignmentBehaviour.Execute(plane) + cohesionBehaviour.Execute(plane) + wanderBehaviour.Execute(plane, timeElapsed);
             
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