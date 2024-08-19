using Controllers;
using UnityEngine;
using Utilities;

namespace AnimationStateMachine.BotAnimationStates
{
    public class BotAnimationController : MonoSingleton<BotAnimationController>
    {
        [SerializeField] private Animator animator;

        private IState _currentState;
        private readonly int _isWaiting = Animator.StringToHash("isWaiting");

        private void Update()
        {
            animator.SetBool(_isWaiting, BotMovementController.Instance.IsWaiting);
        }
        
        public void ChangeState(IState newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
    }
}