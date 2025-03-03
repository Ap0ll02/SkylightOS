using System;
using UnityEngine;

public class ExitWall : MonoBehaviour
{
    public event Action GameOverEvent;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) GameOverEvent?.Invoke();
    }
}
