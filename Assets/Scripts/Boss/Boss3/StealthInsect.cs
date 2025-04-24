using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthInsect : AbstractEnemy
{
    public void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = Random.Range(70f, 90f);
        maxHealth = 100;
        currentHealth = maxHealth;
        pointValue = 100;
        damage = 1;
        reward = 6;
        pointValue = 2;
    }

    public void Update()
    {
        Move();
    }
}
