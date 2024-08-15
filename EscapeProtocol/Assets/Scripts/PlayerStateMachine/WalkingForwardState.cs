using Controllers;

namespace PlayerStateMachine
{
    public class WalkingForwardState : IState
    {
        public void EnterState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
        }

        public void UpdateState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
            if (!playerMovementController.IsMovingForward && playerMovementController.IsMoving)
            {
                player.ChangeState(new WalkingBackwardState());
            }

            if (!playerMovementController.IsGrounded)
            {
                player.ChangeState(new JumpState());
            }
        }

        public void ExitState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
        }
    }
}