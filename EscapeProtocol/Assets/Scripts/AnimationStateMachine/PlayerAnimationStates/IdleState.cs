using Controllers;
using Controllers.Player;

namespace AnimationStateMachine.PlayerAnimationStates
{
    public class IdleState : IState
    {
        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if (PlayerMovementController.Instance.IsMoving)
            {
                if (PlayerMovementController.Instance.IsMovingForward)
                {
                    PlayerAnimationController.Instance.ChangeState(new WalkingForwardState());
                }
                else
                {
                    PlayerAnimationController.Instance.ChangeState(new WalkingBackwardState());
                }
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