using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputHandler
    {
        private readonly PlayerInputs _playerInputs;
        
        private bool _isAttacking;
        public InputHandler()
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Player.DefaultFire.performed += DefaultAttackPerformed;
            _playerInputs.Player.DefaultFire.canceled += DefaultAttackCanceled;
            _playerInputs.Player.Enable();            
        }

        private void DefaultAttackCanceled(InputAction.CallbackContext obj)
        {
            _isAttacking = false;
        }
        private void DefaultAttackPerformed(InputAction.CallbackContext obj)
        {
            _isAttacking = true;
        }
        public bool GetAttackInput()
        {
            return _isAttacking;
        }
        public bool GetJumpInput()
        {
            return _playerInputs.Player.Jump.triggered;
        }
        public Vector2 GetMovementDirection()
        {
            return _playerInputs.Player.Move.ReadValue<Vector2>();
        }
        
        public bool GetThrowGrenadeInput()
        {
            return _playerInputs.Player.ThrowGrenade.triggered;
        }
        
        public bool GetInteractInput()
        {
            return _playerInputs.Player.Interact.triggered;
        }
        
        public bool GetPauseInput()
        {
            return _playerInputs.Player.StopGame.triggered;
        }
    }
}
