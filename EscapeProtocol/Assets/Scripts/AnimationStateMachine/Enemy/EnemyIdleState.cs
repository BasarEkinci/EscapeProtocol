using Controllers.Enemy;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace AnimationStateMachine.Enemy
{
    public class EnemyIdleState : IState<EnemyMovementController>
    {
        private static readonly int IsPlayerDetected = Animator.StringToHash("IsPlayerDetected");
        private SoundDataScriptable _soundData = Resources.Load<SoundDataScriptable>("Scriptables/Sounds/EnemySound");

        public void EnterState(EnemyMovementController enemy)
        {
            enemy.Animator.SetBool(IsPlayerDetected,true);
            SoundManager.PLaySound(_soundData,"Target");
        }

        public void UpdateState(EnemyMovementController enemy)
        {
            enemy.Animator.SetBool(IsPlayerDetected,enemy.IsPlayerDetected);

            if (!enemy.IsPlayerDetected)
            {
                enemy.ChangeState(new EnemyWalkingState());
            }
        }

        public void ExitState(EnemyMovementController enemy)
        {
            enemy.Animator.SetBool(IsPlayerDetected,false);
        }
    }
}