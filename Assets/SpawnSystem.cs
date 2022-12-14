using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private Enemy[] npcPrefabs;
    [SerializeField] private int npcCount = 10;
    [SerializeField] private Waypoint[] waypoints;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        for (int i = 0; i < npcCount; i++)
        {
            Waypoint targetWaypoint = waypoints[Random.Range(0, waypoints.Length)];
            Enemy enemy = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Length)], targetWaypoint.transform.position, Quaternion.identity);
            enemy.AssignWaypoint(targetWaypoint.GetRandomNext());

            if (npcCount % 10 == 0)
            {
                yield return null;
            }
        }
    }
}
