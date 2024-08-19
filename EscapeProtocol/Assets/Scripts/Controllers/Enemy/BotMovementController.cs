using Combat;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utilities;

namespace Controllers.Enemy
{
    public class BotMovementController : MonoSingleton<BotMovementController>
    {
        [SerializeField] private EnemyArea enemyArea;
        [SerializeField] private Transform bodyTransform;
        [SerializeField] private GameObject groundDetector;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float moveSpeed;
        
        public bool IsWaiting => _isWaiting;
        public bool IsEnemyDetected => enemyArea.IsEnemyDetected;
        
        private Rigidbody _rb;
        private Vector3 _velocity;
        private float _speedMultiplier; 
        private bool _isWaiting;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _isWaiting = false;
        }

        private void Update()
        {
            DetectGround().Forget();
            LookAtEnemy();
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
                _isWaiting = true;
                moveSpeed *= -1;
                await UniTask.Delay(1000);
                _isWaiting = false;
                if(moveSpeed > 0)
                    transform.DORotate(Vector3.up * 90, 0.1f);
                else
                    transform.DORotate(Vector3.up * -90, 0.1f);
            }
        }
        
        private bool CheckGround()
        {
            return Physics.CheckSphere(groundDetector.transform.position, 0.1f, groundLayer);
        }
        
        private void LookAtEnemy()
        {
            if (enemyArea.IsEnemyDetected)
            {
                _isWaiting = true;
                if(transform.position.x < enemyArea.Enemy.transform.position.x)
                    transform.DORotate(Vector3.up * 90, 0.1f);
                else
                    transform.DORotate(Vector3.up * -90, 0.1f);
                bodyTransform.LookAt(enemyArea.Enemy.transform.position + new Vector3(0,1.5f,0));
            }
            else
            {
                if(moveSpeed > 0)
                {
                    transform.DORotate(Vector3.up * 90, 0.1f);
                    bodyTransform.DORotate(new Vector3(5,90,0), 0.1f);
                }
                else
                {
                    transform.DORotate(Vector3.up * -90, 0.1f);
                    bodyTransform.DORotate(new Vector3(5,-90,0), 0.1f);
                }
                _isWaiting = false;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundDetector.transform.position, 0.1f);
        }
    }
}
