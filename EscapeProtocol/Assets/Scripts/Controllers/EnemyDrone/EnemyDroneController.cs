using Data.UnityObjects;
using DG.Tweening;
using Movements;
using Objects;
using UnityEngine;
using Utilities;

namespace Controllers.EnemyDrone
{
    public class EnemyDroneController : MonoBehaviour
    {
        [Header("Enemy Detection")]        
        [SerializeField] private ObjectDetector objectDetector;
        [SerializeField] private EnemyArea enemyArea;
        
        [Header("Script References")]
        [SerializeField] private EnemyRotator enemyRotator;
        [SerializeField] private EnemyDroneGunController gunController;
        
        [Header("Movement Settings")]
        [SerializeField] private Ease easeType;
        
        [Header("Data")]
        [SerializeField] private EnemyDataScriptable enemyData;
        
        private float _direction;
        private bool _isPlayerDetected;
        private float _moveSpeed;

        private void Awake()
        {
            _moveSpeed = 2f;
        }

        private void OnEnable()
        {
            transform.DOMoveY(transform.position.y + 0.2f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(easeType);
        }
        

        private void Update()
        {
            Reverse();
            HandlePlayerDetectedPhase();
            if (_isPlayerDetected)
            {
                gunController.Fire();
                return;
            }
            HandleMovement();
        }
        

        private void HandleMovement()
        {
            Vector3 moveVector = new Vector3(_moveSpeed, 0, 0);
            transform.position += moveVector * Time.deltaTime;
        }

        private void Reverse()
        {
            if (objectDetector.IsLayerDetected())
            {
                _moveSpeed = -_moveSpeed;
                enemyRotator.SetRotationToMoveDirection(_moveSpeed);
            }
        }
        
        private void HandlePlayerDetectedPhase()
        {
            if (enemyArea.IsPlayerInArea && enemyArea.Target.activeSelf)
            {
                enemyRotator.SetRotationToTarget(transform.position, enemyArea.Target.transform.position);
                enemyRotator.GetAim(enemyArea.Target.transform.position);
                _isPlayerDetected = true;
            }
            else
            {
                _isPlayerDetected = false;
                enemyRotator.SetRotationToMoveDirection(_moveSpeed);
            }
        }
    }
}