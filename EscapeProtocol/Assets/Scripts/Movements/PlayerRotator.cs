using DG.Tweening;
using Movements.Interfaces;
using UnityEngine;
using Utilities;

namespace Movements
{
    public class PlayerRotator : MonoBehaviour, IRotator
    {
        [SerializeField] private Transform playerBody;
        public void GetAim(Vector3 target)
        {
            playerBody.LookAt(target);
        }
        /// <summary>
        /// Set the rotation of the player to the move direction.
        /// </summary>
        /// <param name="direction"></param>
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
        /// <summary>
        /// Set the rotation of the player to the target.
        /// </summary>
        /// <param name="currentRotation"></param>
        /// <param name="targetRotation"></param>
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