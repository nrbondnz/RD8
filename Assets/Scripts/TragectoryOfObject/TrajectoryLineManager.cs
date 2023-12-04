using System;
using Player;
using TrajectoryObject;
using UnityEngine;
using Utilities;

namespace TrajectoryObject
{
    public class TrajectoryLineManager : MonoBehaviour
    {
        [SerializeField] Projection projection;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] public bool isTrajectoryLine = true;
        private Rigidbody _pRb;
        private PlayerInputState _playerInputState;

        private void UpdatePlayerInputState(PlayerInputState pPlayerInputState)
        {
            _playerInputState = pPlayerInputState;
        }
        
        public void OnEnable()
        {
            Actions.OnPlayerInput += UpdatePlayerInputState;
        }

        public void OnDisable()
        {
            Actions.OnPlayerInput -= UpdatePlayerInputState;
        }

        public void Start()
        {
            _pRb = gameObject.GetComponent<Rigidbody>();
            projection.CreatePhysicsScene();
            //lineRenderer = new LineRenderer();
        }
        
        private void OnDrawGizmos()
        {
            if (lineRenderer == null || (isTrajectoryLine && (projection == null )))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
                Debug.LogError("Line Renderer : " + lineRenderer);
                Debug.LogError("projection is on : " + isTrajectoryLine + " projection : " + projection);
            }
        }

       

        public void DrawRayFromRigidBody(PlayerInputState pPlayerInputState)
        {
            
            //create a new pRay, it's center is the player position, it's direction is Vector3.Down
            Ray ray = new Ray(_pRb.transform.position, Vector3.down);
            //Physics.Raycast will return true if the pRay hits a collider
            //send the pRay and check if it did hit anything, the pRay length is going to be half of our scale(player's radius),
            //plus a small value to make sure our pRay is barley longer than the player's radius
            if (Physics.Raycast(ray, _pRb.transform.localScale.x / 2f + 0.01f))
            {
                //pOnScreenPlayerUpdate.IsGrounded = true;
                _playerInputState.Grounded = true;
                Actions.OnPlayerInput(_playerInputState);
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
                _playerInputState.Grounded = false;
                Actions.OnPlayerInput(_playerInputState);
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, _pRb.position);
                if (isTrajectoryLine)
                {
                    bool? lands = projection?.SimulateTrajectory( _pRb.position, _pRb.velocity);
                    
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
                        Vector3 pos = _pRb.transform.position;
                        pos.y = pos.y - 15.0f; 
                        lineRenderer.SetPosition(1, pos);
                    }
                }
            }
        }
    }
}