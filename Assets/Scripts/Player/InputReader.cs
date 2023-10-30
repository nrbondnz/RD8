using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {

        private Player _player = new();
        
        private Controls _controls;

        private void OnEnable()
        {
            if (_controls != null)
                return;

            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }

        public void OnDisable()
        {
            _controls.Player.Disable();
        }


        public void OnMovement(InputAction.CallbackContext context)
        {
                Vector2 input2D = context.ReadValue<Vector2>();
                _player.HozInput = input2D.x;
                _player.VertInput = input2D.y;
                string zero = _player.HozInput == 0 && _player.VertInput == 0 ? " ZERO" : "";
                UnityEngine.Debug.Log("OnMovement : x = " + _player.HozInput + " y = " + _player.VertInput + zero);
                Actions.OnPlayerChanged(_player);
               
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Jump Pressed");
                _player.IsJumpButtonPressed = true;
                Actions.OnPlayerChanged(_player);
            }
        }
    }
}