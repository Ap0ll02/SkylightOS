using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractBug : MonoBehaviour
{
    public CatGirlWizard catGirl;
    public BasicWindow window;

    public int hearts;
    public int damage;
    public int score;

    public bool CoolDown;
    public bool Isdone;

    public float moveSpeed;
    public Vector2 distance;

    public SpriteRenderer spriteRenderer;
    private Animator animator;

    public BugSmashGameScoreManager scoreManager;


    public Sprite[] sprites;
    public int spriteIndex;


    // Start is called before the first frame update

    public void Start()
    {
        if (catGirl == null)
        {
            catGirl = GameObject.FindGameObjectWithTag("CatGirlWizardPlayer").GetComponent<CatGirlWizard>();
        }

        if (scoreManager == null)
        {
            scoreManager = GameObject.Find("BugSmashScoreManager").GetComponent<BugSmashGameScoreManager>();
        }

        if (catGirl == null)
        {
            Debug.Log("We are missing CatGirlWizard");
        }

        if (scoreManager == null)
        {
            Debug.Log("We are missing ScoreManager");
        }
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("We are missing Animator");
        }
    }
    public void TakeDamage(int damage)
    {
        hearts -= damage;
        // In order to kill the bug
        if (hearts <= 0)
        {
            scoreManager.AddScore(score);
            Destroy(this.gameObject);
        }
    }

    // Simply is a method used to track the player
    public void TrackPlayer()
    {
        //animator.SetTrigger("Move");
        Vector2 currentPosition = this.transform.position;
        Vector2 playerPosition = catGirl.transform.position;
        this.transform.position = Vector2.MoveTowards(
            currentPosition,
            playerPosition,
            moveSpeed * Time.deltaTime);
        // We have to use this in order to control the animation triggers
        distance = playerPosition - currentPosition;
        // We use this in order to look at the player it works... dont fuck with it
        Vector3 dir = catGirl.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "CatGirlWizardPlayer")
        {
            HandleCollision(collision);
        }
    }

    public void HandleCollision(Collision2D collision)
    {
        var CatGirl = collision.gameObject.GetComponent<CatGirlWizard>();
        if (CatGirl != null && CoolDown == false)
        {
            CatGirl.TakeDamage(damage);
        }
    }

    // Will play the bug death animation when it is destroyed
    private void OnDestroy()
    {
        scoreManager.AddScore(score);
        Debug.Log("Bug Destroyed");
        //animator.SetTrigger("Death");
    }

    private IEnumerator AttackCooldown()
    {
        // Set cooldown flag to true
        CoolDown = true;
        //Wait for a second for the attack
        yield return new WaitForSeconds(1f);
        // Reset cooldown flag
        CoolDown = false;
    }

    public void GameOverCheck()
    {
        if (scoreManager.gameOver)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayTravellingAnimation()
    {
        StartCoroutine(PlayAnimation);
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        if()
    }

}
