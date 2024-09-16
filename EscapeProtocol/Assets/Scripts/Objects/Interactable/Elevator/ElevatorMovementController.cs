using Data.UnityObjects;
using DG.Tweening;
using Inputs;
using Managers;
using TMPro;
using UnityEngine;

namespace Objects.Interactable.Elevator
{
    public class ElevatorMovementController : MonoBehaviour
    {
        #region Serialized Fields
        [Header("Movement Settings")]
        [SerializeField] private Transform floor1;
        [SerializeField] private Transform floor2;
        [SerializeField] private Ease easeType;
        [SerializeField] private float duration;
        
        [Header("Data")]
        [SerializeField] private SoundDataScriptable soundData;
        
        [SerializeField] private TMP_Text elevatorText;
        #endregion
        public bool IsMoving => _isMoving;
        

        #region Private Variables
        private int _currentFloor;
        private bool _isPlayerInside;
        private bool _isMoving;
        private string _playerTag = "Player";
        private Rigidbody _playerRb;
        #endregion
        
        private AudioSource _audioSource;
        private InputHandler _inputHandler;
        private void Awake()
        {
            _inputHandler = new InputHandler();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
          _currentFloor = 1;
          elevatorText.text = _currentFloor.ToString();
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _playerRb = other.GetComponent<Rigidbody>();
               // other.transform.SetParent(transform);
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
                _isPlayerInside = false;
                //other.transform.SetParent(null);
            }
        }
        
        private void HandleElevatorMovement()
        {
            if (_isPlayerInside && _inputHandler.GetInteractInput() && !_isMoving)
            {
                _isMoving = true;
                if (_currentFloor == 1)
                {
                    elevatorText.text = "\\/";
                    SoundManager.PLaySound(soundData,"ElevatorMovement",_audioSource);
                    transform.DOMove(floor2.position, duration).SetEase(easeType).OnComplete(() =>
                    {
                        ElevatorArriveActions(2);
                    });
                }
                else if (_currentFloor == 2)
                {
                    elevatorText.text = "/\\";
                    SoundManager.PLaySound(soundData,"ElevatorMovement",_audioSource);
                    transform.DOMove(floor1.position, duration).SetEase(easeType).OnComplete(() =>
                    {
                        ElevatorArriveActions(1);
                    });
                }
            }
        }

        private void ElevatorArriveActions(int currentFloor)
        {
            _currentFloor = currentFloor;
            elevatorText.text = _currentFloor.ToString();
            _isMoving = false;
            _audioSource.Stop();
            _playerRb.isKinematic = false;
        }
    }
}
