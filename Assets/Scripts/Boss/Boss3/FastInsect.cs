using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastInsect : AbstractInsect
{
    void Start()
    {
        GetNewWaypoint();
        speed = 20f + BugSpeedBuff;
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
