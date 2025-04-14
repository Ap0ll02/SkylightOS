using System;
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

    #region UpgradeLists
    public int[] damages;
    public float[] timesToDamage;
    public float[] cooldowns;
    public bool[] isSpecials;
    public float[] radii;
    #endregion UpgradeLists
    public Towers towerType;

    public int damage;

    public float timeToDamage;

    public float cooldown;

    public bool isSpecial;

    protected bool canAttack = true;
    public int level;

    public int[] costToUpgrade = new int[3];

    public GameObject targetEnemy;
    public List<GameObject> enemyQueue = new();
    public Player playerScript;
    public float duration;
    public float slowPercent;
    public abstract void Attack();

    public void LookAtTarget(Transform target)
    {
        float speed = 50f * Time.deltaTime;
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (towerType == Towers.AOE)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                -speed
            );
        }
        else if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                speed
            );
        }
    }

    public void Update()
    {
        try
        {
            LookAtTarget(targetEnemy.transform);
        }
        catch (Exception e)
            when (e is NullReferenceException
                || e is UnassignedReferenceException
                || e is MissingReferenceException
            )
        {
            return;
        }
    }

    public GameObject GetTarget()
    {
        int maxInd = 0;
        // Gather all waypoints within radius
        // We need a tag on the waypoints and a collider to allow for detection by the tower
        foreach (GameObject en in enemyQueue)
        {
            try
            {
                if (en.TryGetComponent<AbstractEnemy>(out AbstractEnemy enemy))
                {
                    if (enemy.currentPosition > maxInd)
                    {
                        maxInd = enemy.currentPosition;
                        targetEnemy = en;
                    }
                }
            }
            catch (Exception e) when (e is MissingReferenceException)
            {
                continue;
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
        if (
            (
                other.gameObject.CompareTag("tdEnemy")
                || (other.gameObject.CompareTag("StealthEnemyTD") && isSpecial)
            ) && !enemyQueue.Contains(other.gameObject)
        )
        {
            canAttack = true;
            enemyQueue.Add(other.gameObject);
            Debug.Log("Enemy Added To Queue");
            if (other.TryGetComponent<AbstractEnemy>(out AbstractEnemy enemyScript))
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
                if (other.TryGetComponent<AbstractEnemy>(out AbstractEnemy enemyScript))
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
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage, duration, slowPercent);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemyQueue.Contains(enemy))
        {
            if (enemy.TryGetComponent<AbstractEnemy>(out AbstractEnemy en))
            {
                //playerScript.SetCurrency(playerScript.GetCurrency() + en.reward);
                _ = enemyQueue.Remove(enemy);
            }
        }

        targetEnemy = null;
        _ = GetTarget();
    }
}
