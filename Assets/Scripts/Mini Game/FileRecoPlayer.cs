using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileRecoPlayer : MonoBehaviour
{
    public event Action<Collider2D> FileRecovered;
    public void OnTriggerEnter2D(Collider2D collision) {   
        Debug.Log("Collision Detected");
        // if(collision.gameObject.CompareTag("Player")){
        //     FileRecovered?.Invoke(this.gameObject);
        // }
        FileRecovered?.Invoke(collision);
    }
}
