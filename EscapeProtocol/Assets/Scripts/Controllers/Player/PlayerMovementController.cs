using System.Collections.Generic;
using Data.UnityObjects;
using Inputs;
using Managers;
using Movements;
using UnityEngine;
using Utilities;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoSingleton<PlayerMovementController>
    {
        public bool IsMovingForward => _isMovingForward;
        public bool IsMoving => _movementDirectionX != 0;
        public bool IsGrounded => _isGrounded;
        private InputHandler _inputHandler;
        
        [Header("Sound Settings")]
        [SerializeField] private SoundDataScriptable soundData;
        
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

        private void Update()
        {
            _movementDirectionX = _inputHandler.GetMovementDirection().x;
            _isGrounded = Physics.CheckSphere(playerFoot.position, groundDistance, groundLayer);
            _isMovingForward = playerRotator.IsMovingForward(_characterController.velocity);
            playerRotator.RotatePlayer();
            playerRotator.GetAim();
            SetEffects();
            HandleJump();
            Move();
        }
        private void Move()
        {
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
            if(!_isGrounded)
            {
                if(_velocity.y > 0)
                {
                    _velocity.y += gravity * Time.deltaTime;
                }
                else
                {
                    const float gravityMultiplier = 1.5f;
                    _velocity.y += gravity * gravityMultiplier * Time.deltaTime;
                }
            }
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
    }
}