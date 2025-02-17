using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class IceShard : MonoBehaviour
{
    public float currentLifeTime;
    public float maxLifeTime;
    public float speed;
    public Vector3 direction;
    public bool isDestroyed;
    public int manaCost;
    public int spellDamage;
    public AnimatorController animationController;
    private Animator animator;
    //public Image image;
    //public Sprite spriteArray;


    public void  Start()
    {
        //animator =  animationController.GetComponent<Animator>();
        manaCost = 1;
        spellDamage = 1;
        speed = 7;
        maxLifeTime = 4;
        currentLifeTime = 0;
        isDestroyed = false;
    }


// Move the projectile every frame
    public void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime; // Move toward the target
        }
        // Handle the ice shard’s lifetime
        SpellLife();
    }

    public void SpellLife()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= maxLifeTime)
        {
            if (animator) animator.SetTrigger("Hit"); // Trigger animator if available
            Destroy(gameObject); // Destroy the ice shard
            isDestroyed = true;
        }
    }

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

    public void explosionAnimation()
    {
        animator.SetTrigger("Hit");
    }


}
