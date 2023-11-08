using System;
using System.Collections;
using System.Collections.Generic;
using Key;
using Managers;
using Managers.WaypointManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace key
{
    public class Key : WaypointSubscriber
    {
        [SerializeField] KeyActionController keyActionController;
        [SerializeField] private float keyRotationSpeed = 3;
        //private WaypointManager _waypointManager;

        
        
      

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                keyActionController.CarryOutAction();
                FindAnyObjectByType<WaypointManager>().NextWaypoint();
                gameObject.SetActive(false);
                Debug.Log("Just setActive to false for waypoint : " + gameObject.name);
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * keyRotationSpeed);
        }

        private void OnDrawGizmos()
        {
            //WaypointManager.GetInstance().OnDrawGizmos();
            /*if (( ! keyActionController.IsUnityNull()) && ( waypointManager.hasWaypoints()))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, keyActionController.transform.position - transform.position);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
            }*/
        }
        
    }
}
