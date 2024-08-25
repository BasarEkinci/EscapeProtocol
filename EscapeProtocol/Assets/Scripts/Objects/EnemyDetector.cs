using UnityEngine;

namespace Objects
{
    public class EnemyDetector : MonoBehaviour
    {
        
        public bool IsPlayerDetected => _isPlayerDetected;

        private bool _isPlayerDetected;
        private void Update()
        {
            DetectEnemy();
        }
        
        private void DetectEnemy()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    _isPlayerDetected = true; 
                    Debug.Log("Enemy Detected");
                }
                else
                {
                    Debug.Log(hit.collider.name);
                    _isPlayerDetected = false;
                }
            }
        }
    }
}