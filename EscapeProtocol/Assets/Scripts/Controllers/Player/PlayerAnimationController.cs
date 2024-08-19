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
        
        private readonly int _isMoving = Animator.StringToHash("IsMoving");
        private readonly int _isMovingForward = Animator.StringToHash("IsMovingForward");
        private readonly int _isGrounded = Animator.StringToHash("IsGrounded");

        private void Start()
        {
            _currentState = new IdleState();
            _currentState.EnterState();
        }

        private void Update()
        {
            animator.SetBool(_isMoving, PlayerMovementController.Instance.IsMoving);
            animator.SetBool(_isMovingForward, PlayerMovementController.Instance.IsMovingForward);
            animator.SetBool(_isGrounded, PlayerMovementController.Instance.IsGrounded);
        }

        public void ChangeState(IState newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
    }
}