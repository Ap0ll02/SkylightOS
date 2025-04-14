using System.Collections;
using UnityEngine;

public class SlowDownTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    public Coroutine ActivateTower;

    public new void Start()
    {
        base.Start();
        ActivateTower = StartCoroutine(ActiveTower());

        // ========= Tower Upgrade Lists =========
        damages = new int[3] { 5, 20, 60 };
        timesToDamage = new float[] { 1, 1, 1 };
        cooldowns = new float[] { 3, 2, 1.25f };
        isSpecials = new bool[] { false, false, true };
        radii = new float[] { 85, 95, 105 };
        slowDowns = new float[] { 0.4f, 0.5f, 0.6f };
        durations = new float[] { 3, 5, 8 };
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
