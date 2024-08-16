using PlayerStateMachine;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private Animator animator;
        
        private IState _currentState;
        
        private readonly int _isMoving = Animator.StringToHash("IsMoving");
        private readonly int _isMovingForward = Animator.StringToHash("IsMovingForward");
        private readonly int _isGrounded = Animator.StringToHash("IsGrounded");

        private void Start()
        {
            _currentState = new IdleState();
            _currentState.EnterState(this,playerMovementController);
        }

        private void Update()
        {
            animator.SetBool(_isMoving, playerMovementController.IsMoving);
            animator.SetBool(_isMovingForward, playerMovementController.IsMovingForward);
            animator.SetBool(_isGrounded, playerMovementController.IsGrounded);
        }

        public void ChangeState(IState newState)
        {
            _currentState.ExitState(this,playerMovementController);
            _currentState = newState;
            _currentState.EnterState(this,playerMovementController);
        }
    }
}