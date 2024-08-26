using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player
{
    public class PlayerIdleState : IState<PlayerMovementController>
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public void EnterState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsMoving, false);
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsMoving, player.IsMoving);
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
            player.Animator.SetBool(IsMoving, true);
        }
    }
}