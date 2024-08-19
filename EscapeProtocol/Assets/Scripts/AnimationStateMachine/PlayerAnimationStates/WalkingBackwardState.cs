using Controllers;
using Controllers.Player;

namespace AnimationStateMachine.PlayerAnimationStates
{
    public class WalkingBackwardState : IState
    {
        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if(PlayerMovementController.Instance.IsMovingForward && PlayerMovementController.Instance.IsMoving)
            {
                PlayerAnimationController.Instance.ChangeState(new WalkingForwardState());
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