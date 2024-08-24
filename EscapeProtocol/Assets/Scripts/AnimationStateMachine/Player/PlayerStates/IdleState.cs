using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class IdleState : BaseState
    {
        public override void EnterState()
        {

        }
        public override void UpdateState()
        {
            PlayerMovementController.Instance.Move();
            if (PlayerMovementController.Instance.IsMoving)
            {
                if (PlayerMovementController.Instance.IsMovingForward)
                {
                    PlayerStateManager.Instance.ChangeState(new WalkForwardState());
                }
                else
                {
                    PlayerStateManager.Instance.ChangeState(new PlayerWalkBackwardState());
                }
            }else if (!PlayerMovementController.Instance.IsGrounded)
            {
                PlayerStateManager.Instance.ChangeState(new PlayerJumpState());
            }
        }
        public override void ExitState()
        {

        }
    }
}