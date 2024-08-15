using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movements
{
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private Transform playerBody;

        
        private Camera _mainCamera;
        
        private Vector3 _playerWorldPosition;
        private Vector3 _playerScreenPosition;
        private Vector3 _mousePosition;
        private Vector3 _screenCenter;
        private Vector3 _aimPosition;
        
        private float _mouseX;
        private float _mouseY;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            GetMousePosition();
            GetPlayerScreenPosition();
            RotatePlayer();
            GetAim();
        }

        private void GetPlayerScreenPosition()
        {
            _playerWorldPosition = transform.position;
            _playerScreenPosition = _mainCamera.WorldToScreenPoint(_playerWorldPosition);
        }
        
        private void GetMousePosition()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.z = transform.position.z;
        }
        
        private void RotatePlayer()
        {
            if(_mousePosition.x > _playerScreenPosition.x)
            {
                transform.DORotate(Vector3.up * 90, 0.1f);
            }
            else
            {
                transform.DORotate(Vector3.up*-90, 0.1f);
            }
        }

        private void GetAim()
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