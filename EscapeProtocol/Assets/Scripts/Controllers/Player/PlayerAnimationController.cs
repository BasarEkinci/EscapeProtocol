using System.Collections.Generic;
using AnimationStateMachine;
using AnimationStateMachine.PlayerAnimationStates;
using UnityEngine;
using Utilities;

namespace Controllers.Player
{
    public class PlayerAnimationController : MonoSingleton<PlayerAnimationController>
    {
        [SerializeField] private Animator animator;
        
        private IState _currentState;
        
        private readonly Dictionary<string,int> _animatorParameters = new Dictionary<string, int>{
            { "IsMoving", Animator.StringToHash("IsMoving") },
            { "IsMovingForward", Animator.StringToHash("IsMovingForward") },
            { "IsGrounded", Animator.StringToHash("IsGrounded") }
        };

        private void Start()
        {
            _currentState = new IdleState();
            _currentState.EnterState();
        }

        private void Update()
        {
            animator.SetBool(_animatorParameters["IsMoving"], PlayerMovementController.Instance.IsMoving);
            animator.SetBool(_animatorParameters["IsMovingForward"], PlayerMovementController.Instance.IsMovingForward);
            animator.SetBool(_animatorParameters["IsGrounded"], PlayerMovementController.Instance.IsGrounded);
        }

        public void ChangeState(IState newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
    }
}