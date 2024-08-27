using UnityEngine;

namespace Movements.Interfaces
{
    public interface IRotator
    {
        void SetRotationToMoveDirection(float direction);
        void SetRotationToTarget(Vector3 currentRotation, Vector3 targetRotation);
        void GetAim(Vector3 target);
    }
}