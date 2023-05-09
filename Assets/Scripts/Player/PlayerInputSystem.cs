using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerInputSystem : MonoBehaviour
    {
        private Player _player;
        private PlayerInput playerInput;
        private Rigidbody _rb;
        
        //private PlayerInputActions playerInputActions;
        private CreateInputActions createInputActions;
        
        public void Awake()
        {
            //playerInput = GetComponent<PlayerInput>();
            //playerInput.onActionTriggered += Jump;
            
           
        }

        public void Start()
        {
            createInputActions = new CreateInputActions();
            createInputActions.Player.Enable();
            createInputActions.Player.Jump.performed += Jump;
            createInputActions.Player.Movement.performed += Movement;
            _player = new Player();
            _rb = GetComponent<Rigidbody>();
        }

        private void Movement(InputAction.CallbackContext context)
        {
            Debug.Log("Movement : " + context);
            _player.HozInput = context.action.ReadValue<Vector2>().x;
            _player.VertInput = context.action.ReadValue<Vector2>().y;
            if ((!_player.GoingForwards) && (_player.VertInput == 1) && (_rb.velocity.magnitude > 6.0f))
            {
                _player.GoingForwards = true;
                Actions.OnPlayerChanged(_player);
            }
            else if ((_player.GoingForwards) && (_player.VertInput == -1))

            {
                _player.GoingForwards = false;
                Actions.OnPlayerChanged(_player);
            }
            //Actions.OnPlayerChanged(_player);
            Debug.Log("Player : " + _player);
        }
        
        private void Jump(InputAction.CallbackContext context)
        {
            Debug.Log("Jump : " + context);
            _player.IsJumpButtonPressed = true;
            Actions.OnPlayerChanged(_player);
            Debug.Log("Player : " + _player);
            //GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse); 
            
        }
    }
}