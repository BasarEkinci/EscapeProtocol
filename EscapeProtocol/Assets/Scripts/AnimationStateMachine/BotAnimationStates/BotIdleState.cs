using Controllers;

namespace AnimationStateMachine.BotAnimationStates
{
    public class BotIdleState : IState
    {
        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if (!BotMovementController.Instance.IsWaiting)
            {
                BotAnimationController.Instance.ChangeState(new BotWalkingState());
            }
        }

        public void ExitState()
        {
        }
    }
}