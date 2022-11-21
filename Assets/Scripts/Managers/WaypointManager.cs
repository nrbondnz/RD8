using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _waypoints;

    private int currentWaypoint = 0;
    private int lastWaypoint = 0;
    
    public void Start()
    {
        lastWaypoint = _waypoints.Length - 1;
    }

    public void ResetWaypoints()
    {
        currentWaypoint = 0;
    }

    public void NextWaypoint()
    {
        if (currentWaypoint < lastWaypoint)
        {
            currentWaypoint++;
            Actions.OnWaypointUpdate?.Invoke(_waypoints[currentWaypoint]);
        }
    }

}
