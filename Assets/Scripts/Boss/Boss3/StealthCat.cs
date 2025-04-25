using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCat : AbstractEnemy
{
    public void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = Random.Range(80f, 100f);
        maxHealth = 120;
        currentHealth = maxHealth;
        damage = 1;
        reward = 15;
        pointValue = 2;
    }

    public void Update()
    {
        Move();
    }

    public override void GetNewWaypoint()
    {
        var waypointGameObject = navi.NextWaypoint(currentPosition++);
        transform.rotation = waypointGameObject.GetComponent<Waypoint>().nyanKittenRotation();
        // Debug.Log("Current Waypoint:" + waypointGameObject.name);
        nextWaypoint = waypointGameObject.transform.position;
    }

    public override void Move()
    {
        if (Vector3.Distance(this.transform.position, nextWaypoint) < 0.001f)
        {
            Debug.Log("Reached Waypoint in the override move function");
            GetNewWaypoint();
        }
        else
        {
            animator.SetBool("StealthKitten", true);
            // Move the object towards the waypoint as before
            transform.position = Vector3.MoveTowards(
                transform.position,
                nextWaypoint,
                speed * Time.deltaTime
            );
        }
    }
}
