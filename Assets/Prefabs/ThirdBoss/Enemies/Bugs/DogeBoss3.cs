using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogeBoss3 : AbstractEnemy
{

    public void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = 30;
        maxHealth = 22000;
        currentHealth = maxHealth;
        pointValue = 100;
        damage = 20;
    }

    public void Update()
    {
        Move();
        if (currentHealth < 2000)
            SceneManager.LoadScene("EndingCutscene");
    }

    public override void GetNewWaypoint()
    {
        var waypointGameObject = navi.NextWaypoint(currentPosition++);
        transform.rotation = waypointGameObject.GetComponent<Waypoint>().nyanKittenRotation();
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
            // Move the object towards the waypoint as before
            transform.position = Vector3.MoveTowards(
                transform.position,
                nextWaypoint,
                speed * Time.deltaTime
            );
        }
    }
}
