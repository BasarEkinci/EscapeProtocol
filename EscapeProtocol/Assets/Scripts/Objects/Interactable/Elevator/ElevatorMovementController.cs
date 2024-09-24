using System;
using System.Collections.Generic;
using Data.UnityObjects;
using Inputs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


namespace Objects.Interactable.Elevator
{
    public class ElevatorMovementController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Movement Settings")] 
        [SerializeField] private List<Transform> elevatorFloors;
        [SerializeField] private float speed;
        
        [Header("Data")]
        [SerializeField] private SoundDataScriptable soundData;
        
        [Header("Text")]
        [SerializeField] private TMP_Text elevatorText;
        
        [Header("References")]
        [SerializeField] private TriggerElevator trigger;
        #endregion
        public bool IsMoving => _isMoving;
        

        #region Private Variables
        private bool _isPlayerInside;
        private bool _isMoving;
        private bool _canMove;
        private float _direction;
        private Transform _targetFloor;
        #endregion
        
        private AudioSource _audioSource;
        private InputHandler _inputHandler;
        private void Awake()
        {
            _inputHandler = new InputHandler();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            SetTargetFloor();
            _isMoving = false;
        }

        private void Update()
        {
            if (trigger.IsPlayerInside && _inputHandler.GetInteractInput())
            {
                _canMove = true;
            }

            if (_isMoving)
            {
                elevatorText.text = _direction switch
                {
                    1 => "/\\",
                    -1 => "\\/",
                    _ => elevatorText.text
                };
            }
        }

        private void FixedUpdate()
        {
            HandleElevatorMovement();
        }
        
        private void HandleElevatorMovement()
        {
            float distance = Vector3.Distance(transform.position, _targetFloor.position);
            if (distance <= 0.01)
            {
               _canMove = false;
               _isMoving = false;
               SetTargetFloor();
            }
            if (_canMove)
            {
                _isMoving = true;
                transform.position += new Vector3(0, _direction * speed * Time.deltaTime, 0);
            }
        }

        private void SetTargetFloor()
        {
            float distanceBetweenWith0 = Vector3.Distance(transform.position, elevatorFloors[0].position);
            float distanceBetweenWith1 = Vector3.Distance(transform.position, elevatorFloors[1].position);
            
            if (distanceBetweenWith0 < distanceBetweenWith1)
            {
                _targetFloor = elevatorFloors[1];
                _direction = -1;
                elevatorText.text = "0";
            }
            else if (distanceBetweenWith0 > distanceBetweenWith1)
            {
                _targetFloor = elevatorFloors[0];
                _direction = 1;
                elevatorText.text = "1";   
            }
        }
    }
}
