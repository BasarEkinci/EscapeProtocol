using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player
{
    public class PlayerWalkBackwardState : IState<PlayerMovementController>
    {
        private static readonly int IsMovingBackward = Animator.StringToHash("IsMovingBackward");

        public void EnterState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsMovingBackward, true);
        }

        public void UpdateState(PlayerMovementController player)
        {
            player.Animator.SetBool(IsMovingBackward,player.IsMovingBackward);
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
            player.Animator.SetBool(IsMovingBackward, false);
        }
    }
}