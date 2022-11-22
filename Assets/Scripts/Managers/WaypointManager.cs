using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _waypoints;

    private int currentWaypoint = 0;
    private int lastWaypoint = 0;

    public void Start()
    {
        if (_waypoints is not null)
        {
            lastWaypoint = _waypoints.Length - 1;
            ResetWaypoints();
        }
    }

    public void ResetWaypoints()
    {
        currentWaypoint = 0;
        Actions.OnWaypointUpdate?.Invoke(this);
    }

    public void NextWaypoint()
    {
        if (currentWaypoint < lastWaypoint)
        {
            currentWaypoint++;
            Actions.OnWaypointUpdate?.Invoke(this);
        }
    }

    public GameObject CurrentWaypointGameObject()
    {
        return _waypoints == null ? null : _waypoints[currentWaypoint];
    }

}
