using UnityEngine;

namespace Inputs
{
    public class InputHandler
    {
        private PlayerInputs _playerInputs;
        public bool IsJumped { get; private set; }
        public bool IsFiring { get; private set; }
        public bool IsPowerShootPressed { get; private set; }
        public Vector2 LeftRight => GetMovementDirection();

        public InputHandler()
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Player.Enable();            
            _playerInputs.Player.DefaultFire.performed += context => IsFiring = context.ReadValueAsButton();
            _playerInputs.Player.PowerShoot.performed += context => IsPowerShootPressed = context.ReadValueAsButton();
            _playerInputs.Player.Jump.performed += context => IsJumped = context.ReadValueAsButton();
            
            _playerInputs.Enable();
        }

        private Vector2 GetMovementDirection()
        {
            return _playerInputs.Player.Move.ReadValue<Vector2>();
        }
    }
}
