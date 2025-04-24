using System.Collections;
using UnityEngine;

public class MageTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    public Coroutine ActivateTower;
    public Animator animator;

    public new void Start()
    {
        // ========= Tower Upgrade Lists =========
        damages = new int[3] { 100, 200, 400 };
        timesToDamage = new float[] { 2, 1.5f, 1 };
        cooldowns = new float[] { 2, 1.5f, 1.15f };
        isSpecials = new bool[] { true, true, true };
        radii = new float[] { 150, 300, 420 };
        durations = new float[] { 0, 0, 0 };
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
        Debug.Log("Attacking: " + targetEnemy);
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public void DeleteTower()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
