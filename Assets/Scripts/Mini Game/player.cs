using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Action<Collider2D> OnTObs;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("DriverPlayer"))
        {
            Debug.Log ("Triggered");
            Destroy(other);
            OnTObs?.Invoke(other);
        }
    }
}
