using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player
{
    public class PlayerJumpingState : IState
    {
        public void EnterState(PlayerMovementController player)
        {
            
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.HandleJumping();
            player.HandleMovementDirection();
            if (player.IsMoving)
            {
                if (player.IsMovingForward)
                {
                    player.ChangeState(new PlayerWalkForwardState());
                }
                else if (player.IsMovingBackward)
                {
                    player.ChangeState(new PlayerWalkBackwardState());
                }
            }
        }

        public void ExitState(PlayerMovementController player)
        {
        }
    }
}