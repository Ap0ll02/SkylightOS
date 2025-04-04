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

    public float timeToDamage = 1f;

    public float cooldown;

    public bool isSpecial;

    public int level;

    public int[] costToUpgrade = new int[3];

    public GameObject targetEnemy;
    public List<GameObject> enemyQueue = new();

    public abstract void Attack();

    public void GetTarget()
    {
        int maxInd = 0;
        // Gather all waypoints within radius
        // We need a tag on the waypoints and a collider to allow for detection by the tower
        foreach (GameObject en in enemyQueue)
        {
            if (en.GetComponent<AbstractEnemy>().currentPosition > maxInd)
            {
                maxInd = en.GetComponent<AbstractEnemy>().currentPosition;
                targetEnemy = en;
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Checking:");
        if (other.gameObject.CompareTag("tdEnemy") && !enemyQueue.Contains(other.gameObject))
        {
            enemyQueue.Add(other.gameObject);
            Debug.Log("Enemy Here");
            Debug.Log(
                "Stats: "
                    + GetComponent<Transform>().position
                    + " with pos: "
                    + other.gameObject.GetComponent<Transform>().position
            );
        }
        GetTarget();
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tdEnemy"))
        {
            if (enemyQueue.Remove(other.gameObject))
            {
                return;
            }
        }
    }

    public void HitEnemy(GameObject enemy)
    {
        enemy.GetComponent<AbstractEnemy>().TakeDamage(damage);
    }
}
