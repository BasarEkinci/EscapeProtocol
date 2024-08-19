using Controllers;

namespace AnimationStateMachine.BotAnimationStates
{
    public class BotWalkingState : IState
    {
        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if (BotMovementController.Instance.IsWaiting)
            {
                BotAnimationController.Instance.ChangeState(new BotIdleState());
            }
            
        }

        public void ExitState()
        {
        }
    }
}