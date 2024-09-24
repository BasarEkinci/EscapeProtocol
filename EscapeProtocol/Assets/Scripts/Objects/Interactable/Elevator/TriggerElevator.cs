using UnityEngine;

namespace Objects.Interactable.Elevator
{
    public class TriggerElevator : MonoBehaviour
    {
        public bool IsPlayerInside => _isPlayerInside;
        private string _playerTag = "Player";
        private bool _isPlayerInside;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isPlayerInside = true;
                other.transform.SetParent(transform);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isPlayerInside = false;
                other.transform.SetParent(null);
            }
        }
    }
}
