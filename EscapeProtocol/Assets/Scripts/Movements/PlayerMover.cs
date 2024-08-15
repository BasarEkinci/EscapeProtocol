using UnityEngine;

namespace Movements
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private Transform _playerTransform;
        private void Awake()
        {
            _playerTransform = transform;
        }
        internal void Move(float direction)
        {
            Vector3 moveVector = new Vector3(direction, 0, 0) * (speed * Time.deltaTime);
            _playerTransform.position += moveVector;
        }
    }
}