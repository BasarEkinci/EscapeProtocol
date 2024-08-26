using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player
{
    public class PlayerWalkForwardState : IState<PlayerMovementController>
    {
        private static readonly int IsMovingForward = Animator.StringToHash("IsMovingForward");

        public void EnterState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsMovingForward, true);
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsMovingForward,player.IsMovingForward);
            Debug.Log("Player is walking forward");
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
            player.Animator.SetBool(IsMovingForward, false);
        }
    }
}