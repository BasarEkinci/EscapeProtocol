﻿using AnimationStateMachine;
using AnimationStateMachine.Drone;
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
        
        [Header("Object References")]
        [SerializeField] private EnemyRotator enemyRotator;
        [SerializeField] private EnemyDroneGunController gunController;
        
        [Header("Movement Settings")]
        [SerializeField] private Ease easeType;
        
        [Header("Data")]
        [SerializeField] private EnemyDataScriptable enemyData;
        [SerializeField] private SoundDataScriptable soundData;
        
        public bool IsPlayerDetected => _isPlayerDetected;
        public AudioSource AudioSource => _audioSource;
        
        
        private IState<EnemyDroneController> _currentState;
        private float _direction;
        private bool _isPlayerDetected;
        private float _moveSpeed;
        private AudioSource _audioSource;

        private void Awake()
        {
            _moveSpeed = 2f;
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _currentState = new DronePatrolState();
        }
        

        private void Update()
        {
            _currentState.UpdateState(this);
        }
        
        internal void HandleMovement()
        {
            Vector3 moveVector = new Vector3(_moveSpeed, 0, 0);
            transform.position += moveVector * Time.deltaTime;
        }

        internal void Reverse()
        {
            if (objectDetector.IsLayerDetected())
            {
                _moveSpeed = -_moveSpeed;
                enemyRotator.SetRotationToMoveDirection(_moveSpeed);
            }
        }
        
        internal void HandlePlayerDetectedPhase()
        {
            if (enemyArea.IsPlayerInArea && enemyArea.Target.activeSelf)
            {
                enemyRotator.SetRotationToTarget(transform.position, enemyArea.Target.transform.position);
                enemyRotator.GetAim(enemyArea.Target.transform.position);
                _isPlayerDetected = true;
                gunController.Fire();
            }
            else
            {
                _isPlayerDetected = false;
                enemyRotator.SetRotationToMoveDirection(_moveSpeed);
            }
        }

        internal void ChangeState(IState<EnemyDroneController> state)
        {
            _currentState.ExitState(this);
            _currentState = state;
            _currentState.EnterState(this);
        }
    }
}