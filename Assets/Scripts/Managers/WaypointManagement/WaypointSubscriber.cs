using Key;
using UnityEngine;

namespace Managers.WaypointManagement
{
    public class WaypointSubscriber : MonoBehaviour,ITimeAllowedToWaypoint
    {
        [SerializeField] private float timeAllowedToWaypoint = 0.0f;
        [SerializeField] private int index;
        //[SerializeField] private bool isLast = false;
        
        //  Awake are all called before the first start on a scene
        void Awake()
        {
            Debug.Log("WaypointSubscriber : " + this + " start method");
            //WaypointManager.GetInstance().AddSubscriber(this, index);
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public float TimeAllowedToWaypoint()
        {
            return this.timeAllowedToWaypoint;
        }
        
    }
}
