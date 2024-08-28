using Controllers.Player;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace AnimationStateMachine.Player
{
    public class PlayerJumpingState : IState<PlayerMovementController>
    {
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        private readonly SoundDataScriptable _soundData = Resources.Load<SoundDataScriptable>("Scriptables/Sounds/PlayerSoundData");

        public void EnterState(PlayerMovementController player)
        {
            SoundManager.PLaySound(_soundData,"Jump");
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
            SoundManager.PLaySound(_soundData,"Land");
        }
    }
}