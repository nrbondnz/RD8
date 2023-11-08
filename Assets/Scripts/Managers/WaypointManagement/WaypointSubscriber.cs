using Key;
using UnityEngine;

namespace Managers.WaypointManagement
{
    public class WaypointSubscriber : MonoBehaviour,ITimeAllowedToWaypoint
    {
        [SerializeField] private float timeAllowedToWaypoint = 0.0f;
        [SerializeField] private int index;
        [SerializeField] private bool isLast = false;
        
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("WaypointSubscriber : " + this + " start method");
            WaypointManager.GetInstance().AddSubscriber(this, index, isLast);
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public float TimeAllowedToWaypoint()
        {
            return this.timeAllowedToWaypoint;
        }

        public bool IsLast()
        {
            return isLast;
        }
    }
}
