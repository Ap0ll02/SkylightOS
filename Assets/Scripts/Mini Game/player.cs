using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Action OnTObs;
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log ("Triggered");
        OnTObs?.Invoke();
    }
}
