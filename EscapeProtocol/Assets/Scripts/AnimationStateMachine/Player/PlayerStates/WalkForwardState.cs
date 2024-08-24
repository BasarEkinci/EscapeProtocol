using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class WalkForwardState : BaseState
    {
        private static readonly int IsWalkingForward = Animator.StringToHash("IsMovingForward");

        public override void ExitState()
        {
            Debug.Log("Exit WalkForwardState");
            PlayerStateManager.Instance.animator.SetBool(IsWalkingForward, false);
        }

        public override void EnterState()
        {
            Debug.Log("Enter WalkForwardState");
            PlayerStateManager.Instance.animator.SetBool(IsWalkingForward, true);
        }

        public override void UpdateState()
        {
            if (PlayerMovementController.Instance.IsMoving)
            {
                if (!PlayerMovementController.Instance.IsMovingForward)
                {
                    PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.PlayerWalkBackwardState);
                }
                else if (PlayerMovementController.Instance.IsMovingForward && !PlayerMovementController.Instance.IsGrounded)
                {
                    PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.PlayerJumpState);
                }
            }
            else
            {
                PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.IdleState);
            }
        }
    }
}