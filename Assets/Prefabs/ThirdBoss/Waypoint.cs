using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Waypoint : MonoBehaviour
{
    public Transform nyanKittenTransform;

    public Quaternion nyanKittenRotation()
    {
        return nyanKittenTransform.rotation;
    }
}
