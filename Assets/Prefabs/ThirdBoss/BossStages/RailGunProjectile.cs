using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunProcetile : Projectile
{
    public int Penetration = 5;

    public override void Start()
    {
        if (tm == null)
        {
            tm = GetComponentInParent<Tower>();
        }

        // Reference To Target Enemy
        GameObject temp = tm.GetTarget();
        // We need the target direction so we find the distance between the enemy and the tower. The vector should be normalized
        targetPosition = (temp.transform.position - transform.position).normalized;
        seeStealth = tm.isSpecial;
        Debug.Log("Enemy Locked: " + temp);
        transform.SetParent(null, true);
        StartCoroutine(ProjectileLife());
    }

    public override void Update()
    {
        Debug.Log("Projectile Update: ");
        // Move the projectile
        transform.position += targetPosition * speed * Time.deltaTime;
    }
    public override void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision: " + collision.gameObject);
        if (
            collision.CompareTag("tdEnemy")
            || (seeStealth && collision.CompareTag("StealthEnemyTD"))
        )
        {
            tm.HitEnemy(collision.gameObject);
            Penetration--;
        }
        if(Penetration <= 0)
            CleanUp();
    }

}
