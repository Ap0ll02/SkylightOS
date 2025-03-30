using System.Collections;
using UnityEngine;

public class AOETower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;

    public override void Attack()
    {
        Debug.Log("Attack");
    }
}
