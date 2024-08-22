using UnityEngine;

namespace Movements
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed = 0.125f;
        
        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position + offset, smoothSpeed);
        }
    }
}
