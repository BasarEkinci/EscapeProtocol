using Controllers.Enemy;

namespace AnimationStateMachine.BotAnimationStates
{
    public class BotIdleState : IState
    {
        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if (!EnemyMovementController.Instance.IsWaiting)
            {
                EnemyAnimationController.Instance.ChangeState(new BotWalkingState());
            }
        }

        public void ExitState()
        {
        }
    }
}