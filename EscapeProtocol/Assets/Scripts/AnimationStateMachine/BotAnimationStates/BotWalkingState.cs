using Controllers.Enemy;

namespace AnimationStateMachine.BotAnimationStates
{
    public class BotWalkingState : IState
    {
        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if (EnemyMovementController.Instance.IsWaiting)
            {
                EnemyAnimationController.Instance.ChangeState(new BotIdleState());
            }
            
        }

        public void ExitState()
        {
        }
    }
}