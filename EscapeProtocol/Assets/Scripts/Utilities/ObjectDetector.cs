using UnityEngine;

namespace Utilities
{
    public class ObjectDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float detectionRadius;
        
        internal bool IsLayerDetected()
        {
            return Physics.CheckSphere(transform.position, detectionRadius, layerMask);
        }
        internal bool IsObjectDetected(string objectTag)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var objectCollider in colliders)
            {
                if (objectCollider.CompareTag(objectTag))
                {
                    Debug.Log("Object Detected");
                    return true;
                }
            }
            return false;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, detectionRadius);
        }
    }
}