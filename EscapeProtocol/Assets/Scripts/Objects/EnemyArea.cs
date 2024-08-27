using UnityEngine;

namespace Objects
{
    public class EnemyArea : MonoBehaviour
    {
        public bool IsPlayerInArea { get;private set; }
        public GameObject Target { get; private set; }
        
        
        private string _playerTag = "Player";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                IsPlayerInArea = true;
                Target = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                IsPlayerInArea = false;
                Target = null;
            }
        }
    }
}