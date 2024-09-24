using AnimationStateMachine;
using AnimationStateMachine.Elevator;
using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Objects.Interactable.Elevator
{
    public class ElevatorDoorController : MonoBehaviour
    {
        [SerializeField] private SoundDataScriptable soundData;

        public Animator Animator => _animator;
        public bool IsPlayerNearby => _isPlayerNearby;
        
        #region Referances
        private ElevatorMovementController _elevatorMovementController;
        private Animator _animator;
        private AudioSource _audioSource;
        private IState<ElevatorDoorController> _currentState;
        #endregion
        
        #region Private Variables
        private bool _isPlayerNearby;
        private string _playerTag = "Player";
        #endregion
        
        private void Awake()
        {
            _audioSource = GetComponentInParent<AudioSource>();
            _elevatorMovementController = GetComponentInParent<ElevatorMovementController>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _currentState = new ClosedState();
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isPlayerNearby = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isPlayerNearby = false;
            }
        }

        public void PlayOpenSound()
        {
            SoundManager.PLaySound(soundData,"ElevatorDoorOpen",_audioSource);   
        }
        
        public void PlayCloseSound()
        {
            SoundManager.PLaySound(soundData,"ElevatorDoorClose",_audioSource);
        }

        public bool IsMoving()
        {
            return _elevatorMovementController.IsMoving;
        }

        public void ChangeState(IState<ElevatorDoorController> state)
        {
            state?.ExitState(this);
            _currentState = state;
            state?.EnterState(this);
        }
    }
    
}
