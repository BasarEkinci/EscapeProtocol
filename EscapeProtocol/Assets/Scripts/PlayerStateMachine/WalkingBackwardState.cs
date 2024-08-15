using Controllers;

namespace PlayerStateMachine
{
    public class WalkingBackwardState : IState
    {
        public void EnterState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
        }

        public void UpdateState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
            if(playerMovementController.IsMovingForward && playerMovementController.IsMoving)
            {
                player.ChangeState(new WalkingForwardState());
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