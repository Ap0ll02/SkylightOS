using System.Collections;
using UnityEngine;

public class AOETower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    public Coroutine ActivateTower;
    public Animator animator;

    public new void Start()
    {
        ActivateTower = StartCoroutine(ActiveTower());

        // ========= Tower Upgrade Lists =========
        damages = new int[3] { 150, 200, 250 };
        costToUpgrade = new int[3] { 500, 1000, 2000};
        timesToDamage = new float[] { 4, 3, 2 };
        cooldowns = new float[] { 8, 7f, 5f };
        isSpecials = new bool[] { true, true, true };
        radii = new float[] { 300, 385, 500 };
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
        animator.SetTrigger("Fire");
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public void DeleteTower()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
