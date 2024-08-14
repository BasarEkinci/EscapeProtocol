using Inputs;
using UnityEngine;

namespace Movements
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform playerBody;
        
        private InputHandler _inputHandler;
        
        private void Awake()
        {
            _inputHandler = new InputHandler();
        }
        
        private void Update()
        {
            if(_inputHandler.LeftRight.x != 0)
                Move(_inputHandler.LeftRight, _speed);
        }
        
        private void Move(Vector2 direction, float speed)
        {
            transform.position += new Vector3(direction.x, 0, 0) * (speed * Time.deltaTime);
        }
    }
}