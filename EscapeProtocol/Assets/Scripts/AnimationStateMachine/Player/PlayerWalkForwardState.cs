using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player
{
    public class PlayerWalkForwardState : IState
    {
        public void EnterState(PlayerMovementController player)
        {
            Debug.Log("Player Walk Forward");
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.HandleMovement();
            player.HandleMovementDirection();
            if (!player.IsMoving)
            {
                player.ChangeState(new PlayerIdleState());
            }
            else if (player.IsMovingBackward)
            {
                player.ChangeState(new PlayerWalkBackwardState());    
            }
            else if(!player.IsGrounded)
            {
                player.ChangeState(new PlayerJumpingState());
            }
        }

        public void ExitState(PlayerMovementController player)
        {
        }
    }
}