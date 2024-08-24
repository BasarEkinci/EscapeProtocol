using UnityEngine;

namespace AnimationStateMachine.Player
{
    public abstract class BaseState
    {
        public abstract void ExitState();
        public abstract void EnterState();
        public abstract void UpdateState();
    }
}