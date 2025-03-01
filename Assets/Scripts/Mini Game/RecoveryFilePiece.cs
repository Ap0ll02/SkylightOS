using System;
using UnityEngine;

public class RecoveryFilePiece : MonoBehaviour
{
    public event Action<GameObject> FileRecovered;
    void OnTriggerEnter2D(Collider2D collision) {   
        Debug.Log("Collision Detected");
        // if(collision.gameObject.CompareTag("Player")){
        //     FileRecovered?.Invoke(this.gameObject);
        // }
        FileRecovered?.Invoke(this.gameObject);
    }
}
