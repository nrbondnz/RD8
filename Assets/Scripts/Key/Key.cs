using System;
using System.Collections;
using System.Collections.Generic;
using Key;
using Managers;
using UnityEngine;

namespace key
{
    public class Key : MonoBehaviour
    {
        [SerializeField] KeyActionController keyActionController;
        [SerializeField] private float keyRotationSpeed = 3;
        [SerializeField] WaypointManager waypointManager;

      

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                keyActionController.CarryOutAction();
                waypointManager?.NextWaypoint();
                gameObject.SetActive(false);
                Debug.LogWarning("Just setActive to false");
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * keyRotationSpeed);
        }

        private void OnDrawGizmos()
        {
            if (keyActionController != null)
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
    }
}
