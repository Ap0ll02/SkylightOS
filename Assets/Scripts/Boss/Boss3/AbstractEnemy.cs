using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy: MonoBehaviour
{
    // contains all the base variables used by every variant of enemy.
    #region enemystats
        public int pointValue;
        public int currentHealth;
        public int maxHealth;
        public int damage;
        public float speed;
    #endregion enemystats
    public int currentPosition = 0;
    public NavigationManager navi;
    public Vector3 nextWaypoint;
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Move()
    {
        if (Vector3.Distance(this.transform.position, nextWaypoint) < 0.001f)
        {
            GetNewWaypoint();
        }
        else
        {
            animator.SetTrigger("Moving");
            // Move the object towards the waypoint as before
            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);

        }
    }

    public void GetNewWaypoint()
    {
        var waypointGameObject = navi.NextWaypoint(currentPosition++);
        transform.rotation = waypointGameObject.transform.rotation;
        Debug.Log("Current Waypoint:"+ waypointGameObject.name);
        nextWaypoint = waypointGameObject.transform.position;
    }

    //public abstract void SlowDownHit(int damage = 0, float percent = 1, float duration = 0);
    // public abstract void Death();
    // public abstract void Move();

}
