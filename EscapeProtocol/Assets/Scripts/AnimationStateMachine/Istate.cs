using Controllers;

namespace AnimationStateMachine
{
    public interface IState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}