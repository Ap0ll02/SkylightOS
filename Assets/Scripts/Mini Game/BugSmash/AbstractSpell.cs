using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractSpell : MonoBehaviour
{
    // Start is called before the first frame update  
    public Vector3 direction;

    public int manaCost;
    public int spellDamage;

    public float speed;
    public float maxLifeTime;

    public bool isDestroyed;

    public Animator animator;
    public AnimationClip travelAnimation;
    public AnimationClip ExplosionAnimation;

    // This is the function that is called when the Bug tag is collided with a spell. This means that this function is only called  
    // when the spell collides with a game object that has the Bug tag.  
    // This is what happens to object B, the object that is being collided into.  
    public abstract void CastSpell(Vector3 direction);
    public abstract void SpellLife();

    private void SpellHit(Collision2D collision)
    {
        var bug = collision.gameObject.GetComponent<AbstractBug>();
        bug.TakeDamage(spellDamage);
        explosionAnimation();
    }
    // This event is reffering to what happens when this object collides into object B. If object B is a Bug we do damage!  
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bug"))
        {
            SpellHit(collision);
        }
    }

    public void TravelAnimation()
    {
        animator.SetTrigger("Travel");
    }

    public void explosionAnimation()
    {
        animator.SetTrigger("Hit");
    }
}
