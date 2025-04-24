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
        speed = Random.Range(40f,50f);
        maxHealth = 15;
        currentHealth = maxHealth;
        pointValue = 5;
        reward = 25;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
