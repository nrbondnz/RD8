using System.Linq;
using Key;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Managers.WaypointManagement
{
    /// <summary>
    /// Tracks current waypoint and if using timed then time allowed to waypoint
    /// In addition if waypointTimeAdds is set to true the remaining waypoint time after one just completed
    /// is added to the available time to get to the next waypoint
    /// </summary>
    public class WaypointManager : MonoBehaviour
    {
        [SerializeField] private WaypointSubscriber[] waypoints;
        //private int _masterIndex = 5;
        private static WaypointManager _instance;
        private int _currentWaypoint = 0;

        [SerializeField] private bool waypointTimeAdds;
        
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
            ResetWaypoints();
            //if (waypoints is not null)
            //{
            //    _lastWaypoint = waypoints.Length - 1;
                //ResetWaypoints();
            //}
        }

        public void setTimeAllowedToNextWaypoint()
        {
            // This is the next waypoint so if first its zero time
            // if not it could add if waypoint manager is doing that
            WaypointSubscriber currentWaypoint = CurrentWaypointSubscriber();
            /*if (currentWaypoint.IsConvertibleTo<ITimeAllowedToWaypoint>(currentWaypoint))
            {*/
                // Debug.Log("GameObject on waypoint implements ITimeAllowedToWaypoint");
                //ITimeAllowedToWaypoint timeAllowedToWaypoint =
                //    Utilities.GameObjectUtilities.TimeAllowedToWaypointComponent(currentWaypoint);
                if (( ! currentWaypoint.IsUnityNull() ) &&
                    currentWaypoint.TimeAllowedToWaypoint() > 0.0)
                {
                    float timeRemainingToWaypoint = GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining;
                    float timeToNextWaypoint = currentWaypoint.TimeAllowedToWaypoint();
                    GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining = timeToNextWaypoint +
                        (waypointTimeAdds ? timeRemainingToWaypoint : 0.0f);

                    //Actions.OnGameStatusChanged(GamePlayManager.GetInstance().GameStatus);
                }
                else
                {
                    GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining = 0.0f;
                }
            /*}
            else
            {
                GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining = 0.0f;
            } */
        }

        public void ResetWaypoints()
        {
            _currentWaypoint = 0;
            GamePlayManager.GetInstance().GameStatus.WaypointTimeRemaining = 0.0f;
            setTimeAllowedToNextWaypoint();
            Actions.OnWaypointUpdate?.Invoke(this);
        }

        public void NextWaypoint()
        {
            if (_currentWaypoint < waypoints.Length)
            {
                _currentWaypoint++;
                setTimeAllowedToNextWaypoint();
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

        public void AddSubscriber(WaypointSubscriber waypointSubscriber, int index)
        {
            Debug.Log("AddSubscriber index " + index + " set to " + waypointSubscriber.name);
            this.waypoints[index] = waypointSubscriber;
            ResetWaypoints();
            /*if (isLast)
            {
                Debug.Log("AddSubscriber is last is true");
                for (int i = index + 1; i < _masterIndex; i++)
                {
                    Debug.Log("Index " + i + " set to null");
                    this.waypoints[i] = null;
                }
                
            }*/
        }
    }
}
