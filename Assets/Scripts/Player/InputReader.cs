using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {

        private Player player = new();
        
        private Controls controls;

        private void OnEnable()
        {
            if (controls != null)
                return;

            controls = new Controls();
            controls.Player.SetCallbacks(this);
            controls.Player.Enable();
        }

        public void OnDisable()
        {
            controls.Player.Disable();
        }
        
        public void Start()
        {
            //player = Player.getInstance();
        }

        /*public void OnJump()
        {
            Debug.Log("Jump Pressed");
            player.IsJumpButtonPressed = true;
            Actions.OnPlayerChanged(player);
        }*/

        /*public void OnMovement(InputAction.CallbackContext pContext)
        {
            if (pContext.performed)
            {*/
        /*public void OnMovement(InputValue pInputValue){
                Vector2 input2D = pInputValue.Get<Vector2>();

                player.HozInput = input2D.x;
                player.VertInput = input2D.y;
                string zero = player.HozInput == 0 && player.VertInput == 0 ? " ZERO" : "";
                UnityEngine.Debug.Log("OnMovement : x = " + player.HozInput + " y = " + player.VertInput + zero);


                //UnityEngine.Debug.Log("OnMovement : x = " + input2D.x + " y = " + input2D.y);
                Actions.OnPlayerChanged(player);
                //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
                //gameObjects[0].GetComponent<Rigidbody>()
                //    .AddForce(new Vector3(player.HozInput, 0.0f, player.VertInput), ForceMode.Impulse);
            }*/

        public void OnMovement(InputAction.CallbackContext context)
        {
            //if (context.started)
            //{
                Vector2 input2D = context.ReadValue<Vector2>();

                player.HozInput = input2D.x;
                player.VertInput = input2D.y;
                string zero = player.HozInput == 0 && player.VertInput == 0 ? " ZERO" : "";
                UnityEngine.Debug.Log("OnMovement : x = " + player.HozInput + " y = " + player.VertInput + zero);


                //UnityEngine.Debug.Log("OnMovement : x = " + input2D.x + " y = " + input2D.y);
                Actions.OnPlayerChanged(player);
                //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
                //gameObjects[0].GetComponent<Rigidbody>()
                //    .AddForce(new Vector3(player.HozInput, 0.0f, player.VertInput), ForceMode.Impulse);
            //}
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Jump Pressed");
                player.IsJumpButtonPressed = true;
                Actions.OnPlayerChanged(player);
            }
        }
    }
}