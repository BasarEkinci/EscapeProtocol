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
            transform.position = Vector3.Lerp(transform.position, targetTransform.position + offset, Time.deltaTime * smoothSpeed);
        }
    }
}
