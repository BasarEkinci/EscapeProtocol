using UnityEngine;
using Utilities;

namespace Combat
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float zPos;

        private void Update()
        {
            transform.position = MouseToWorldPosition.Instance.GetCursorWorldPoint(zPos);
        }
    }
}
