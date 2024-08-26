using System.Runtime.Serialization;
using AnimationStateMachine;
using AnimationStateMachine.Player;
using Data.UnityObjects;
using Inputs;
using Movements;
using UnityEngine;
using Utilities;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Public Fields

        public bool IsMoving => _isMoving;
        public bool IsMovingForward => _isMovingForward;
        public bool IsGrounded => _isGrounded;
        public Animator Animator => _animator;

        #endregion
        
        #region Serialized Fields

        [Header("Sound Settings")] [SerializeField]
        private SoundDataScriptable soundData;

        [Header("Jump Settings")] [SerializeField]
        private ObjectDetector detector;

        [Header("Movement Settings")] [SerializeField]
        private float moveSpeed = 5;

        [SerializeField] private float jumpForce = 6;

        #endregion

        #region Classes

        private Mover _mover;
        private Jumper _jumper;
        private PlayerRotator _rotator;
        private InputHandler _inputHandler;
        private PlayerEffectController _effectController;

        #endregion

        #region UnityComponents

        private Rigidbody _rigidbody;

        #endregion

        #region Private Variables

        private IState<PlayerMovementController> _currentState;
        private Animator _animator;

        private bool _isGrounded;
        private bool _isMoving;
        private bool _isMovingForward;
        #endregion
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _rotator = GetComponent<PlayerRotator>();
            _effectController = GetComponent<PlayerEffectController>();
            _inputHandler = new InputHandler();
            _currentState = new PlayerIdleState();
            _mover = new Mover(_rigidbody, moveSpeed);
            _jumper = new Jumper(_rigidbody, jumpForce);
        }

        private void Update()
        {
            _isGrounded = detector.IsLayerDetected();
            _rotator.SetRotationToTarget(transform.position,
                MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z));
            _rotator.GetAim(MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z));
            _effectController.SetParticles(_isGrounded);
            if (_isGrounded && _inputHandler.GetJumpInput())
            {
                _jumper.Jump();
            }
            _currentState.UpdateState(this);
            HandleMovementDirection();
        }

        private void FixedUpdate()
        {
            _mover.Move(_inputHandler.GetMovementDirection().x);
            if (!_isGrounded && _rigidbody.velocity.y < 0)
            {
                
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y - 0.1f,
                    _rigidbody.velocity.z);
            }
        }

        private void HandleMovementDirection()
        {
            Vector2 movementDirection = _inputHandler.GetMovementDirection();
            float cursorWorldX = MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z).x;
            float playerPositionX = transform.position.x;
            float playerVelocityX = _rigidbody.velocity.x;

            if (movementDirection.x != 0)
            {
                _isMoving = true;
                _isMovingForward = (playerPositionX < cursorWorldX && playerVelocityX > 0) || (playerPositionX > cursorWorldX && playerVelocityX < 0);
            }
            else
            {
                _isMoving = false;
            }
        }
        
        internal void ChangeState(IState<PlayerMovementController> state)
        {
            _currentState?.ExitState(this);
            _currentState = state;
            _currentState.EnterState(this);
        }
    }
}