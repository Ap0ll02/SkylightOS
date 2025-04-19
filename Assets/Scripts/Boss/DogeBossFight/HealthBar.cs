using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform target;  // Assign the boss object
    public Transform healthFill; // Assign the HealthFill sprite

    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        float healthPercent = currentHealth / maxHealth;

        // Scale the health bar fill
        healthFill.localScale = new Vector3(healthPercent, 1, 1);
    }
}
