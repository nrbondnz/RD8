using System;
using System.Collections;
using System.Collections.Generic;
using Key;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace key
{
    public class Key : MonoBehaviour, IKeyTimeAllowedToWaypoint
    {
        [SerializeField] KeyActionController keyActionController;
        [SerializeField] private float keyRotationSpeed = 3;
        [SerializeField] WaypointManager waypointManager;

        [SerializeField] private float timeToWaypoint = 0.0f;

      

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                keyActionController.CarryOutAction();
                waypointManager?.NextWaypoint();
                gameObject.SetActive(false);
                Debug.Log("Just setActive to false for waypoint" + gameObject.name);
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * keyRotationSpeed);
        }

        private void OnDrawGizmos()
        {
            if (( ! keyActionController.IsUnityNull()) && ( waypointManager.hasWaypoints()))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, keyActionController.transform.position - transform.position);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
            }
        }

        public float TimeAllowedToWaypoint()
        {
            return this.timeToWaypoint;
        }
    }
}
