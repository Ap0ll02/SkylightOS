using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractEnemy : MonoBehaviour
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
    public event Action<GameObject> EnemyDeath;

    // private UnityEvent EnemyDeath; Currently unneeded
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth + "Minus Damage: " + damage);
        if (currentHealth <= 0)
            Death();
    }

    public virtual void Move()
    {
        if (Vector3.Distance(this.transform.position, nextWaypoint) < 0.001f)
        {
            GetNewWaypoint();
        }
        else
        {
            animator.SetBool("Moving", true);
            // Move the object towards the waypoint as before
            transform.position = Vector3.MoveTowards(
                transform.position,
                nextWaypoint,
                speed * Time.deltaTime
            );
        }
    }

    public virtual void GetNewWaypoint()
    {
        var waypointGameObject = navi.NextWaypoint(currentPosition++);
        transform.rotation = waypointGameObject.transform.rotation;
        // Debug.Log("Current Waypoint:" + waypointGameObject.name);
        nextWaypoint = waypointGameObject.transform.position;
    }

    // I have to think of this method a little bit more
    /// <summary>
    /// If I make it a unity event then all towers would invoke the unity death event removing the specific enemy. Maybe we only want to remove the bug the tower sees
    /// We could add conditional object to check if everytower had this bug in its view this can cause more problems
    /// basically unity events invoke methods that all the towers share which has potential to remove our bugs from all towers are cause null references
    /// </summary>
    //public void SubscribeToDeath(UnityAction death)
    public virtual void Death()
    {
        // Debug.Log("Hey We Hit The GPU Killing Bug");
        EnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }
    //public abstract void SlowDownHit(int damage = 0, float percent = 1, float duration = 0);
}
