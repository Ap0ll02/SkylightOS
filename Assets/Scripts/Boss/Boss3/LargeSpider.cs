using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeSpider : AbstractInsect
{
    void Start()
    {
        GetNewWaypoint();
        speed = 4f + BugSpeedBuff;
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
