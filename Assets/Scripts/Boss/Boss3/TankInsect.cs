using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class TankInsect : AbstractEnemy
{
    public int damageNegatedMax;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = Random.Range(60f,70f);
        maxHealth = 400;
        currentHealth = maxHealth;
        pointValue = 150;
        damage = 1;
        damageNegatedMax = 400;
        reward = 10;
        pointValue = 4;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void TakeDamage(int damage, float time = 0, float precent = 0)
    {
        // If the bug still has armor to negate damage then we half the damage rounding up
        if (damageNegatedMax > 0)
        {
            base.TakeDamage((int)Math.Ceiling(damage * 0.5f),time,precent);
            damageNegatedMax -= damage;
        }
        // Once the max negation damage is reached it takes damage normally
        else
        {
            base.TakeDamage(damage,time,precent);
        }
    }

    // public void OnDestroy()
    // {
    //     animator.SetBool("Moving", false);      // Stop movement animation
    //     animator.SetBool("Death", true);        // Trigger death animation
    //
    //     StartCoroutine(PlayDeathThenDestroy());
    // }
    //
    // private IEnumerator PlayDeathThenDestroy()
    // {
    //     AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0); // Get current animation state
    //     yield return new WaitForSeconds(state.length); // Wait for current animation clip to finish
    //     Destroy(gameObject);                          // Destroy after animation finishes
    // }

}
