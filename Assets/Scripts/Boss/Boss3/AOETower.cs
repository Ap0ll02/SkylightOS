using UnityEngine;

public class AOETower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;

    public GameObject targetEnemy;

    public override void Attack()
    {
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public override void GetWaypoints() { }
}
