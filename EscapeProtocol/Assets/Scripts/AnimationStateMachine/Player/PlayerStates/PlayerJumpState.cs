using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class PlayerJumpState : BaseState
    {
        private static readonly int IsJumping = Animator.StringToHash("IsGrounded");

        public override void ExitState()
        {
            Debug.Log("Exit JumpState");
            PlayerStateManager.Instance.animator.SetBool(IsJumping, false);
        }

        public override void EnterState()
        {
            Debug.Log("Enter JumpState");
            PlayerStateManager.Instance.animator.SetBool(IsJumping, true);
        }

        public override void UpdateState()
        {
            if (!PlayerMovementController.Instance.IsGrounded)
            {
                if (PlayerMovementController.Instance.IsMoving)
                {
                    if (PlayerMovementController.Instance.IsMovingForward)
                    {
                        PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.WalkForwardState);
                    }
                    else
                    {
                        PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.PlayerWalkBackwardState);
                    }
                }
                else
                {
                    PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.IdleState);
                }
            }
        }
    }
}