using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class PlayerWalkBackwardState : BaseState
    {
        private static readonly int IsWalkingForward = Animator.StringToHash("IsMovingForward");

        public override void ExitState()
        {
            Debug.Log("Exit WalkBackwardState");
            PlayerStateManager.Instance.animator.SetBool(IsWalkingForward, true);
        }

        public override void EnterState()
        {
            Debug.Log("Enter WalkBackwardState");
            PlayerStateManager.Instance.animator.SetBool(IsWalkingForward, false);
        }

        public override void UpdateState()
        {
            if (PlayerMovementController.Instance.IsMoving)
            {
                if(PlayerMovementController.Instance.IsMovingForward)
                    PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.WalkForwardState);
            }
            else if (!PlayerMovementController.Instance.IsMoving)
            {
                PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.IdleState);
            }
            else if (!PlayerMovementController.Instance.IsGrounded)
            {
                PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.PlayerJumpState);
            }
        }
    }
}