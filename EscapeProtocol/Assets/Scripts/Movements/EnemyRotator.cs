using DG.Tweening;
using UnityEngine;

namespace Movements
{
    public class EnemyRotator : MonoBehaviour
    {
        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Transform ownTransform;

        private float _turnDuration;
        private float _turdDirectionAngle;
        private Vector3 _aimOffset;

        private void Awake()
        {
            _turdDirectionAngle = 90f;
            _aimOffset = new Vector3(0, 0.8f, 0);
            _turnDuration = 0.1f;
        }

        internal void GetAimToPlayer(Vector3 ownPos, Vector3 targetPos)
        {
            if (ownPos.x < targetPos.x)
            {
                ownTransform.DORotate(Vector3.up * _turdDirectionAngle, _turnDuration);
            }
            else
            {
                ownTransform.DORotate(Vector3.up * -_turdDirectionAngle, _turnDuration);
            }
            bodyTransform.LookAt(targetPos + _aimOffset);
        }

        internal void SetRotationToMoveDirection(float moveSpeed)
        {
            if (moveSpeed > 0)
            {
                ownTransform.DORotate(Vector3.up * _turdDirectionAngle, _turnDuration);
                bodyTransform.DORotate(new Vector3(5, _turdDirectionAngle, 0), _turnDuration);
            }
            else
            {
                ownTransform.DORotate(Vector3.up * -_turdDirectionAngle, _turnDuration);
                bodyTransform.DORotate(new Vector3(5, -_turdDirectionAngle, 0), _turnDuration);
            }
        }
    }
}