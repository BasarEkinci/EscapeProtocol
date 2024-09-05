using DG.Tweening;
using Inputs;
using UnityEngine;

namespace Objects.Interactable.Elevator
{
    public class ElevatorMovementController : MonoBehaviour
    {
        [SerializeField] private Transform floor1;
        [SerializeField] private Transform floor2;
        [SerializeField] private float duration;
        
        public bool IsMoving => _isMoving;
        
        private int _currentFloor;
        private bool _isPlayerInside;
        private bool _isMoving;
        private string _playerTag = "Player";
        private InputHandler _inputHandler;

        private void Awake()
        {
            _inputHandler = new InputHandler();
        }

        private void Start()
        {
          _currentFloor = 1;   
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                other.transform.parent = transform;
                _isPlayerInside = true;
            }
        }

        private void Update()
        {
            HandleElevatorMovement();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                other.transform.parent = null;
                _isPlayerInside = false;
            }
        }
        
        private void HandleElevatorMovement()
        {
            if (_isPlayerInside && _inputHandler.GetInteractInput() && !_isMoving)
            {
                _isMoving = true;
                if (_currentFloor == 1)
                {
                    transform.DOMove(floor2.position, duration).OnComplete(() =>
                    {
                        _currentFloor = 2;
                        _isMoving = false;
                    });
                }
                else if (_currentFloor == 2)
                {
                    transform.DOMove(floor1.position, duration).OnComplete(() =>
                    {
                        _currentFloor = 1;
                        _isMoving = false;
                    });
                }
            }
        }
    }
}
