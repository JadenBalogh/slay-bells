using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint[] adjacentWaypoints;

    public Waypoint GetRandomNext()
    {
        return adjacentWaypoints[Random.Range(0, adjacentWaypoints.Length)];
    }
}
