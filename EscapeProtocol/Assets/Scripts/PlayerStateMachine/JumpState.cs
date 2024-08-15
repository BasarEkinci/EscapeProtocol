using Controllers;

namespace PlayerStateMachine
{
    public class JumpState : IState
    {
        public void EnterState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
            
        }

        public void UpdateState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
            if (playerMovementController.IsGrounded)
            {
                player.ChangeState(new IdleState());
            }
        }

        public void ExitState(PlayerAnimationController player, PlayerMovementController playerMovementController)
        {
        }
    }
}