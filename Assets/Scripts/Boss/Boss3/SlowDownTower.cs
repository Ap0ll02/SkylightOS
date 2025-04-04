using System.Collections;
using UnityEngine;

public class SlowDownTower : Tower
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

    public IEnumerator ActiveTower()
    {
        Attack();
        yield return new WaitForSeconds(cooldown);
    }

    public override void Attack()
    {
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }
}
