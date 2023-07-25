using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Player
{
    public class PlayerInputController : MonoBehaviour {
    
        private Player player;
        public void Start()
        {
            player = Player.getInstance();
        }

        public void OnJump()
        {
            Debug.Log("Jump Pressed");
            player.IsJumpButtonPressed = true;
            Actions.OnPlayerChanged(player);
        }

        public void OnMovement(InputValue pInputValue)
        {
            Debug.Log("Movement : " + pInputValue);
            Vector2 input2D= pInputValue.Get<Vector2>();
            player.HozInput = input2D.x;
            player.VertInput = input2D.y;
            UnityEngine.Debug.Log(input2D);
            Actions.OnPlayerChanged(player);
            //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            //gameObjects[0].GetComponent<Rigidbody>()
            //    .AddForce(new Vector3(player.HozInput, 0.0f, player.VertInput), ForceMode.Impulse);
        }
    }
}