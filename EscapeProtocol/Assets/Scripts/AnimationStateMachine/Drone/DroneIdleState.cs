using Controllers.EnemyDrone;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace AnimationStateMachine.Drone
{
    public class DroneIdleState : IState<EnemyDroneController>
    {
        private readonly SoundDataScriptable _soundData = Resources.Load<SoundDataScriptable>("Scriptables/Sounds/DroneSound");

        public void EnterState(EnemyDroneController character)
        {
            SoundManager.PLaySound(_soundData,"PlayerDetected");
        }

        public void UpdateState(EnemyDroneController character)
        {
            character.HandlePlayerDetectedPhase();
            if (!character.IsPlayerDetected)
            {
                character.ChangeState(new DronePatrolState());
            }
        }

        public void ExitState(EnemyDroneController character)
        {
        }
    }
}