using DG.Tweening;
using Inputs;
using UnityEngine;

namespace Objects.Interactable
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] private Transform floor1;
        [SerializeField] private Transform floor2;

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
                    transform.DOMove(floor2.position, 1f).OnComplete(() =>
                    {
                        _currentFloor = 2;
                        _isMoving = false;
                    });
                }
                else if (_currentFloor == 2)
                {
                    transform.DOMove(floor1.position, 1f).OnComplete(() =>
                    {
                        _currentFloor = 1;
                        _isMoving = false;
                    });
                }
            }
        }
    }
}
