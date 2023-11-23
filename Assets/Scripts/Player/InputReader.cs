using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
namespace Player
{
    /// <summary>
    /// Uses the New Input 'Controls'(name you choose) which causes dot net generation of controls for the scenes
    /// OnMovement is generated to connect to the Movement action(in unity) Input Actions object
    /// The Controls.IPlayerActions is then an interface that this class implments and will be called when the action takes place
    /// </summary>
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {

        private OnScreenPlayerUpdate _onScreenPlayerUpdate = new();
        
        private Controls _controls;

        /// <summary>
        /// OnEnable is used to connect up the callbacks to this 'implementing' class
        /// </summary>
        private void OnEnable()
        {
            if (_controls != null)
                return;

            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }

        /// <summary>
        /// OnDissable is used as the InputReader is dissabled, ending callbacks and setting the service to dissabled
        /// </summary>
        public void OnDisable()
        {
            _controls.Player.Disable();
        }

        /// <summary>
        /// This is one of the implementing interface methods that will be called when the action takes place.
        /// However, this old input system using a parameter of InputValue.  The new input action is below
        /// </summary>
        /// <param name="pInputValue"></param>
        public void OnMovement(InputValue pInputValue)
        {
            Debug.Log("Old world OnMovement");
        }

        public void OnJump(InputValue pInputValue)
        {
            Debug.Log("Old world OnJump");
        }


        /// <summary>
        /// This is using the new input system with its signature of CallbackContext(which is more rich in functionality)
        /// The fact its value is a Vector2 comes from the unity setup of the action properties
        /// It uses the Actions class to connect it to interested consumers of change
        /// </summary>
        /// <param name="context"></param>
        public void OnMovement(InputAction.CallbackContext context)
        {
                Vector2 input2D = context.ReadValue<Vector2>();
                _onScreenPlayerUpdate.HozInput = input2D.x;
                _onScreenPlayerUpdate.VertInput = input2D.y;
                string zero = _onScreenPlayerUpdate.HozInput == 0 && _onScreenPlayerUpdate.VertInput == 0 ? " ZERO" : "";
                //UnityEngine.Debug.Log("OnMovement : x = " + _player.HozInput + " y = " + _player.VertInput + zero);
                Actions.OnPlayerChanged(_onScreenPlayerUpdate);
               
        }

        public void OnCameraMove(InputAction.CallbackContext context)
        {
            Vector2 input2D = context.ReadValue<Vector2>();
            CameraFollow cameraFollow = (CameraFollow)GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            cameraFollow.MoveCamera(input2D);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Jump Pressed");
                _onScreenPlayerUpdate.IsJumpButtonPressed = true;
                Actions.OnPlayerChanged(_onScreenPlayerUpdate);
            }
        }
    }
}