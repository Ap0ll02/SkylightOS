using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlpinePlayer : MonoBehaviour
{
    private Animator animator;
    // Used event based programing to envoke specific functions for moving and shooting
    public InputActionReference move;
    // We want our catgirl to have a body we can reference
    public Rigidbody2D catGirlBody;
    //The sounds our catgirl will make when doing stuff
    public AudioClip[] catGirlSounds;

    // Max health variable
    public int maxHearts = 10;
    // This is the current amount of health our catgirl has
    public int hearts;
    // OUR CATGIRL HAS MANA!
    public int Mana = 100;
    // Is the catgirl dead or alive
    public bool isDead;
    // The speed of our catgirl
    public float moveSpeed = 10;
    public float jumpSpeed = 5;

    public Vector2 moveDirection;
    // Start is called before the first frame update

    private void Update()
    {
        MoveDirection();
    }

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
        //CastSpell();
    }

    // Controls the movement of our catgirl wizard and controls the animation controller this is also handled through the input controller
    public void MoveDirection()
    {

        moveDirection = move.action.ReadValue<Vector2>();
        catGirlBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * jumpSpeed * Time.deltaTime);
        if (move.action.ReadValue<Vector2>().y > 0)
        {
            // animator.SetTrigger("Jump");
            // animator.ResetTrigger("Idle");
            // animator.ResetTrigger("Right");
            // animator.ResetTrigger("Left");
        }
        else if (move.action.ReadValue<Vector2>().x > 0)
        {
            // animator.SetTrigger("Right");
            // animator.ResetTrigger("Jump");
            // animator.ResetTrigger("Idle");
            // animator.ResetTrigger("Left");
        }
        else if (move.action.ReadValue<Vector2>().x < 0)
        {
            // animator.SetTrigger("Left");
            // animator.ResetTrigger("Right");
        }
        else
        {
            // animator.SetTrigger("Idle");
            // animator.ResetTrigger("Jump");
            // animator.ResetTrigger("Right");
            // animator.ResetTrigger("Left");
        }
    }

    private void OnEnable()
    {
        move.action.Enable();
        StartCoroutine(ControlCatGirl());
    }

    // This unsubscribes form the events so we can keep our code nice and clean
    public void OnDisable()
    {
        // Unsubscribe from the movement events and disables input
        move.action.Disable();
    }
    public void TakeDamage(int damage)
    {
        hearts -= damage;
        if (hearts <= 0)
        {
            isDead = true;
        }
    }

    public void Heal(int heal)
    {
        hearts += heal;
    }
}
