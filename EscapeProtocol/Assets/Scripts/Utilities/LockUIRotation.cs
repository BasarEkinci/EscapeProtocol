using System;
using UnityEngine;

namespace Utilities
{
    public class LockUIRotation : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;

        private void LateUpdate()
        {
            transform.LookAt(cameraTransform);
        }
    }
}
