using Controllers;

namespace PlayerStateMachine
{
    public class IdleState : IState
    {
        public void EnterState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
        }

        public void UpdateState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
            if (playerMovementController.IsMoving)
            {
                if (playerMovementController.IsMovingForward)
                {
                    player.ChangeState(new WalkingForwardState());
                }
                else
                {
                    player.ChangeState(new WalkingBackwardState());
                }
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