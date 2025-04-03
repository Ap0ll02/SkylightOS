using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInsect : AbstractEnemy
{
    public void Start()
    {
        animator = GetComponent<Animator>();
        navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = 60f;
        maxHealth = 5;
        currentHealth = maxHealth;
        pointValue = 100;
        damage = 6;
    }

    public void Update()
    {
        Move();
    }
}
