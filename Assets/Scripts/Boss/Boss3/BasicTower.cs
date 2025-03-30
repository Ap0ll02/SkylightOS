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
}
