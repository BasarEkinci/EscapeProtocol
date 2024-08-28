using UnityEngine;

namespace Objects
{
    public class EnemyFieldOfView : MonoBehaviour
    {
        public bool IsPlayerInView => IsThePlayerInView();
        public GameObject Player { get; private set; }
        [SerializeField] private Transform aimTransform;
        [SerializeField] private float viewDistance = 30f;
        private void Update()
        {
            IsThePlayerInView();
        }

        private bool IsThePlayerInView()
        {
            var hit = Physics.Raycast(aimTransform.position, aimTransform.forward, out var hitInfo, viewDistance);
            if (hit)
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    Player = hitInfo.collider.gameObject;
                    return true;   
                }
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(aimTransform.position, aimTransform.forward * viewDistance);
        }
    }
}