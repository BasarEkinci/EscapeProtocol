using UnityEngine;

namespace Movements
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed = 0.125f;
        
        private void LateUpdate()
        {
            Vector3 desiredPosition = targetTransform.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
