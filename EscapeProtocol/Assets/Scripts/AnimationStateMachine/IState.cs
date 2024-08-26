using UnityEngine;

namespace AnimationStateMachine
{
    public interface IState<T>
    {
        void EnterState(T character);
        void UpdateState(T character);
        void ExitState(T character);
    }
}