using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicInsect : AbstractEnemy
{
    public void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = Random.Range(100f, 120f);
        maxHealth = 5;
        currentHealth = maxHealth;
        pointValue = 100;
        damage = 1;
        reward = 1;
    }

    public void Update()
    {
        Move();
    }
}
