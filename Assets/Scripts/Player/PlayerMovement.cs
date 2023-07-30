using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TrajectoryObject;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("platformPower")] [SerializeField] private float mobileMultiplier = 2.5f;
        private GameObject _mainCamera;
        private bool _isGhost;
        private Player _player = new();
        

        public void Awake()
        {
            Actions.OnPlayerChanged += UpdatePlayer;
        }

        public void UpdatePlayer(Player pPlayer)
        {
            _player = pPlayer;
            Debug.Log("Player : " + _player.ToString());
        }
        
        // Start is called before the first frame update
        void Start()
        {
            //trajectoryLineManager = gameObject.GetComponent<TrajectoryLineManager>();    
            //projection = Projection.GetInstance();
            
            //onGoingForwards += GoingBackwards;
            _rb = GetComponent<Rigidbody>();
            _mainCamera = GameObject.FindWithTag("MainCamera");
            Debug.Log("_cameraFollow set to : " + _mainCamera);
        }

        // Update is called once per frame
        void Update()
        {
            GamePlayManager.GetInstance().UpdateTimeRemaining();
            // d -> 1.0f, a -> -1.0f
            // TODO does not really mean this to the player, they are looking at the ball, so its ball change not x and y
            /*_player.HozInput = Input.GetAxis("Horizontal");
            _player.VertInput = Input.GetAxis("Vertical");

            if ((Input.acceleration.x > 0.1) || (Input.acceleration.x < -0.1))
            {
                _player.HozInput = Input.acceleration.x * mobileMultiplier;
            }

            if ((Input.acceleration.y > 0.1) || (Input.acceleration.y < -0.1))
            {
                _player.VertInput = Input.acceleration.y * mobileMultiplier;

            }

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase.HumanName() == TouchPhase.Began.HumanName())
                {
                    _player.IsJumpButtonPressed = true;
                }
            }*/

        }

        private void FixedUpdate()
        {
            Vector3 forward = _mainCamera.transform.forward;
            Vector3 right = _mainCamera.transform.right;
            forward.y = 0.0f;
            right.y = 0.0f;
            forward = forward.normalized;
            right = right.normalized;
            Vector3 forwardRelativeVerticalInput = forward * (mobileMultiplier * (_player.VertInput * speed));
            Vector3 rightRelativeHorizontalInput = right * (mobileMultiplier * (_player.HozInput * speed));
            //
            Vector3 playerMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
            _rb.AddForce(playerMovement, ForceMode.Acceleration);

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