using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInsect : AbstractEnemy
{
    public void Start()
    {
        GetNewWaypoint();
        speed = 60f;
        maxHealth = 5;
        currentHealth = maxHealth;
        pointValue = 100;
    }

    public void Update()
    {
        Move();
    }
}
