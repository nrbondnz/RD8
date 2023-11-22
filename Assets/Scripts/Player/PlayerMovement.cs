using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TrajectoryObject;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Player
{
    /// <summary>
    /// Moves the player by adding force in the required direction
    /// TODO: This class is too complex and needs abstraction
    /// TODO: Remove the projection system in favour of a subscription model so if available it will react correctly
    /// The _mainCamera is needed to help direct the movement so its not confusing to the player,
    /// as it swings around to focus on the next waypoint
    /// TODO: When we get to more than IOS, mobileMultiplier will need extending for other devices during test probably
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {

        private Rigidbody _rb;
        [SerializeField] private float speed = 10;
        [SerializeField] private float jumpForce = 13;
        [SerializeField] TrajectoryLineManager trajectoryLineManager;
        private GameObject _mainCamera;
        //private bool _isGhost;
        private Player _player;
        
    
        
        
        // Start is called before the first frame update
        void Start()
        {
            //trajectoryLineManager = gameObject.GetComponent<TrajectoryLineManager>();    
            //projection = Projection.GetInstance();
            
            //onGoingForwards += GoingBackwards;
            _rb = GetComponent<Rigidbody>();
            _player = Player.getInstance();
            _mainCamera = GameObject.FindWithTag("MainCamera");
            Debug.Log("_cameraFollow set to : " + _mainCamera);
        }
        
        private void FixedUpdate()
        {
            GamePlayManager.GetInstance().UpdateTimers();
 
            Vector3 forward = _mainCamera.transform.forward;
            Vector3 right = _mainCamera.transform.right;
            forward.y = 0.0f;
            right.y = 0.0f;
            forward.Normalize();
            right.Normalize();
            Vector3 desiredDirection = forward * Player.getInstance().VertInput + right * Player.getInstance().HozInput;
                       
            _rb.AddForce(desiredDirection * speed, ForceMode.Acceleration);
            
            trajectoryLineManager.DrawRayFromRigidBody(_player);

            if (_player.IsJumpButtonPressed && _player.IsGrounded)
            {
                //if true, then add a force in the up direction of our player in the form of an impulse
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //then reset the jump variable so we don't fly to the moon :).
                _player.IsJumpButtonPressed = false;
            }
        }
    }
}