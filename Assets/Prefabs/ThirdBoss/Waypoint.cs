using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Transform nyanKittenTransform;

    public Transform BugWaypoint()
    {
        return this.transform;
    }

    public Transform NyanKittenWaypoint()
    {
        return nyanKittenTransform;
    }
}
