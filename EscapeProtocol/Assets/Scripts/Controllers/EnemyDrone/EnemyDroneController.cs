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
        
        [Header("Movement Settings")]
        [SerializeField] private Ease easeType;
        [SerializeField] private float moveSpeed;
        
        private float _direction;
        private bool _isPlayerDetected;

        private void OnEnable()
        {
            transform.DOMoveY(transform.position.y + 0.2f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(easeType);
        }
        

        private void Update()
        {
            Reverse();
            HandlePlayerDetectedPhase();
            if (_isPlayerDetected) return;
            HandleMovement();
        }

        private void FixedUpdate()
        {
        }

        private void HandleMovement()
        {
            Vector3 moveVector = new Vector3(moveSpeed, 0, 0);
            transform.position += moveVector * Time.deltaTime;
        }

        private void Reverse()
        {
            if (objectDetector.IsLayerDetected())
            {
                moveSpeed = -moveSpeed;
                enemyRotator.SetRotationToMoveDirection(moveSpeed);
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
                enemyRotator.SetRotationToMoveDirection(moveSpeed);
            }
        }
    }
}