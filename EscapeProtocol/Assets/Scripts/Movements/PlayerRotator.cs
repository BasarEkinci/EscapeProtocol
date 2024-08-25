using DG.Tweening;
using Movements.Interfaces;
using UnityEngine;
using Utilities;

namespace Movements
{
    public class PlayerRotator : MonoBehaviour, IRotator
    {
        [SerializeField] private Transform playerBody;
        internal bool IsMovingForward(Vector3 direction)
        {
            bool isMovingForward;
            if(MouseToWorldPosition.Instance.GetCursorWorldPoint().x > transform.position.x)
            {
                isMovingForward = direction.x > 0;
            }
            else
            {
                isMovingForward = direction.x < 0;
            }
            return isMovingForward;     
        }
        public void GetAim(Vector3 worldPosition)
        {
            playerBody.LookAt(worldPosition);
        }

        public void SetRotationToMoveDirection(float direction)
        {
            if (direction > 0)
            {
                transform.DORotate(Vector3.up * 90, 0.1f);
            }
            else
            {
                transform.DORotate(Vector3.up * -90, 0.1f);
            }
        }

        public void SetRotationToTarget(Vector3 currentRotation, Vector3 targetRotation)
        {
            if (currentRotation.x > targetRotation.x)
            {
                transform.DORotate(Vector3.up * -90, 0.1f);
            }
            else
            {
                transform.DORotate(Vector3.up * 90, 0.1f);
            }   
        }
    }
}