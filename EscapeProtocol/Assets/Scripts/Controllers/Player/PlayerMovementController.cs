using System.Collections.Generic;
using System.Linq;
using Data.UnityObjects;
using Inputs;
using Movements;
using UnityEngine;
using Utilities;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Sound Settings")] 
        [SerializeField] private SoundDataScriptable soundData;

        [Header("Jump Settings")] 
        [SerializeField] private ObjectDetector detector;
        
        [Header("Particle Effects")] 
        [SerializeField] private List<ParticleSystem> jumpParticles;
        [SerializeField] private List<ParticleSystem> landParticles;

        [Header("Movement Settings")] 
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float jumpForce = 6;
        
        #endregion
        #region Classes
        
        private Mover _mover;
        private Jumper _jumper;
        private PlayerRotator _rotator;
        private InputHandler _inputHandler;

        #endregion
        #region UnityComponents

        private Rigidbody _rigidbody;
        
        #endregion

        private bool _isGrounded;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rotator = GetComponent<PlayerRotator>();
            _inputHandler = new InputHandler();
            _mover = new Mover(_rigidbody, moveSpeed);
            _jumper = new Jumper(_rigidbody, jumpForce);
        }

        private void Update()
        {
            _isGrounded = detector.IsLayerDetected();
            _rotator.SetRotationToTarget(transform.position, MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z));
            _rotator.GetAim(MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z));
            SetParticles();
            if(_isGrounded && _inputHandler.GetJumpInput())
            {
                _jumper.Jump();
            }
        }

        private void FixedUpdate()
        {
            _mover.Move(_inputHandler.GetMovementDirection().x);
        }

        private void SetParticles()
        {
            if (!_isGrounded)
            {
                foreach (var particle in jumpParticles.Where(particle => particle.isStopped))
                {
                    particle.Play();
                }

                foreach (var particle in landParticles.Where(particle => !particle.isPlaying))
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
        }
    }
}