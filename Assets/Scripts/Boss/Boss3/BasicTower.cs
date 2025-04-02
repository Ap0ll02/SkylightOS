using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    private SphereCollider mySphere;
    public Coroutine ActivateTower;
    public int attackRadius;
    public GameObject targetEnemy;
    public List<GameObject> waypoints = new();

    public void Start()
    {
        mySphere = GetComponent<SphereCollider>();
        mySphere.radius = attackRadius;
        ActivateTower = StartCoroutine(ActiveTower());
    }

    public IEnumerator ActiveTower()
    {
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tdEnemy"))
        {
            Debug.Log("Enemy Here");
            Debug.Log(
                "Stats: "
                    + GetComponent<Transform>().position
                    + " with pos: "
                    + other.GetComponent<Transform>().position
            );
            Attack();
        }
    }

    public override void Attack()
    {
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public override void GetWaypoints()
    {
        // Gather all waypoints within radius
        // We need a tag on the waypoints and a collider to allow for detection by the tower
    }
}
