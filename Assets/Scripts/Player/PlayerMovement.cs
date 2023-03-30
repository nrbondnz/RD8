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
    /// </summary>
    public partial class PlayerMovement : MonoBehaviour
    {

        private Rigidbody _rb;
        [SerializeField] private float speed = 10;
        [SerializeField] private float jumpForce = 13;
        [SerializeField] private float platformPower = 2.5f;
        
        [SerializeField] Projection projection;
        [SerializeField] private LineRenderer lineRenderer;
        private GameObject _mainCamera;
        [SerializeField] private bool isTrajectoryLine = true;


        private bool _isGhost;
        private readonly Player _player = new();
        
        private void OnDrawGizmos()
        {
            if (lineRenderer == null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
            //projection = Projection.GetInstance();
            projection.CreatePhysicsScene();
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
            _player.HozInput = Input.GetAxis("Horizontal");
            _player.VertInput = Input.GetAxis("Vertical");

            if ((Input.acceleration.x > 0.1) || (Input.acceleration.x < -0.1))
            {
                _player.HozInput = Input.acceleration.x * platformPower;
            }

            if ((Input.acceleration.y > 0.1) || (Input.acceleration.y < -0.1))
            {
                _player.VertInput = Input.acceleration.y * platformPower;

            }

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

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _player.IsJumpButtonPressed = true;
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                _player.IsJumpButtonPressed = true;
            }

        }

        private void FixedUpdate()
        {
            Vector3 forward = _mainCamera.transform.forward;
            Vector3 right = _mainCamera.transform.right;
            forward.y = 0.0f;
            right.y = 0.0f;
            forward = forward.normalized;
            right = right.normalized;
            Vector3 forwardRelativeVerticalInput = forward * (_player.VertInput * speed);
            Vector3 rightRelativeHorizontalInput = right * (_player.HozInput * speed);
            //
            Vector3 playerMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;
            _rb.AddForce(playerMovement, ForceMode.Acceleration);

            //create a new ray, it's center is the player position, it's direction is Vector3.Down
            Ray ray = new Ray(transform.position, Vector3.down);
            //Physics.Raycast will return true if the ray hits a collider
            //send the ray and check if it did hit anything, the ray length is going to be half of our scale(player's radius),
            //plus a small value to make sure our ray is barley longer than the player's radius


            if (Physics.Raycast(ray, transform.localScale.x / 2f + 0.01f)) {
                _player.IsGrounded = true;
                if (isTrajectoryLine)
                {
                    projection?.RemoveTrajectoryLine();
                }
                else
                {
                    lineRenderer.enabled = false;
                }
            }
            else
            {
                _player.IsGrounded = false;
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, _rb.position);
                if (isTrajectoryLine)
                {
                    projection?.SimulateTrajectory(gameObject, _rb.position, _rb.velocity);
                }
                else
                {

                    //Vector3 down = rb.TransformDirection(Vector3.down) * 10;
                    
                    if (Physics.Raycast(ray, out var hitInfo, 20.0f))
                    {
                        lineRenderer.startColor = Color.green;
                        lineRenderer.endColor = Color.cyan;
                        lineRenderer.SetPosition(1, hitInfo.point);
                    }
                    else
                    {
                        lineRenderer.startColor = Color.red;
                        lineRenderer.endColor = Color.black;
                        Vector3 pos = transform.position;
                        pos.y = pos.y - 15.0f;
                        lineRenderer.SetPosition(1, pos);
                    }
                }
            }

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