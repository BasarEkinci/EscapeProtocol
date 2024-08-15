using Controllers;

namespace PlayerStateMachine
{
    public interface IState
    {
        void EnterState(PlayerAnimationController player,PlayerMovementController playerMovementController);
        void UpdateState(PlayerAnimationController player,PlayerMovementController playerMovementController);
        void ExitState(PlayerAnimationController player,PlayerMovementController playerMovementController);
    }
}