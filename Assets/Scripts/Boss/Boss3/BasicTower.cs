using UnityEngine;

public class BasicTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;

    public override void Attack()
    {
        Debug.Log("Attack");
        projectile = Instantiate(
            projectilePrefab,
            parent: this.gameObject.GetComponent<Transform>()
        );
    }
}
