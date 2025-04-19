using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doge : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    public Transform player;  // Reference to the player's transform
    public float speed = 3f;  // Speed at which the enemy follows
    public float followRange = 3f;  // Distance within which the enemy starts following
    public bool isDead = false;
    public GameObject laserPrefab;  // Reference to the laser prefab
    public Transform firePoint;     // Position where the laser will be fired from
    public float laserSpeed = 10f;



    private void Attack1()
    {
        if (player == null || laserPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing references for Attack1.");
            return;
        }

        // Instantiate the laser prefab at the firePoint position and rotation
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        // Calculate the direction toward the player
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Apply velocity to the laser to make it move toward the player
        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        if (laserRb != null)
        {
            laserRb.velocity = direction * laserSpeed;
        }

    }

    private void Attack2()
    {

    }

    private void Attack3()
    {

    }

    private IEnumerator BossEnabled()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned.");
            yield break; // Exit the coroutine if no player is assigned
        }

        while (!isDead) // Continuously check and follow the player
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the player is within the follow range
            if (distanceToPlayer <= followRange)
            {
                // Calculate the direction towards the player
                Vector2 direction = (player.position - transform.position).normalized;

                // Move the enemy towards the player
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            // Wait until the next frame before continuing
            yield return null;
        }
    }


    public void  OnEnable()
    {
        Debug.Log("Doge Boss Enabled");
        StartCoroutine(BossEnabled());
    }

}
