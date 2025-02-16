using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CatGirlWizard : MonoBehaviour
{
    public GameObject iceBallPrefab; // Reference to the Ice Ball prefab
    public BasicWindow window;
    public float moveSpeed = 5f;

    private Animator animator;
    public InputActionReference move;
    public InputActionReference shoot;

    public Rigidbody2D catGirlBody;
    public int Hearts = 3;
    public int Mana = 10;

    public Vector2 moveDirection;
    public bool isDead = false;
    public bool playGame = false;

    private void Start()
    {
        // Reference the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        Hearts -= damage;
    }

    public void Heal(int heal)
    {
        Hearts += heal;
    }

    public void CastSpell()
    {

    }


    public IEnumerator ControlCatGirl()
    {
        while (!isDead)
        {
            moveDirection = move.action.ReadValue<Vector2>();
            MoveDirection();
            //Shoot();
            yield return null;
        }
    }



    private void OnShootPerformed(InputAction.CallbackContext context)

    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        mousePos.z = 0; // Ensure the Z-coordinate matches your game's plane.
        GameObject newIceBall = Instantiate(iceBallPrefab, transform.position, Quaternion.identity);
        var iceball = newIceBall.GetComponent<AbstractSpell>();
        iceball.CastSpell(mousePos);
    }

    public void MoveDirection()
    {
        catGirlBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

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
    }

    private void OnEnable()
    {
        StartCoroutine(ControlCatGirl());
        move.action.Enable();
    }

    // This unsubscribes form the events so we can keep our code nice and clean
    public void OnDisable()
    {
        // Unsubscribe from the movement events and disables input
        move.action.Disable();
    }


}
