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
    Vector2 targetPosition;
    NavigationManager navi;
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Move()
    {
        var nextWaypoint= navi.NextWaypoint(currentPosition);
        targetPosition = nextWaypoint.GetComponent<Vector2>();
        if (Vector2.Distance(this.transform.position, targetPosition) < 0.1f)
        {
            targetPosition = navi.NextWaypoint(currentPosition).GetComponent<Vector2>();
        }
    }

    public abstract void SlowDownHit(int damage = 0, float percent = 1, float duration = 0);
    // public abstract void Death();
    // public abstract void Move();

}
