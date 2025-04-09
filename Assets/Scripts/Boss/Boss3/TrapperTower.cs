using System.Collections;
using UnityEngine;

public class TrapperTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    private SphereCollider mySphere;
    public Coroutine ActivateTower;
    public int attackRadius;

    public void Start()
    {
        mySphere = GetComponent<SphereCollider>();
        mySphere.radius = attackRadius;
        ActivateTower = StartCoroutine(ActiveTower());
    }

    public override void Attack()
    {
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public IEnumerator ActiveTower()
    {
        while (true)
        {
            if (targetEnemy != null && canAttack)
            {
                Attack();
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
