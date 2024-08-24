using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class PlayerWalkBackwardState : BaseState
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
                if(PlayerMovementController.Instance.IsMovingForward)
                    PlayerStateManager.Instance.ChangeState(new WalkForwardState());
            }
            else if (!PlayerMovementController.Instance.IsMoving)
            {
                PlayerStateManager.Instance.ChangeState(new IdleState());
            }
            else if (!PlayerMovementController.Instance.IsGrounded)
            {
                PlayerStateManager.Instance.ChangeState(new PlayerJumpState());
            }
        }
    }
}