using System;
using UnityEngine;

public class RecoveryFilePiece : MonoBehaviour
{
    public event Action<GameObject> FileRecovered;
    void OnTriggerEnter2D(Collider2D collision) {   
        if(collision.gameObject.CompareTag("Player")){
            FileRecovered?.Invoke(collision.gameObject);
        }
    }
}
