using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CatGirlWizard : MonoBehaviour
{
    // Our iceBallPrefab that our catgirl wizard will be shooting
    public GameObject iceBallPrefab;
    // This is the window that the game is playing on and is important for bounding
    public GameObject BugSmashWindow;
    // This is the catgirl death sound
    public GameObject CatgirlDeath;
    public Rect windowBounds;
    public BasicWindow window;

    public float moveSpeed = 4f;

    private Animator animator;
    // Used event based programing to envoke specific functions for moving and shooting
    public InputActionReference move;
    public InputActionReference shoot;
    public Rigidbody2D catGirlBody;


    public int maxHearts = 10;
    public int hearts;
    public int Mana = 100;

    public Vector2 moveDirection;
    public bool isDead;
    public bool isSpellOnCooldown = false;
    public float spellCooldown = 0.25f;

    // For when she cast a spell!
    public AudioClip[] catGirlSounds;
    public AudioClip WeLost;

    private void Start()
    {

        // Reference the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
        isDead = false;
        hearts = maxHearts;
    }

    public void TakeDamage(int damage)
    {
        hearts -= damage;
        if (hearts <= 0)
        {
            isDead = true;
            Instantiate(CatgirlDeath, transform.position, Quaternion.identity);
        }
    }

    public void Heal(int heal)
    {
        hearts += heal;
    }

    public void CastSpell()
    {
        // Check if the spell is on cooldown
        if (isSpellOnCooldown)
            return;
        // Once the spell is cast we set the bool to true in order to signifiy the cool down is needed
        isSpellOnCooldown = true;
        // Mouse position stuff dont really fuck with this please
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        //GameObject iceBall = Instantiate(iceBallPrefab, transform.position, Quaternion.identity);
        Vector3 dir = (worldPosition - transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 180;
        GameObject iceBall = Instantiate(iceBallPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward),BugSmashWindow.transform);

        // This is how we set the appropriate variables for the ice shard. The Ice shard will handle its travel and collision. This is a one time thing per iceshard
        iceBall.GetComponent<IceShard>().direction = dir;
        iceBall.GetComponent<IceShard>().transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // we pick a random audio source to play.
        //this.gameObject.GetComponent<AudioSource>().clip = catGirlSounds[UnityEngine.Random.Range(0,catGirlSounds.Length)];
        //this.gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(SpellCoolDown(spellCooldown));
        // This is what actually starts the cool down Coroutine
    }

// This function works similarly to the update loop but is only activated when the move callback occurs invoking the ControlCatGirl function
    public IEnumerator ControlCatGirl()
    {
        while (!isDead)
        {
            moveDirection = move.action.ReadValue<Vector2>();
            MoveDirection();
            yield return null;
        }
    }

    // Similar to the ControlCatGirl function it is invoked when the shoot call back is invoked AKA left clicking. Check the input manager for this logic.
    public void OnShootPerformed(InputAction.CallbackContext context)
    {
        CastSpell();
    }

    // Controls the movement of our catgirl wizard and controls the animation controller this is also handled through the input controller
    public void MoveDirection()
    {
        catGirlBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        windowBounds = BugSmashWindow.GetComponent<RectTransform>().rect;
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        if (move.action.ReadValue<Vector2>().y > 0)
        {
            animator.SetTrigger("Up");
            animator.ResetTrigger("Down");
            animator.ResetTrigger("Right");
            animator.ResetTrigger("Left");
        }
        if (move.action.ReadValue<Vector2>().y < 0)
        {
            animator.SetTrigger("Down");
            animator.ResetTrigger("Up");
            animator.ResetTrigger("Right");
            animator.ResetTrigger("Left");
        }
        if (move.action.ReadValue<Vector2>().x > 0)
        {
            animator.SetTrigger("Right");
            animator.ResetTrigger("Down");
            animator.ResetTrigger("Up");
            animator.ResetTrigger("Left");
        }
        if (move.action.ReadValue<Vector2>().x < 0)
        {
            animator.SetTrigger("Left");
            animator.ResetTrigger("Down");
            animator.ResetTrigger("Right");
            animator.ResetTrigger("Up");
        }
        // Bound our catgirl to the window so don't really mess with it unless you want to change her bounding
        if(rectTransform.anchoredPosition.x < windowBounds.xMin)
            rectTransform.anchoredPosition = new Vector2(windowBounds.xMin, rectTransform.anchoredPosition.y);
        if(rectTransform.anchoredPosition.x > windowBounds.xMax)
            rectTransform.anchoredPosition = new Vector2(windowBounds.xMax, rectTransform.anchoredPosition.y);
        if(rectTransform.anchoredPosition.y < windowBounds.yMin)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, windowBounds.yMin);
        if(rectTransform.anchoredPosition.y > windowBounds.yMax)
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, windowBounds.yMax);
    }

    // When the object is enabled
    private void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();
        StartCoroutine(ControlCatGirl());
        // How we subscribe to the input event using the On Shoot Performed
        shoot.action.performed += OnShootPerformed;
    }

    // This unsubscribes form the events so we can keep our code nice and clean
    public void OnDisable()
    {
        // Unsubscribe from the movement events and disables input
        move.action.Disable();
        shoot.action.Disable();
        // we unsubscribe to the event lets be clean coders... alright
        shoot.action.performed -= OnShootPerformed;
    }

    // So we can play again
    public void ResetCatgirl()
    {
        hearts = maxHearts;
        isDead = false;
        Mana = 100;
        isSpellOnCooldown = false;
    }

    // So we dont get that catgirl magic spam
    IEnumerator SpellCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        isSpellOnCooldown = false;
    }

}
