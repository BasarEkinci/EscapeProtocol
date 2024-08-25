using System.Runtime.Serialization;
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
        public bool IsMovingBackward => _isMovingBackward;
        public bool IsGrounded => _isGrounded;

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

        private IState _currentState;
        private Animator _animator;

        private bool _isGrounded;
        private bool _isMoving;
        private bool _isMovingForward;
        private bool _isMovingBackward;

        #endregion

        #region Animation Variables

        private static readonly int Moving = Animator.StringToHash("IsMoving");
        private static readonly int MovingForward = Animator.StringToHash("IsMovingForward");
        private static readonly int MovingBackward = Animator.StringToHash("IsMovingBackward");
        private static readonly int Grounded = Animator.StringToHash("IsGrounded");

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
            SetValuesToAnimator();
            _currentState.UpdateState(this);
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

        internal void HandleMovement()
        {
            _isMoving = Mathf.RoundToInt(_inputHandler.GetMovementDirection().x) != 0;
        }

        internal void HandleMovementDirection()
        {
            if (_inputHandler.GetMovementDirection().x == 0) return;
                
            if (transform.position.x > MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z).x)
            {
                if (_rigidbody.velocity.x > 0)
                {
                    _isMovingForward = false;
                    _isMovingBackward = true;
                }
                else
                {
                    _isMovingForward = true;
                    _isMovingBackward = false;
                }
            }
            else
            {
                if (_rigidbody.velocity.x > 0)
                {
                    _isMovingForward = true;
                    _isMovingBackward = false;
                }
                else
                {
                    _isMovingForward = false;
                    _isMovingBackward = true;
                }
            }
        }

        internal void HandleJumping()
        {
            _isGrounded = detector.IsLayerDetected() && _inputHandler.GetJumpInput();
        }

        internal void ChangeState(IState state)
        {
            _currentState.ExitState(this);
            _currentState = state;
            _currentState.EnterState(this);
        }

        private void SetValuesToAnimator()
        {
            _animator.SetBool(Moving, _inputHandler.GetMovementDirection().x != 0);
            _animator.SetBool(MovingForward, _isMovingForward);
            _animator.SetBool(MovingBackward, _isMovingBackward);
            _animator.SetBool(Grounded, _isGrounded);
        }
    }
}