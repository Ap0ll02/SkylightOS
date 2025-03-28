using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInsect : AbstractInsect
{
    public void Start()
    {
        GetNewWaypoint();
        speed = 30f + BugSpeedBuff;
        maxHealth = 5;
        currentHealth = maxHealth;
        pointValue = 100;
    }

    public void Update()
    {
        Move();
    }
}
