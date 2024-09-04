using Movements.Interfaces;
using UnityEngine;

namespace Movements
{
    public class Mover : IMover
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        
        public Mover(Rigidbody rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
        }
           
        public void Move(float direction)
        {
            Vector3 move = new Vector3(direction * _speed, _rigidbody.velocity.y, _rigidbody.velocity.z);
            _rigidbody.velocity = move;
        }
    }
}