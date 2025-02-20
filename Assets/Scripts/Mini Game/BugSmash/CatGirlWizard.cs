using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CatGirlWizard : MonoBehaviour
{
    public GameObject iceBallPrefab; // Reference to the Ice Ball prefab
    public GameObject BugSmashWindow;
    public BasicWindow window;

    public float moveSpeed = 4f;

    private Animator animator;
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
        if(hearts <= 0 )
            isDead = true;
    }

    public void Heal(int heal)
    {
        hearts += heal;
    }

    public void CastSpell()
    {
        if (isSpellOnCooldown)
            return;
        isSpellOnCooldown = true;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        //GameObject iceBall = Instantiate(iceBallPrefab, transform.position, Quaternion.identity);
        Vector3 dir = (worldPosition - transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 180;
        GameObject iceBall = Instantiate(iceBallPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward),BugSmashWindow.transform);

        iceBall.GetComponent<IceShard>().direction = dir;
        iceBall.GetComponent<IceShard>().transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        StartCoroutine(SpellCoolDown(spellCooldown));
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



    public void OnShootPerformed(InputAction.CallbackContext context)
    {
        CastSpell();
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
        move.action.Enable();
        shoot.action.Enable();
        StartCoroutine(ControlCatGirl());
        shoot.action.performed += OnShootPerformed;
    }

    // This unsubscribes form the events so we can keep our code nice and clean
    public void OnDisable()
    {
        // Unsubscribe from the movement events and disables input
        move.action.Disable();
        shoot.action.Disable();
        shoot.action.performed -= OnShootPerformed;
    }

    public void ResetCatgirl()
    {
        hearts = maxHearts;
        isDead = false;
        Mana = 100;
        isSpellOnCooldown = false;
    }

    IEnumerator SpellCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        isSpellOnCooldown = false;
    }

}
