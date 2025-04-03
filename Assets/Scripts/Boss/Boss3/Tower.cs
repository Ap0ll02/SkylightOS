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

    // TODO: Use current enemy position for waypoints instead of grabbing
    // all of the waypoints
    public Towers towerType;

    public float damage;

    public float timeToDamage = 1f;

    public float cooldown;

    public bool isSpecial;

    public int level;

    public int[] costToUpgrade = new int[3];

    public GameObject targetWaypoint;
    public List<GameObject> enemyQueue;
    public List<GameObject> waypoints;

    public abstract void Attack();

    public abstract void GetWaypoints();

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision Checking:");
        if (other.gameObject.CompareTag("tdEnemy"))
        {
            enemyQueue.Add(other.gameObject);
            Debug.Log("Enemy Here");
            Debug.Log(
                "Stats: "
                    + GetComponent<Transform>().position
                    + " with pos: "
                    + other.gameObject.GetComponent<Transform>().position
            );
            Attack();
        }
        else if (other.gameObject.CompareTag("waypoint"))
        {
            waypoints.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("tdEnemy"))
        {
            if (enemyQueue.Remove(other.gameObject))
            {
                return;
            }
        }
    }
}
