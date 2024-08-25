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

        [Header("Movement Settings")] 
        [SerializeField] private float moveSpeed = 5;
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

        private bool _isGrounded;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rotator = GetComponent<PlayerRotator>();
            _effectController = GetComponent<PlayerEffectController>();
            _inputHandler = new InputHandler();
            _mover = new Mover(_rigidbody, moveSpeed);
            _jumper = new Jumper(_rigidbody, jumpForce);
        }

        private void Update()
        {
            _isGrounded = detector.IsLayerDetected();
            _rotator.SetRotationToTarget(transform.position, MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z));
            _rotator.GetAim(MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z));
            _effectController.SetParticles(_isGrounded);
            if(_isGrounded && _inputHandler.GetJumpInput())
            {
                _jumper.Jump();
            }
        }

        private void FixedUpdate()
        {
            _mover.Move(_inputHandler.GetMovementDirection().x);
            if (!_isGrounded && _rigidbody.velocity.y <0)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x,_rigidbody.velocity.y - 0.1f,_rigidbody.velocity.z);
            }
        }
    }
}