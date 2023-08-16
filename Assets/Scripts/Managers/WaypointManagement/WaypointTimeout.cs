using System.Collections;
using System.Collections.Generic;
using Key;
using UnityEngine;

public class WaypointTimeout : MonoBehaviour,ITimeAllowedToWaypoint
{
    [SerializeField] private float timeAllowedToWaypoint = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
