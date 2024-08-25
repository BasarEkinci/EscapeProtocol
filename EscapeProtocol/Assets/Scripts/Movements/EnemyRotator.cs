using DG.Tweening;
using Movements.Interfaces;
using UnityEngine;

namespace Movements
{
    public class EnemyRotator : MonoBehaviour, IRotator
    {
        [SerializeField] private Transform bodyTransform;

        private float _turnDuration;
        private float _turdDirectionAngle;
        private Vector3 _aimOffset;

        private void Awake()
        {
            _turdDirectionAngle = 90f;
            _aimOffset = new Vector3(0, 0.8f, 0);
            _turnDuration = 0.1f;
        }
        public void SetRotationToTarget(Vector3 ownPos, Vector3 targetPos)
        {
            if (ownPos.x < targetPos.x)
            {
                transform.DORotate(Vector3.up * _turdDirectionAngle, _turnDuration);
            }
            else
            {
                transform.DORotate(Vector3.up * -_turdDirectionAngle, _turnDuration);
            }
        }

        public void GetAim(Vector3 worldPosition)
        {
            bodyTransform.LookAt(worldPosition + _aimOffset);
        }

        public void SetRotationToMoveDirection(float direction)
        {
            if (direction > 0)
            {
                transform.DORotate(Vector3.up * _turdDirectionAngle, _turnDuration);
            }
            else
            {
                transform.DORotate(Vector3.up * -_turdDirectionAngle, _turnDuration);
            }
        }
    }
}