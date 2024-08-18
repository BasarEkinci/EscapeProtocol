using Controllers;

namespace AnimationStateMachine
{
    public class JumpState : IState
    {
        public void EnterState()
        {
            
        }

        public void UpdateState()
        {
            if (PlayerMovementController.Instance.IsGrounded)
            {
                PlayerAnimationController.Instance.ChangeState(new IdleState());
            }
        }

        public void ExitState()
        {
        }
    }
}