using Controllers.Player;
using UnityEngine;

namespace AnimationStateMachine.Player.PlayerStates
{
    public class PlayerJumpState : BaseState
    {
        
        public override void ExitState()
        {
            
        }

        public override void EnterState()
        {

        }

        public override void UpdateState()
        {
            PlayerMovementController.Instance.HandleJump();
            if (!PlayerMovementController.Instance.IsGrounded)
            {
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
                }
                else
                {
                    PlayerStateManager.Instance.ChangeState(new IdleState());
                }
            }
        }
    }
}