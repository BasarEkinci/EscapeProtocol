using Controllers.Player;

namespace AnimationStateMachine.Player
{
    public class PlayerIdleState : IState
    {
        public void EnterState(PlayerMovementController player)
        {
            
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.HandleMovementDirection();
            if (player.IsGrounded && player.IsMoving)
            {
                if (player.IsMovingForward)
                {
                    player.ChangeState(new PlayerWalkForwardState());
                }
                else if (player.IsMovingBackward)
                {
                    player.ChangeState(new PlayerWalkForwardState());
                }
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