using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {

        public void OnJump()
        {
            Debug.Log("Jump Pressed");
        }

        public void OnMovement(InputValue pInputValue)
        {
            Debug.Log("Movement : " + pInputValue);
        }
        
    }
}