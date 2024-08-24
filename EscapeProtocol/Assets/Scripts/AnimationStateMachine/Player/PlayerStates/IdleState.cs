using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class IdleState : BaseState
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public override void EnterState()
        {
            Debug.Log("Enter IdleState");
            PlayerStateManager.Instance.animator.SetBool(IsMoving,false);
        }
        public override void UpdateState()
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
            }else if (!PlayerMovementController.Instance.IsGrounded)
            {
                PlayerStateManager.Instance.ChangeState(PlayerStateManager.Instance.PlayerJumpState);
            }
        }
        public override void ExitState()
        {
            Debug.Log("Exit IdleState");
            PlayerStateManager.Instance.animator.SetBool(IsMoving,false);
        }
    }
}