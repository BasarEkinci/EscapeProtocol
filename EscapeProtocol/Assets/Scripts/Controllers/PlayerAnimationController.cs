using PlayerStateMachine;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private Animator animator;
        
        private IState _currentState;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsMovingForward = Animator.StringToHash("IsMovingForward");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

        private void Start()
        {
            _currentState = new IdleState();
            _currentState.EnterState(this,playerMovementController);
        }

        private void Update()
        {
            animator.SetBool(IsMoving, playerMovementController.IsMoving);
            animator.SetBool(IsMovingForward, playerMovementController.IsMovingForward);
            animator.SetBool(IsJumping, !playerMovementController.IsGrounded);
        }

        public void ChangeState(IState newState)
        {
            _currentState.ExitState(this,playerMovementController);
            _currentState = newState;
            _currentState.EnterState(this,playerMovementController);
        }
    }
}