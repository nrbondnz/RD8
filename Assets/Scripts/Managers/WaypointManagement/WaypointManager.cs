using System;
using System.Collections;
using System.Collections.Generic;
using Key;
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
        //private float _timeAllowedToWaypoint = 0.0f;
        private int _lastWaypoint = 0;

        public bool hasWaypoints()
        {
            return ( ( ! waypoints.IsUnityNull() ) && ( waypoints.Length > 0 ) );
        }
        
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

        public void setTimeAllowedToWaypoint()
        {
            GameObject currentWaypoint = CurrentWaypointGameObject();
            if (currentWaypoint.IsConvertibleTo<ITimeAllowedToWaypoint>(currentWaypoint))
            {
                Debug.Log("GameObject on waypoint implements ITimeAllowedToWaypoint");
                ITimeAllowedToWaypoint timeAllowedToWaypoint =
                    Utilities.GameObjectUtilities.TimeAllowedToWaypointComponent(currentWaypoint);
                if (( ! timeAllowedToWaypoint.IsUnityNull() ) &&
                    timeAllowedToWaypoint.TimeAllowedToWaypoint() > 0.0)
                {
                    GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining =
                        timeAllowedToWaypoint.TimeAllowedToWaypoint();
                    //Actions.OnGameStatusChanged(GamePlayManager.GetInstance().GameStatus);
                }
                else
                {
                    GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining = 0.0f;
                }
            }
            else
            {
                GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining = 0.0f;
            } 
        }

        public void ResetWaypoints()
        {
            _currentWaypoint = 0;
            setTimeAllowedToWaypoint();
            Actions.OnWaypointUpdate?.Invoke(this);
        }

        public void NextWaypoint()
        {
            if (_currentWaypoint < _lastWaypoint)
            {
                _currentWaypoint++;
                setTimeAllowedToWaypoint();
                Actions.OnWaypointUpdate?.Invoke(this);
            }
            else
            {
                // last waypoint - reset the timer
                //this._timeAllowedToWaypoint = 0.0f;
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
