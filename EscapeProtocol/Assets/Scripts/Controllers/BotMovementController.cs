using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utilities;

namespace Controllers
{
    public class BotMovementController : MonoSingleton<BotMovementController>
    {
        public bool IsGroundDetected => CheckGround();
        public bool IsWaiting => _isWaiting;
        public bool IsEnemyDetected => _isEnemyDetected;
        
        [SerializeField] private GameObject groundDetector;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float moveSpeed;
        
        private CharacterController _characterController;
        
        private float _moveSpeedMultiplier;
        private bool _isWaiting;
        private bool _isEnemyDetected;

        protected override void Awake()
        {
            base.Awake();
            _characterController = GetComponent<CharacterController>();
            _moveSpeedMultiplier = moveSpeed;
        }

        private void Start()
        {
            _isWaiting = false;
            _isEnemyDetected = false;
        }

        private void Update()
        {
            DetectGround().Forget();
            _characterController.Move(Vector3.right * (moveSpeed * Time.deltaTime));
        }


        private async UniTaskVoid DetectGround()
        {
            if (!CheckGround())
            {
                _isWaiting = true;
                moveSpeed = 0;
                await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
                transform.DORotate(Vector3.up * -90, 0.1f);
                _isWaiting = false;
                moveSpeed += _moveSpeedMultiplier;
                moveSpeed *= -1;
            }
        }

        private bool CheckGround()
        {
            return Physics.CheckSphere(groundDetector.transform.position, 0.1f, groundLayer);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundDetector.transform.position, 0.1f);
        }
    }
}
