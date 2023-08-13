using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace Managers
{
    public class WaypointManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] waypoints;

        private int _currentWaypoint = 0;
        private int _lastWaypoint = 0;

        private void OnDrawGizmos()
        {
            if ( ( ! waypoints.IsUnityNull() ) && ( waypoints.Length > 0) )
            {
                Gizmos.color = Color.green;
                //Gizmos.DrawRay(transform.position, WaypointManager.position - transform.position);
            }
            else
            {
                Gizmos.color = Color.red;
                Debug.LogWarning("WaypointManager : elements not set");
                
            }
            Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
        }

        public void Start()
        {
            if (waypoints is not null)
            {
                _lastWaypoint = waypoints.Length - 1;
                ResetWaypoints();
            }
        }

        public void ResetWaypoints()
        {
            _currentWaypoint = 0;
            Actions.OnWaypointUpdate?.Invoke(this);
        }

        public void NextWaypoint()
        {
            if (_currentWaypoint < _lastWaypoint)
            {
                _currentWaypoint++;
                Actions.OnWaypointUpdate?.Invoke(this);
            }
        }

        public GameObject CurrentWaypointGameObject()
        {
            if (waypoints?.Length > 0)
            {
                return waypoints[_currentWaypoint];
            } else {
                Debug.LogWarning("No Waypoints set");
                return null;
            }
        }

    }
}
