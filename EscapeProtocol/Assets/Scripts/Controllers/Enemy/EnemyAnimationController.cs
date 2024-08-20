using System.Collections.Generic;
using AnimationStateMachine;
using UnityEngine;
using Utilities;

namespace Controllers.Enemy
{
    public class EnemyAnimationController : MonoSingleton<EnemyAnimationController>
    {
        [SerializeField] private Animator animator;

        private IState _currentState;
        
        private readonly Dictionary<string,int> _animatorParameters = new Dictionary<string, int>{
            { "isWaiting", Animator.StringToHash("isWaiting") },
        };
        
        private void Update()
        {
            animator.SetBool(_animatorParameters["isWaiting"], EnemyMovementController.Instance.IsWaiting);
        }
        public void ChangeState(IState newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
    }
}