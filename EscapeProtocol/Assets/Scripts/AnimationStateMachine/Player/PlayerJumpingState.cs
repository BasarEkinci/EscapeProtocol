using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player
{
    public class PlayerJumpingState : IState<PlayerMovementController>
    {
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

        public void EnterState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsGrounded, false);
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsGrounded,player.IsGrounded);
            if(player.IsGrounded)
            {
                if (player.IsMoving)
                {
                    if (player.IsMovingForward)
                    {
                        player.ChangeState(new PlayerWalkForwardState());
                    }
                    else if (!player.IsMovingForward)
                    {
                        player.ChangeState(new PlayerWalkBackwardState());
                    }
                }
                else
                    player.ChangeState(new PlayerIdleState());
            }
        }

        public void ExitState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsGrounded, true);
        }
    }
}