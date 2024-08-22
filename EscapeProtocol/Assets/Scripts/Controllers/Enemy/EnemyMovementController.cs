using Combat;
using Cysharp.Threading.Tasks;
using Data.UnityObjects;
using DG.Tweening;
using Movements;
using UnityEngine;
using Utilities;

namespace Controllers.Enemy
{
    public class EnemyMovementController : MonoSingleton<EnemyMovementController>
    {
        [Header("Detector")]
        [SerializeField] private EnemyArea enemyArea;
        
        [Header("Movement Settings")]
        [SerializeField] private LayerDetector layerDetector;
        [SerializeField] private float moveSpeed;
        
        [Header("Script References")]
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private HealthController healthController;
        [SerializeField] private EnemyGunController gunController;
        [SerializeField] private EnemyRotator enemyRotator;
        public bool IsWaiting => _isWaiting;
        public bool IsEnemyDetected => enemyArea.IsEnemyDetected;
        
        private Rigidbody _rb;
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
            if (healthController.IsDead) return;
            DetectGround();
            if (enemyArea.IsEnemyDetected)
            {
                _isWaiting = true;
                _speedMultiplier = 0;
                enemyRotator.GetAimToPlayer(transform.position, enemyArea.Enemy.transform.position);
            }
            else
            {
                _isWaiting = false;
                _speedMultiplier = 1;
                enemyRotator.SetRotationToMoveDirection(moveSpeed);
            }
        }

        private void FixedUpdate()
        {
            if (healthController.IsDead) return;
            Move();
        }

        private void Move()
        {
            if(_rb)
            {
                Vector3 move = new Vector3(moveSpeed * _speedMultiplier, _rb.velocity.y, _rb.velocity.z);
                _rb.velocity = move;
            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private async void DetectGround()
        {
            if (!layerDetector.IsLayerDetected())
            {
                _isWaiting = true;
                moveSpeed *= -1;
                await UniTask.Delay(2000);
                _isWaiting = false;
                if(moveSpeed > 0)
                    transform.DORotate(Vector3.up * 90, 0.1f);
                else
                    transform.DORotate(Vector3.up * -90, 0.1f);
            }
        }
    }
}
