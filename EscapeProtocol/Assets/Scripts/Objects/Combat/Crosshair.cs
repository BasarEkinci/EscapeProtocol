using UnityEngine;
using Utilities;

namespace Combat
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float zPos;
        [SerializeField] private MouseToWorldPosition mouseToWorldPosition;

        private void Update()
        {
            transform.position = mouseToWorldPosition.GetCursorWorldPoint(zPos);
        }
    }
}
