using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class ArmoredInsect : AbstractEnemy
{
    public int immunity = 2;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        GetNewWaypoint();
        speed = Random.Range(60f, 70f);
        maxHealth = 5;
        currentHealth = maxHealth;
        pointValue = 100;
        damage = 2;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (Vector3.Distance(this.transform.position, nextWaypoint) < 0.001f)
        {
            GetNewWaypoint();
        }
        else
        {
            if(animator.GetBool("Moving") == false)
                animator.SetBool("Moving", true);
            // Move the object towards the waypoint as before
            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);
        }
    }

    public override void Death()
    {
        animator.SetBool("Moving", false);
        animator.SetBool("Dead", true);
        Destroy(this.gameObject);
    }
    public override void TakeDamage(int damage, float time = 0, float precent = 0)
    {
        if (immunity > 0)
        {
            // I should add some sort of shield and sound effect
            immunity--;
        }
        else
        {
            // We have the death check in our damage method so we will just reuse it and call the base damage function
            base.TakeDamage(damage, time, precent);
        }
    }
}
