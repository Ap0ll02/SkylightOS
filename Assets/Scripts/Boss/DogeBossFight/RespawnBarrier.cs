using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBarrier : MonoBehaviour
{
    public Transform respawnPoint; // Where the player should respawn\
    public Transform playerTransform;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the barrier is the player
        if (other.CompareTag("Player"))
        {
            // Move the player to the respawn point
            other.transform.position = respawnPoint.position;
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        if (playerTransform != null && respawnPoint != null)
        {
            playerTransform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogError("Missing playerTransform or respawnPoint!");
        }
    }


}
