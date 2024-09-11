using Controllers.EnemyDrone;

namespace AnimationStateMachine.Drone
{
    public class DronePatrolState : IState<EnemyDroneController>
    {
        public void EnterState(EnemyDroneController character)
        {
            character.AudioSource.Play();
        }

        public void UpdateState(EnemyDroneController character)
        {
            character.HandlePlayerDetectedPhase();
            if (!character.IsPlayerDetected)
            {
                character.Reverse();
                character.HandleMovement();
            }
            else
            {       
                character.ChangeState(new DroneIdleState());
            }
        }
        public void ExitState(EnemyDroneController character)
        {
            character.AudioSource.Stop();
        }
    }
}