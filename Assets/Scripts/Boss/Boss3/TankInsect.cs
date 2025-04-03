using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInsect : AbstractEnemy
{
    void Start()
    {
        GetNewWaypoint();
        speed = 30f;
        maxHealth = 15;
        currentHealth = maxHealth;
        pointValue = 300;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
