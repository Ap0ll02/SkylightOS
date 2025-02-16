using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceShard : AbstractSpell
{
// Spell Variables

    private float currentLifetime;
    private bool isDestroyed = false;
    public void  start()
    {
        animator =  animationController.GetComponent<Animator>();
        manaCost = 1;
        spellDamage = 2;
    }

    void Update()
    {
        SpellLife();
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
    }

    public override void CastSpell(Vector2 target)
    {
        direction = (target - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public override void SpellLife()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= maxLifetime)
        {
            animator.SetTrigger("Hit");
            Destroy(gameObject);
            isDestroyed = true;
            return;
        }
    }


}
