using System;
using DG.Tweening;
using UnityEngine;

namespace Movements
{
    public class PlayerRotator : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;

        private Vector3 _playerWorldPosition;
        private Vector3 _playerScreenPosition;
        private Vector3 _mousePosition;
        private Vector3 _screenCenter;
        
        private float _mouseX;
        private float _mouseY;

        private void Update()
        {
            GetMousePosition();
            GetPlayerScreenPosition();
            RotatePlayer();
        }

        private void GetPlayerScreenPosition()
        {
            _playerWorldPosition = transform.position;
            _playerScreenPosition = Camera.main.WorldToScreenPoint(_playerWorldPosition);
        }
        
        private void GetMousePosition()
        {
            _mousePosition = Input.mousePosition;
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
            
        }
    }
}