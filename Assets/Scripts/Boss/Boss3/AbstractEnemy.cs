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
    public float originalSpeed;
    #endregion enemystats
    public int currentPosition = 0;
    public NavigationManager navi;
    public Vector3 nextWaypoint;
    public Animator animator;
    private bool hasBeenSlowed;
    private Coroutine slowdownCoroutine;
    public event Action<GameObject> EnemyDeath;

    // private UnityEvent EnemyDeath; Currently unneeded
    public virtual void TakeDamage(int damage, float time = 0, float percentage = 0 )
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Death();
        if (percentage > 0)
            HandleSlowdown(time, percentage);
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

    public void HandleSlowdown(float time, float percentage)
    {
        bool isGreaterDebuff = (speed * (1 - percentage)) > speed;
        if (hasBeenSlowed && isGreaterDebuff)
        {
            speed = originalSpeed * (1-percentage);
            StopCoroutine(slowdownCoroutine);
            slowdownCoroutine = StartCoroutine(slowdown(time));
        }
        else if (hasBeenSlowed)
        {
            StopCoroutine(slowdownCoroutine);
            slowdownCoroutine = StartCoroutine(slowdown(time));
        }
        else
        {
            hasBeenSlowed = true;
            originalSpeed = speed;
            speed = originalSpeed * (1-percentage);
            StartCoroutine(slowdown(time));
        }
    }
    public IEnumerator slowdown(float time)
    {
        yield return new WaitForSeconds(time);
        hasBeenSlowed = false;
        speed = originalSpeed;
    }
}
