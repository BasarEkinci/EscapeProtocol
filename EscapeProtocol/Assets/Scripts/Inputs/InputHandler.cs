using UnityEngine;

namespace Inputs
{
    public class InputHandler
    {
        private PlayerInputs _playerInputs;

        public InputHandler()
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Player.Enable();            
            _playerInputs.Enable();
        }

        public bool GetJumpInput()
        {
            return _playerInputs.Player.Jump.triggered;
        }
        
        public bool GetRightClick()
        {
            return _playerInputs.Player.PowerShoot.triggered;
        }

        public bool GetLeftClick()
        {
            return _playerInputs.Player.DefaultFire.triggered;
        }
        public Vector2 GetMovementDirection()
        {
            return _playerInputs.Player.Move.ReadValue<Vector2>();
        }
    }
}
