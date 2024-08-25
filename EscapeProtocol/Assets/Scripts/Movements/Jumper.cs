using UnityEngine;

namespace Movements
{
    public class Jumper
    {
        private Rigidbody _rigidbody;
        private float _jumpForce;
        
        public Jumper(Rigidbody rigidbody, float jumpForce)
        {
            _rigidbody = rigidbody;
            _jumpForce = jumpForce;
        }
        public void Jump()
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpForce, _rigidbody.velocity.z);
        }
    }
}