using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Objects.Interactable.Elevator
{
    public class ElevatorDoorController : MonoBehaviour
    {
        [SerializeField] private SoundDataScriptable soundData;

        #region Referances
        private ElevatorMovementController _elevatorMovementController;
        private Animator _animator;
        #endregion
        
        #region Private Variables
        private bool _canOpen;
        private bool _isPlayerNearby;
        private string _playerTag = "Player";
        #endregion

        #region Animator Hashes
        private static readonly int CanOpen = Animator.StringToHash("CanOpen");
        private static readonly int IsPlayerNearby = Animator.StringToHash("IsPlayerNearby");
        #endregion
        
        private void Awake()
        {
            _elevatorMovementController = GetComponentInParent<ElevatorMovementController>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_elevatorMovementController.IsMoving)
            {
                _canOpen = false;
                SoundManager.PLaySound(soundData, "ElevatorMovement");
            }
            else
            {
                _canOpen = true;
            }
            
            _animator.SetBool(CanOpen, _canOpen);
            _animator.SetBool(IsPlayerNearby, _isPlayerNearby);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag) && _canOpen)
            {
                _isPlayerNearby = true;
                SoundManager.PLaySound(soundData,"ElevatorDoorOpen");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag) && _canOpen)
            {
                _isPlayerNearby = false;
                SoundManager.PLaySound(soundData,"ElevatorDoorClose");
            }
        }
        
    }
    
}
