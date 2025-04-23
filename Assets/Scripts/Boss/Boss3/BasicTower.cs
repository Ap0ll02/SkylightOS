using System.Collections;
using UnityEngine;

public class BasicTower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    public Coroutine ActivateTower;
    public Animator anim;

    public new void Start()
    {
        base.Start();
        speed = 70;
        ActivateTower = StartCoroutine(ActiveTower());

        // ========= Tower Upgrade Lists =========
        damages = new int[3] { 35, 60, 100 };
        timesToDamage = new float[] { 1, 1, 1 };
        cooldowns = new float[] { 2, 1, 0.75f };
        isSpecials = new bool[] { false, false, true };
        radii = new float[] { 100, 120, 150 };
        durations = new float[] { 0, 0, 0 };
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
        anim.SetTrigger("Fire");
        Debug.Log("Attacking: " + targetEnemy);
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public void DeleteTower()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
