using System.Collections;
using UnityEngine;

public class SlowDownTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    public Coroutine ActivateTower;

    public new void Start()
    {
        ActivateTower = StartCoroutine(ActiveTower());

        // ========= Tower Upgrade Lists =========
        damages = new int[3] { 10, 15, 20 };
        costToUpgrade = new int[3] { 125, 250, 500};
        timesToDamage = new float[] { 1, 1, 1 };
        cooldowns = new float[] { 3, 2, 1f };
        isSpecials = new bool[] { false, false, true };
        radii = new float[] { 150, 225, 300 };
        slowDowns = new float[] { 0.4f, 0.5f, 0.6f };
        durations = new float[] { 3, 5, 8 };
        base.Start();
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
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public void DeleteTower()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
