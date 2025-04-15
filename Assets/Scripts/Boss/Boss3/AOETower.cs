using System.Collections;
using UnityEngine;

public class AOETower : Tower
{
    public GameObject projectilePrefab;
    public GameObject projectile;
    public Coroutine ActivateTower;
    public int attackRadius;
    public Animator animator;

    public new void Start()
    {
        base.Start();
        ActivateTower = StartCoroutine(ActiveTower());

        // ========= Tower Upgrade Lists =========
        damages = new int[3] { 280, 500, 950 };
        timesToDamage = new float[] { 4, 3, 2 };
        cooldowns = new float[] { 4, 3.25f, 2f };
        isSpecials = new bool[] { true, true, true };
        radii = new float[] { 32, 37, 42 };
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
        animator.Play("AOETowerBeam");
        Debug.Log("Attack");
        projectile = Instantiate(projectilePrefab, parent: GetComponent<Transform>());
    }

    public void DeleteTower()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
