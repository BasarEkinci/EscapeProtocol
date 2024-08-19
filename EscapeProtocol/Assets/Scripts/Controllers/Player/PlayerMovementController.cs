using System.Collections.Generic;
using System.Linq;
using Combat;
using Data.UnityObjects;
using Inputs;
using Managers;
using Movements;
using UnityEngine;
using Utilities;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoSingleton<PlayerMovementController>, IDamageable
    {
        public bool IsMovingForward => _isMovingForward;
        public bool IsMoving => _inputHandler.GetMovementDirection().x != 0;
        public bool IsGrounded => _isGrounded;
        private InputHandler _inputHandler;
        
        [SerializeField] private int maxHealth;
        [SerializeField] private PlayerRotator playerRotator;
        [SerializeField] private SoundDataScriptable soundData;
        [Header("Jump Settings")]
        [SerializeField] private Transform playerFoot;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpForce;
        
        [SerializeField] private List<ParticleSystem> jumpParticles;
        [SerializeField] private List<ParticleSystem> landParticles;
        
        private float _gravity = -15.81f;

        [Header("Movement Settings")]
        [SerializeField] private float speed;

        private Vector3 _velocity;

        private Rigidbody _rb;
        private CharacterController _characterController;
        private Vector3 _moveDirection;
        private bool _isGrounded;
        private bool _isMovingForward;
        private int _currentHealth;

        protected override void Awake()
        {
            base.Awake();
            _inputHandler = new InputHandler();
            _characterController = GetComponent<CharacterController>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        private void Update()
        {
            _isMovingForward = playerRotator.IsMovingForward(_characterController.velocity);
            playerRotator.GetMousePosition();
            playerRotator.RotatePlayer();
            playerRotator.GetAim();
            SetEffects();
            HandleJump();
            Move();
        }


        private void Move()
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            Vector3 move = new Vector3(_inputHandler.GetMovementDirection().x, 0, 0);
            _characterController.Move(move * (speed * Time.deltaTime));
        }
        private void HandleJump()
        {
            _isGrounded = Physics.CheckSphere(playerFoot.position, groundDistance, groundLayer);
            if (_inputHandler.GetJumpInput() && _isGrounded)
            {
                SoundManager.PLaySound(soundData,"Jump",null,1);
                _velocity.y = Mathf.Sqrt(jumpForce * -2f * _gravity);
            }
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void SetEffects()
        {
            if (_inputHandler.GetMovementDirection().x != 0 && !_isGrounded)
            {
                foreach (var particle in landParticles.Where(particle => particle.isStopped))
                {
                    particle.Play();
                }
            }
            else
            {
                foreach (var particle in landParticles.Where(particle => particle.isPlaying))
                {
                    particle.Stop();
                }
            }
            
            if(_isGrounded && _inputHandler.GetJumpInput())
            {
                foreach (var particle in jumpParticles.Where(particle => particle.isStopped))
                {
                    particle.Play();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Min(_currentHealth, 0);
        }
    }
}