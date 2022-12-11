using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private Enemy npcPrefab;
    [SerializeField] private int npcCount = 10;
    [SerializeField] private Waypoint[] waypoints;

    private void Start()
    {
        for (int i = 0; i < npcCount; i++)
        {
            Waypoint targetWaypoint = waypoints[Random.Range(0, waypoints.Length)];
            Enemy enemy = Instantiate(npcPrefab, targetWaypoint.transform.position, Quaternion.identity);
            enemy.AssignWaypoint(targetWaypoint.GetRandomNext());
        }
    }
}
