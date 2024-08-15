using Inputs;
using UnityEngine;

namespace Movements
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private InputHandler _inputHandler;
        
        private void Awake()
        {
            _inputHandler = new InputHandler();
        }
        
        private void Update()
        {
            if(_inputHandler.LeftRight.x != 0)
                Move(_inputHandler.LeftRight, speed);
        }
        
        private void Move(Vector2 direction, float Speed)
        {
            transform.position += new Vector3(direction.x, 0, 0) * (Speed * Time.deltaTime);
        }
    }
}