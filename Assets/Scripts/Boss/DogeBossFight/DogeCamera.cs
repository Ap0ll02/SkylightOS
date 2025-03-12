using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0f, 0.05f);

    public Vector3 currentVelocity;

    void LateUpdate()
    {
            transform.position = target.position + offset;
    }
}
