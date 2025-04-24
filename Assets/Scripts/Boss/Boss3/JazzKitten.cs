using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JazzKitten : AbstractEnemy
{
    public void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = Random.Range(110f, 130f);
        maxHealth = 150;
        currentHealth = maxHealth;
        damage = 1;
        pointValue = 3;
        reward = 75;
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
            animator.SetBool("JazzKitten", true);
            // Move the object towards the waypoint as before
            transform.position = Vector3.MoveTowards(
                transform.position,
                nextWaypoint,
                speed * Time.deltaTime
            );
        }
    }
}
