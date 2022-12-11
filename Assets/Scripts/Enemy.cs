using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private GameObject bloodSplatterPrefab;

    public void Die()
    {
        Instantiate(bloodSplatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
