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

        [Header("Particle Effects")] 
        [SerializeField] private List<ParticleSystem> jumpParticles;
        [SerializeField] private List<ParticleSystem> landParticles;

        [Header("Movement Settings")] 
        [SerializeField] private float speed;

        [SerializeField] private PlayerRotator playerRotator;
        private Vector3 _velocity;
        private Rigidbody _rigidbody;
        private bool _isGrounded;
        private bool _isMovingForward;
        private int _currentHealth;
        private float _movementDirectionX;

        protected override void Awake()
        {
            base.Awake();
            _inputHandler = new InputHandler();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _isGrounded = Physics.Raycast(playerFoot.position, Vector3.down, groundDistance, groundLayer);
            _movementDirectionX = _inputHandler.GetMovementDirection().x;
            _isMovingForward = playerRotator.IsMovingForward(_rigidbody.velocity);
            playerRotator.RotatePlayer();
            playerRotator.GetAim();
            SetEffects();
            _movementDirectionX = _inputHandler.GetMovementDirection().x;
            HandleJump();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(speed * _movementDirectionX, _rigidbody.velocity.y, 0);
        }

        private void HandleJump()
        {
            if (_inputHandler.GetJumpInput() && _isGrounded)
            {
                SoundManager.PLaySound(soundData, "Jump",null,1);
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpForce, 0);
            }
            if (!_isGrounded)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y - 0.01f, 0);
            }
        }

        private void SetEffects()
        {
            if (_movementDirectionX != 0 && !_isGrounded)
            {
                foreach (var particle in landParticles)
                {
                    if (!particle.isPlaying)
                        particle.Play();
                }
            }
            else
            {
                foreach (var particle in landParticles)
                {
                    if (particle.isPlaying)
                        particle.Stop();
                }
            }

            if (_isGrounded && _inputHandler.GetJumpInput())
            {
                foreach (var particle in jumpParticles)
                {
                    if (!particle.isPlaying)
                        particle.Play();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(playerFoot.position, Vector3.down * groundDistance);
        }
    }
}