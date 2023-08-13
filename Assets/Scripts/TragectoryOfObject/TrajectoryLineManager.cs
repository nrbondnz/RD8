using System;
using TrajectoryObject;
using UnityEngine;

namespace TrajectoryObject
{
    public class TrajectoryLineManager : MonoBehaviour
    {
        [SerializeField] Projection projection;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] public bool isTrajectoryLine = true;
        

        public void Start()
        {
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

       

        public void DrawRayFromRigidBody(Player.Player pPlayer)
        {
            Rigidbody pRB = gameObject.GetComponent<Rigidbody>();
            //create a new pRay, it's center is the player position, it's direction is Vector3.Down
            Ray ray = new Ray(pRB.transform.position, Vector3.down);
            //Physics.Raycast will return true if the pRay hits a collider
            //send the pRay and check if it did hit anything, the pRay length is going to be half of our scale(player's radius),
            //plus a small value to make sure our pRay is barley longer than the player's radius
            if (Physics.Raycast(ray, pRB.transform.localScale.x / 2f + 0.01f))
            {
                pPlayer.IsGrounded = true;
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
                pPlayer.IsGrounded = false;
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, pRB.position);
                if (isTrajectoryLine)
                {
                    bool? lands = projection?.SimulateTrajectory( pRB.position, pRB.velocity);
                    
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
                        Vector3 pos = pRB.transform.position;
                        pos.y = pos.y - 15.0f; 
                        lineRenderer.SetPosition(1, pos);
                    }
                }
            }
        }
    }
}