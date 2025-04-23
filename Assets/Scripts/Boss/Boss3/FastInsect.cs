using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class FastInsect : AbstractEnemy
{
    void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = Random.Range(130f, 150f);
        maxHealth = 35;
        currentHealth = maxHealth;
        pointValue = 2;
        damage = 1;
        reward = 25;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
