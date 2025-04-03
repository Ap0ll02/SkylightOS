using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastInsect : AbstractEnemy
{
    void Start()
    {
        GetNewWaypoint();
        speed = 100f;
        maxHealth = 1;
        currentHealth = maxHealth;
        pointValue = 50;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
