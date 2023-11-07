using System.Linq;
using Key;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Managers.WaypointManagement
{
    public class WaypointManager : MonoBehaviour
    {
        private WaypointSubscriber[] waypoints;
        private int _masterIndex = 5;
        private static WaypointManager _instance;
        private int _currentWaypoint = 0;
        //private float _timeAllowedToWaypoint = 0.0f;
        //private int _lastWaypoint = 0;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("WaypointManager Trying second Awake");
                Destroy(gameObject);
                return;
            }

            Debug.Log("WaypointManager Awake");
            _instance = this;
            _instance.waypoints = new WaypointSubscriber[_masterIndex];
            DontDestroyOnLoad(gameObject);
            //SetupLastLevel();
        }

        

        /// <summary>
        /// gets the (single per run) instance of the WaypointManager
        /// which will be created during Awake
        /// </summary>
        /// <returns>WaypointManager</returns>
        public static WaypointManager GetInstance()
        {
            return _instance;
        }
        
        public bool HasWaypoints()
        {
            return ( ( ! waypoints.IsUnityNull() ) && ( waypoints.Length > 0 ) );
        }
        
        /*public void OnDrawGizmos()
        {
            if ( ( ! waypoints.IsUnityNull() ) && ( waypoints.Length > 0) )
            {
                bool changesImplTimeAllowed = false;
                WaypointSubscriber gameObj = null;
                foreach (var waypoint in waypoints)
                {
                    if (waypoint.IsConvertibleTo<ITimeAllowedToWaypoint>(waypoint))
                    {
                        changesImplTimeAllowed = true;
                    }
                    else
                    {
                        changesImplTimeAllowed = false;
                        gameObj = waypoint;
                        break;
                    }
                }

                if (changesImplTimeAllowed)
                {
                    Gizmos.color = Color.green;
                    //Gizmos.DrawRay(transform.position, WaypointManager.position - transform.position);
                }
                else
                {
                    Gizmos.color = Color.magenta;
                    Debug.LogError("Waypoint : " + gameObj + " does not implement ITimeAllowedToWaypoint" );
                }
            }
            else
            {
                Gizmos.color = Color.red;
                Debug.LogWarning("WaypointManager : elements not set");
                
            }
            Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.5f);
        }*/

        public void Start()
        {
            //if (waypoints is not null)
            //{
            //    _lastWaypoint = waypoints.Length - 1;
                //ResetWaypoints();
            //}
        }

        public void setTimeAllowedToWaypoint()
        {
            WaypointSubscriber currentWaypoint = CurrentWaypointSubscriber();
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
            if (_currentWaypoint < _masterIndex)
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

        public WaypointSubscriber CurrentWaypointSubscriber()
        {
            if (waypoints?.Length > 0)
            {
                return waypoints[_currentWaypoint];
            } else {
                Debug.LogWarning("No Waypoints set");
                return null;
            }
        }

        public void AddSubscriber(WaypointSubscriber waypointSubscriber, int index, bool isLast)
        {
            Debug.Log("AddSubscriber index " + index + " set to " + waypointSubscriber.name);
            this.waypoints[index] = waypointSubscriber;
            if (isLast)
            {Debug.Log("AddSubscriber is last is true");
                for (int i = index + 1; i < _masterIndex; i++)
                {
                    Debug.Log("Index " + i + " set to null");
                    this.waypoints[i] = null;
                }
                ResetWaypoints();
            }
        }
    }
}
