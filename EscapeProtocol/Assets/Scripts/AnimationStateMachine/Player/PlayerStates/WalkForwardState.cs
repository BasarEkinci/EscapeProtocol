using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class WalkForwardState : BaseState
    {

        public override void ExitState()
        {

        }

        public override void EnterState()
        {

        }

        public override void UpdateState()
        {
            PlayerMovementController.Instance.Move();
            if (PlayerMovementController.Instance.IsMoving)
            {
                if (!PlayerMovementController.Instance.IsMovingForward)
                {
                    PlayerStateManager.Instance.ChangeState(new PlayerWalkBackwardState());
                }
                else if (PlayerMovementController.Instance.IsMovingForward && !PlayerMovementController.Instance.IsGrounded)
                {
                    PlayerStateManager.Instance.ChangeState(new PlayerJumpState());
                }
            }
            else
            {
                PlayerStateManager.Instance.ChangeState(new IdleState());
            }
        }
    }
}