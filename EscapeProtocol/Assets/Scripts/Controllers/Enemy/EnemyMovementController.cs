using Combat;
using Data.UnityObjects;
using Movements;
using UnityEngine;
using Utilities;

namespace Controllers.Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        [Header("Detector")]
        [SerializeField] private EnemyArea enemyArea;
        
        [Header("Movement Settings")]
        [SerializeField] private ObjectDetector objectDetector;
        [SerializeField] private float moveSpeed;
        
        [Header("Script References")]
        [SerializeField] private SoundDataScriptable soundData;
        [SerializeField] private HealthController healthController;
        [SerializeField] private EnemyGunController gunController;
        [SerializeField] private EnemyRotator enemyRotator;
        public bool IsWaiting => _isWaiting;
        public bool IsEnemyDetected => enemyArea.IsEnemyDetected;
        
        private Rigidbody _rb;
        private float _direction;
        private bool _isWaiting;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _isWaiting = false;
            enemyRotator.SetRotationToMoveDirection(moveSpeed);
        }
        private void Update()
        {
            Reverse();

            if (enemyArea.IsEnemyDetected)
            {
                _isWaiting = true;
                enemyRotator.SetRotationToTarget(transform.position,enemyArea.Enemy.transform.position);
                enemyRotator.GetAim(enemyArea.Enemy.transform.position);
            }
            else
            {
                _isWaiting = false;
            }
            if (!_isWaiting)
            {
                enemyRotator.SetRotationToMoveDirection(moveSpeed);
            }
        }

        private void FixedUpdate()
        {
            if (healthController.IsDead || _isWaiting) return;
            Move();
        }

        private void Move()
        {
            Vector3 move = new Vector3(moveSpeed, _rb.velocity.y, _rb.velocity.z);
            _rb.velocity = move;
        }
        // ReSharper disable Unity.PerformanceAnalysis

        private void Reverse()
        {
            if (objectDetector.IsLayerDetected())
            {
                moveSpeed = -moveSpeed;
            }
        }
    }
}
