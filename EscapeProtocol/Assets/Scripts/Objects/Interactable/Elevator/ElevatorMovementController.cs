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
        [SerializeField] private float speed;
        
        [Header("Data")]
        [SerializeField] private SoundDataScriptable soundData;
        
        [Header("Text")]
        [SerializeField] private TMP_Text elevatorText;
        #endregion
        public bool IsMoving => _isMoving;
        

        #region Private Variables
        private int _currentFloor;
        private bool _isPlayerInside;
        private bool _isMoving;
        private string _playerTag = "Player";
        private float _direction;
        private Rigidbody _rb;
        private Transform _targetFloor;
        #endregion
        
        private AudioSource _audioSource;
        private InputHandler _inputHandler;
        private void Awake()
        {
            _inputHandler = new InputHandler();
            _audioSource = GetComponent<AudioSource>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
          _currentFloor = 1;
          elevatorText.text = _currentFloor.ToString();
        }
        private void Update()
        {
            HandleElevatorTargetFloor();
        }

        private void FixedUpdate()
        {
            HandleElevatorMovement();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                _isPlayerInside = true;
                Debug.Log("Player is inside");
                other.transform.SetParent(transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                _isPlayerInside = false;
                other.transform.SetParent(null);
            }
        }
        
        
        private void HandleElevatorMovement()
        {
            if (_isPlayerInside && _inputHandler.GetInteractInput())
            {
                _rb.velocity = new Vector3(_rb.velocity.x, speed * _direction, _rb.velocity.z);
            }
        }

        private void HandleElevatorTargetFloor()
        {
            if (transform.position == floor1.position)
            {
                elevatorText.text = "1";
                _direction = -1;
                _targetFloor = floor2;
                _isMoving = false;
            }
            else if (transform.position == floor2.position)
            {
                elevatorText.text = "2";
                _direction = 1;
                _targetFloor = floor1;
                _isMoving = false;
            }
            else
            {
                _isMoving = true;
                elevatorText.text = _direction > 0 ? "/\\" : "\\/";
                SoundManager.PLaySound(soundData,"ElevatorMovement",_audioSource);
            }
        }
    }
}
