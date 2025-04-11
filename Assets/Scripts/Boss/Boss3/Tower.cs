using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public enum Towers
    {
        Basic,
        SlowDown,
        Mage,
        AOE,
        Trapper,
    }

    public Towers towerType;

    public int damage;

    public float timeToDamage;

    public float cooldown;

    public bool isSpecial;
    public float effectDuration;
    public float slowPercent;
    protected bool canAttack = true;
    public int level;

    public int[] costToUpgrade = new int[3];

    public GameObject targetEnemy;
    public List<GameObject> enemyQueue = new();

    public abstract void Attack();

    public GameObject GetTarget()
    {
        int maxInd = 0;
        // Gather all waypoints within radius
        // We need a tag on the waypoints and a collider to allow for detection by the tower
        foreach (GameObject en in enemyQueue)
        {
            try {

              if (en.GetComponent<AbstractEnemy>().currentPosition > maxInd)
            {
                maxInd = en.GetComponent<AbstractEnemy>().currentPosition;
                targetEnemy = en;
            }

            } catch (Exception e) {
              enemyQueue.Remove(en);
            }
        }

        canAttack = true;
        if (targetEnemy == null)
        {
            canAttack = false;
        }
        return targetEnemy;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Checking:");
        if (other.gameObject.CompareTag("tdEnemy") && !enemyQueue.Contains(other.gameObject))
        {
            canAttack = true;
            enemyQueue.Add(other.gameObject);
            Debug.Log("Enemy Added To Queue");

            AbstractEnemy enemyScript = other.gameObject.GetComponent<AbstractEnemy>();
            if (enemyScript != null)
            {
                enemyScript.EnemyDeath += RemoveEnemy;
            }
        }
        _ = GetTarget();
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tdEnemy"))
        {
            if (enemyQueue.Remove(other.gameObject))
            {
                AbstractEnemy enemyScript = other.gameObject.GetComponent<AbstractEnemy>();
                if (enemyScript != null)
                {
                    enemyScript.EnemyDeath -= RemoveEnemy;
                }
                targetEnemy = null;
                _ = GetTarget();

                return;
            }
        }
    }

    public void HitEnemy(GameObject enemy)
    {
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage, effectDuration, slowPercent);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemyQueue.Remove(enemy))
        {
            AbstractEnemy enemyScript = enemy.GetComponent<AbstractEnemy>();
            if (enemyScript != null)
            {
                enemyScript.EnemyDeath -= RemoveEnemy;
            }
        }

        targetEnemy = null;
        _ = GetTarget();
    }
}
