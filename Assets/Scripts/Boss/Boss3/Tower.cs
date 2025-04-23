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
    public float[] durations;
    public float[] slowDowns;
    #endregion UpgradeLists
    public GameObject postProcessObj;
    public float attackRadius;
    public SphereCollider mySphere;
    public Towers towerType;
    public List<GameObject> towerDesigns;
    public int damage;
    public int displayCost;
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

    public void Start()
    {
        mySphere = GetComponent<SphereCollider>();
        mySphere.radius = radii[0];
        displayCost = costToUpgrade[0];
        damage = damages[0];
        timeToDamage = timesToDamage[0];
        cooldown = cooldowns[0];
        isSpecial = isSpecials[0];
        duration = durations[0];
        slowPercent = slowDowns[0];
    }

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

    public bool UpgradeTower()
    {
        // Ensure tower is appropriate level
        if (level > 3)
        {
            Debug.Log("Cannot Upgrade Past Level 3");
            return false;
        }

        // Fix all standard attributes
        damage = damages[level - 1];
        attackRadius = radii[level - 1];
        cooldown = cooldowns[level - 1];
        isSpecial = isSpecials[level - 1];
        timeToDamage = timesToDamage[level - 1];
        duration = durations[level - 1];

        // Slowdown Stuff
        if (towerType == Towers.SlowDown)
        {
            slowPercent = slowDowns[level - 1];
        }

        // Tower Look Update
        int i = 0;
        foreach (GameObject t in towerDesigns)
        {
            t.SetActive(false);
            if (i == level - 1)
            {
                t.SetActive(true);
            }
            i++;
        }
        return true;
    }

    public GameObject tr;

    public void Glow(bool on)
    {
        if (on)
        {
            tr = Instantiate(postProcessObj, parent: transform);
            tr.transform.position += new Vector3(0, 20f, 0);
        }
        else
        {
            if (tr)
            {
                TrailRenderer tro = tr.GetComponentInChildren<TrailRenderer>();
                if (tro)
                {
                    tro.time = 0f;
                }
                tr.SetActive(false);
                DestroyImmediate(tr);
            }
        }
    }
}
