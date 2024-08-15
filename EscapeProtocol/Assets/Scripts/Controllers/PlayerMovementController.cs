using Inputs;
using Movements;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerRotator playerRotator;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private Transform playerFoot;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float jumpForce;
        public bool IsMoving => HorizontalMovement() != 0;
        public bool IsMovingForward => _isMovingForward;
        public bool IsGrounded => IsPlayerGrounded();
        private InputHandler _inputHandler;

        private bool _isJumped;
        private bool _isGrounded;
        private Rigidbody _rb;
        private bool _isMovingForward;
        
        private void Awake()
        {
            _inputHandler = new InputHandler();
            _rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            _isMovingForward = playerRotator.IsMovingForward();
            playerMover.Move(HorizontalMovement());
            playerRotator.GetMousePosition();
            playerRotator.RotatePlayer();
            playerRotator.GetAim();
            HandleJump();
        }

        private void HandleJump()
        {
            if (IsPlayerGrounded() && _inputHandler.GetJumpInput())
            {
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        
        private float HorizontalMovement()
        {
            return _inputHandler.GetMovementDirection().x;
        }

        private bool IsPlayerGrounded()
        {
            float sphereRadius = 0.3f;
            _isGrounded = Physics.CheckSphere(playerFoot.position, sphereRadius, groundLayer);
            return _isGrounded;
        }
    }
}