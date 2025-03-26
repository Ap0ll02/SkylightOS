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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public abstract void SlowDownHit(int damage = 0, float percent = 1, float duration = 0);
    // public abstract void Death();
    // public abstract void Move();

}
