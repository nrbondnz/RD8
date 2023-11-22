using System;
using System.Collections;
using System.Collections.Generic;
using Key;
using Managers;
using Managers.WaypointManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace Key
{
    /// <summary>
    /// The @ref Managers.WaypointManagement.WaypointSubscriber "WaypointSubscriber" is expected from Key gameobjects
    /// as it helps the @ref Utilities.CameraFollow "CameraFollow"
    /// Any actions required on collision(by the player) are managed by the field linking to the @ref Key.KeyActionController "KeyActionController"
    /// It will update to the next waypoint(which there needs to be or camera follow will get confused)
    /// @see Managers.WaypointManagement.WaypointSubscriber
    /// <br>
    /// Utilities.CameraFollow
    /// <br>
    /// Key.KeyActionController
    /// </summary>
    public class Key : WaypointSubscriber
    {
        
        [SerializeField] KeyActionController keyActionController;
        /// @var keyRotationSpeed "turns the key object(to attract attention) at the configurable speed"
        [SerializeField] private float keyRotationSpeed = 20;
        
        /// <summary>
        /// Carries out the keyActionController action and sets the waypoint to the next waypoint(partly so the camera can look at it)
        /// The next waypoint will optionally have a countdown timer to get to it in time
        /// </summary>
        /// <param name="other"></param>
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


        /// <summary>
        /// Keys can use a @var keyRotationSpeed to turn the key in space
        /// </summary>
        private void Update()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * keyRotationSpeed));
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
