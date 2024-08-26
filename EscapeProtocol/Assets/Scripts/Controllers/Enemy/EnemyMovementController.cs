using AnimationStateMachine;
using AnimationStateMachine.Enemy;
using Combat;
using Data.UnityObjects;
using Movements;
using UnityEngine;
using Utilities;

namespace Controllers.Enemy
{
    public class EnemyMovementController : MonoBehaviour
    {
        #region Public Fields
        public bool IsPlayerDetected => enemyArea.IsPlayerDetected;
        public Animator Animator => _animator;
        
        #endregion

        #region Serilaized Fields
        
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

        #endregion

        #region Private Variables
        
        private float _direction;
        private bool _isPlayerDetected;

        private Animator _animator;
        private Rigidbody _rb;
        private IState<EnemyMovementController> _currentState;
        
        #endregion
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _isPlayerDetected = false;
            _currentState = new EnemyWalkingState();
            enemyRotator.SetRotationToMoveDirection(moveSpeed);
        }
        private void Update()
        {
            Reverse();
            HandlePlayerDetectedPhase();
            _currentState.UpdateState(this);
        }

        private void FixedUpdate()
        {
            if (healthController.IsDead || _isPlayerDetected) return;
            Move();
        }

        private void Move()
        {
            Vector3 move = new Vector3(moveSpeed, _rb.velocity.y, _rb.velocity.z);
            _rb.velocity = move;
        }
        private void Reverse()
        {
            if (objectDetector.IsLayerDetected())
            {
                moveSpeed = -moveSpeed;
            }
        }

        private void HandlePlayerDetectedPhase()
        {
            if (enemyArea.IsPlayerDetected)
            {
                _isPlayerDetected = true;
                enemyRotator.SetRotationToTarget(transform.position,enemyArea.Target.transform.position);
                enemyRotator.GetAim(enemyArea.Target.transform.position);
            }
            else
            {
                _isPlayerDetected = false;
            }
            if (!_isPlayerDetected)
            {
                enemyRotator.SetRotationToMoveDirection(moveSpeed);
            }
        }

        internal void ChangeState(IState<EnemyMovementController> state)
        {
            _currentState?.ExitState(this);
            _currentState = state;
            _currentState.EnterState(this);
        }
    }
}
