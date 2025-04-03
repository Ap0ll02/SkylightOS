using UnityEngine;

public class SlowDownTower : Tower
{
    public GameObject targetEnemy;

    public override void Attack()
    {
        Debug.Log("Attack");
    }

    public override void GetWaypoints() { }
}
