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
        private Rigidbody _rb;
        private Vector3 _velocity;
        private float _speedMultiplier;
        private float _moveSpeedMultiplier;
        private bool _isWaiting;
        private bool _isEnemyDetected;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _isWaiting = false;
            _isEnemyDetected = false;
        }

        private void Update()
        {
            DetectGround().Forget();
            Move();
        }

        private void Move()
        {
            _speedMultiplier = _isWaiting ? 0 : 1;
            
            Vector3 move = new Vector3(moveSpeed * _speedMultiplier, _rb.velocity.y, _rb.velocity.z);
            _rb.velocity = move;
        }

        private async UniTaskVoid DetectGround()
        {
            if (!CheckGround())
            {
                Debug.Log("Gap Detected");
                _isWaiting = true;
                moveSpeed *= -1;
                await UniTask.Delay(1000);
                _isWaiting = false;
                if(moveSpeed > 0)
                    transform.DORotate(Vector3.up * 90, 0.1f);
                else
                    transform.DORotate(Vector3.down * 90, 0.1f);
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
