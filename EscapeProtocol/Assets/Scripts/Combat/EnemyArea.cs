using Data.UnityObjects;
using Managers;
using UnityEngine;

namespace Combat
{
    public class EnemyArea : MonoBehaviour
    {
        [SerializeField] private SoundDataScriptable soundData;
        public bool IsEnemyDetected => _isEnemyDetected;
        public GameObject Enemy => _enemy;
        
        private bool _isEnemyDetected;
        private GameObject _enemy;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SoundManager.PLaySound(soundData,"Target",null,1);
                _isEnemyDetected = true;
                _enemy = other.gameObject;
                Debug.Log("Enemy Detected");
            }            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _isEnemyDetected = false;
                _enemy = null;
                Debug.Log("Enemy Lost");
            }
        }
    }
}
