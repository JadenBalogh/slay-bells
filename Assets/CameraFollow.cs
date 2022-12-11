using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float followTime = 0.2f;

    private Vector2 currVel;

    private void Update()
    {
        Vector3 target = Vector2.SmoothDamp(transform.position, followTarget.position, ref currVel, followTime);
        transform.position = target + Vector3.forward * transform.position.z;
    }
}
