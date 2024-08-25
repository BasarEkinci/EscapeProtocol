using Controllers.Player;

namespace AnimationStateMachine.Player
{
    public interface IState
    {
        void EnterState(PlayerMovementController player);
        void UpdateState(PlayerMovementController player);
        void ExitState(PlayerMovementController player);
    }
}