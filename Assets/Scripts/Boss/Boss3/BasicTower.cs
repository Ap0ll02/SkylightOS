using System.Collections;
using UnityEngine;

public class BasicTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    public Coroutine ActivateTower;

    // TODO: Change array stats for all tower types
    // UpdateLevel Function To Change Stats To New Ones
    // Including activation of TowerTwo and Three Objects
    public new void Start()
    {
        base.Start();
        ActivateTower = StartCoroutine(ActiveTower());

        // ========= Tower Upgrade Lists =========
        damages = new int[3] { 35, 60, 100 };
        timesToDamage = new float[] { 1, 1, 1 };
        cooldowns = new float[] { 2, 1, 0.75f };
        isSpecials = new bool[] { false, false, true };
        radii = new float[] { attackRadius, 12, 17 };
        // =======================================
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

    public override void Attack()
    {
        Debug.Log("Attacking: " + targetEnemy);
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public void DeleteTower()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
