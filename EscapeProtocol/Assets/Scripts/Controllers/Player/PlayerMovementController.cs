using System.Collections.Generic;
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
        public bool IsMoving => _movementDirectionX != 0;
        public bool IsGrounded => _isGrounded;
        private InputHandler _inputHandler;
        
        [Header("Sound Settings")]
        [SerializeField] private SoundDataScriptable soundData;
        
        [Header("Health Settings")]
        [SerializeField] private int maxHealth;
        
        [Header("Jump Settings")]
        [SerializeField] private Transform playerFoot;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravity = -15.81f;
        
        [Header("Particle Effects")]
        [SerializeField] private List<ParticleSystem> jumpParticles;
        [SerializeField] private List<ParticleSystem> landParticles;
        
        [Header("Movement Settings")]
        [SerializeField] private float speed;
        [SerializeField] private PlayerRotator playerRotator;

        private Vector3 _velocity;
        
        private CharacterController _characterController;
        private bool _isGrounded;
        private bool _isMovingForward;
        private int _currentHealth;
        private float _movementDirectionX;

        protected override void Awake()
        {
            base.Awake();
            _inputHandler = new InputHandler();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        private void Update()
        {
            _movementDirectionX = _inputHandler.GetMovementDirection().x;
            _isGrounded = Physics.CheckSphere(playerFoot.position, groundDistance, groundLayer);
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
            if (_inputHandler.GetJumpInput() && _isGrounded)
            {
                SoundManager.PLaySound(soundData,"Jump",null,1);
                _velocity.y = Mathf.Sqrt(-jumpForce * gravity);
            }
            _velocity.y += gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void SetEffects()
        {
            if (_movementDirectionX != 0 && !_isGrounded)
            {
                foreach (var particle in landParticles)
                {
                    if(!particle.isPlaying)
                        particle.Play();
                }
            }
            else
            {
                foreach (var particle in landParticles)
                {
                    if(particle.isPlaying)
                        particle.Stop();
                }
            }
            
            if(_isGrounded && _inputHandler.GetJumpInput())
            {
                foreach (var particle in jumpParticles)
                {
                    if(!particle.isPlaying)
                        particle.Play();
                }
            }
        }
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Max(_currentHealth, 0);
        }
    }
}