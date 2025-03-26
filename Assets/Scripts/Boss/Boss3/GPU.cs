using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPU : MonoBehaviour
{
    public int health;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject thingHittingOurGPU = collision.gameObject;

        if (thingHittingOurGPU.CompareTag("Enemy"))
        {
            Debug.Log("Some Fucker Hit Our GPU");
            var enemy = thingHittingOurGPU.GetComponent<AbstractEnemy>();
            if (enemy != null)
            {
                health -= enemy.damage;
            }
        }

    }
}
