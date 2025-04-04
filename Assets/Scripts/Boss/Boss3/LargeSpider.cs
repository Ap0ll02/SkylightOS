using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeSpider : AbstractEnemy
{
    void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = 40f;
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
