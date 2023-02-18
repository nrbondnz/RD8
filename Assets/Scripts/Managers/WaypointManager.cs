using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class WaypointManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] waypoints;

        private int _currentWaypoint = 0;
        private int _lastWaypoint = 0;

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
            return waypoints == null ? null : waypoints[_currentWaypoint];
        }

    }
}
