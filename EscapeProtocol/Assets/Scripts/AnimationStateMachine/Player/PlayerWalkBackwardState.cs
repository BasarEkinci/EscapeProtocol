using Controllers.Player;

namespace AnimationStateMachine.Player
{
    public class PlayerWalkBackwardState : IState
    {
        public void EnterState(PlayerMovementController player)
        {
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.HandleMovement();
            player.HandleMovementDirection();
            if (!player.IsMoving)
            {
                player.ChangeState(new PlayerIdleState());
            }
            else if (player.IsMovingForward)
            {
                player.ChangeState(new PlayerWalkForwardState());
            }
            else if (!player.IsGrounded)
            {
                player.ChangeState(new PlayerJumpingState());
            }
        }

        public void ExitState(PlayerMovementController player)
        {
        }
    }
}