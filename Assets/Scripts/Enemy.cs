using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private GameObject bloodSplatterPrefab;

    private Waypoint targetWaypoint;

    protected override void Update()
    {
        moveDir = (targetWaypoint.transform.position - transform.position).normalized;

        if (Vector3.Distance(targetWaypoint.transform.position, transform.position) < 0.1f)
        {
            targetWaypoint = targetWaypoint.GetRandomNext();
        }

        spriteRenderer.flipX = moveDir.x > 0f;

        base.Update();
    }

    public void AssignWaypoint(Waypoint waypoint)
    {
        targetWaypoint = waypoint;
    }

    public void Die()
    {
        Instantiate(bloodSplatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
