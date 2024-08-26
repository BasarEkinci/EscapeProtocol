using Controllers.Enemy;
using UnityEngine;

namespace AnimationStateMachine.Enemy
{
    public class EnemyWalkingState : IState<EnemyMovementController>
    {
        private static readonly int IsPlayerDetected = Animator.StringToHash("IsPlayerDetected");

        public void EnterState(EnemyMovementController enemy)
        {
            enemy.Animator.SetBool(IsPlayerDetected, false);   
        }

        public void UpdateState(EnemyMovementController enemy)
        {
            enemy.Animator.SetBool(IsPlayerDetected, enemy.IsPlayerDetected);
            if (enemy.IsPlayerDetected)
            {
                enemy.ChangeState(new EnemyIdleState());
            }
        }

        public void ExitState(EnemyMovementController enemy)
        {
            enemy.Animator.SetBool(IsPlayerDetected, true);
        }
    }
}