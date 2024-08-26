using UnityEngine;

namespace Objects
{
    public class EnemyDetector : MonoBehaviour
    {
        public bool IsPlayerDetected => _isPlayerDetected;
        public GameObject Target => _target;
        private GameObject _target;
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
                    _target = hit.collider.gameObject;
                }
                else
                {
                    _isPlayerDetected = false;
                    _target = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward);
        }
    }
}