using AnimationStateMachine.Player.PlayerStates;
using Controllers.Player;
using UnityEngine;
using Utilities;

namespace AnimationStateMachine.Player
{
    public class PlayerStateManager : MonoSingleton<PlayerStateManager>
    {
        private BaseState _currentState;

        private IdleState IdleState;
        private WalkForwardState WalkForwardState;
        private PlayerWalkBackwardState PlayerWalkBackwardState;
        private PlayerJumpState PlayerJumpState;
        
        
        public Animator animator;

        protected override void Awake()
        {
            base.Awake();
            IdleState = new IdleState();
            WalkForwardState = new WalkForwardState();
            PlayerWalkBackwardState = new PlayerWalkBackwardState();
            PlayerJumpState = new PlayerJumpState();
        }

        private void Start()
        {
            _currentState = new IdleState();
            _currentState.EnterState();
        }

        private void Update()
        {
            _currentState.UpdateState();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void ChangeState(BaseState state)
        {
            _currentState.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }
    }
}