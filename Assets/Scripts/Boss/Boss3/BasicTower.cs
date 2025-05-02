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
        speed = 200;
        ActivateTower = StartCoroutine(ActiveTower());
        // ========= Tower Upgrade Lists =========
        costToUpgrade = new int[3] { 100, 200, 500 };
        damages = new int[3] { 40, 65, 100 };
        timesToDamage = new float[] { 1, 1, 1 };
        cooldowns = new float[] { 2, 1.5f, 1f };
        isSpecials = new bool[] { false, false, true };
        radii = new float[] { 125, 150, 200 };
        durations = new float[] { 0, 0, 0 };
        // =======================================
        base.Start();
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
