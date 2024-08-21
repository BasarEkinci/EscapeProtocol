using DG.Tweening;
using UnityEngine;
using Utilities;

namespace Movements
{
    public class PlayerRotator : MonoBehaviour
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
        internal void RotatePlayer()
        {
            if(MouseToWorldPosition.Instance.GetCursorWorldPoint().x > transform.position.x)
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
            playerBody.LookAt(MouseToWorldPosition.Instance.GetCursorWorldPoint(transform.position.z));
        }
    }
}