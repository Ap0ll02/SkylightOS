using System.Collections;
using UnityEngine;

public class BasicTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    private SphereCollider mySphere;
    public Coroutine ActivateTower;
    public int attackRadius;
    public GameObject targetEnemy;

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

    public override void Attack()
    {
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public override void GetWaypoints()
    {
        int maxInd = 0;
        // Gather all waypoints within radius
        // We need a tag on the waypoints and a collider to allow for detection by the tower
        foreach (GameObject wp in waypoints)
        {
            if (waypoints.IndexOf(wp) > maxInd)
            {
                targetWaypoint = wp;
            }
        }
    }
}
