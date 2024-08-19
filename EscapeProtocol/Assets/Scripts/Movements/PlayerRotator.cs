using DG.Tweening;
using UnityEngine;

namespace Movements
{
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private Transform playerBody;
        
        private Camera _mainCamera;
        
        private Vector3 _mousePosition;
        private Vector3 _screenCenter;
        private Vector3 _aimPosition;
        
        private float _mouseX;
        private float _mouseY;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        internal void GetMousePosition()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.z = transform.position.z;
        }

        internal bool IsMovingForward(Vector3 direction)
        {
            bool isMovingForward;
            if(_aimPosition.x > transform.position.x)
            {
                isMovingForward = direction.x > 0;
            }
            else
            {
                isMovingForward = direction.x < 0;
            }
            return isMovingForward;     
        }
        internal void RotatePlayer()
        {
            if(_aimPosition.x > transform.position.x)
            {
                transform.DORotate(Vector3.up * 90, 0.1f);
            }
            else
            {
                transform.DORotate(Vector3.up*-90, 0.1f);
            }
        }

        internal void GetAim()
        {
            Ray ray = _mainCamera.ScreenPointToRay(_mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 lookPoint = hit.point;
                lookPoint.z = transform.position.z;
                _aimPosition = lookPoint;
            }
            playerBody.LookAt(_aimPosition);
        }
    }
}