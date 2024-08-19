using Controllers;

namespace AnimationStateMachine.PlayerAnimationStates
{
    public class WalkingForwardState : IState
    {
        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if (!PlayerMovementController.Instance.IsMovingForward && PlayerMovementController.Instance.IsMoving)
            {
                PlayerAnimationController.Instance.ChangeState(new WalkingBackwardState());
            }

            if (!PlayerMovementController.Instance.IsGrounded)
            {
                PlayerAnimationController.Instance.ChangeState(new JumpState());
            }
        }

        public void ExitState()
        {
        }
    }
}