using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlpinePlayer : MonoBehaviour
{
    [Header("Animation and Audio")]
    private Animator animator;
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
    // private void Update()
    // {
    //     MoveDirection();
    // }

    #region Input
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference shoot;

// These are the listeners the observers! that subscribe or observe the input action reference. So in short when a button is pressed this function will be called
    public void OnShootPerformed(InputAction.CallbackContext context)
    {
        //CastSpell();
    }
    public void OnJumpPerformed(InputAction.CallbackContext context)
    {
        catGirlBody.velocity = new Vector2(catGirlBody.velocity.x, jumpSpeed);
        if (catGirlBody.velocity.y < 0)
        {
            //Higher gravity when falling
            catGirlBody.gravityScale = catGirlBody.gravityScale * 1.5f;
        }
    }
    #endregion
    #region Gameplay
    // Controls the movement of our catgirl wizard and controls the animation controller this is also handled through the input controller
    public void MoveDirection()
    {
        catGirlBody.velocity = new Vector2(move.action.ReadValue<Vector2>().x * moveSpeed, catGirlBody.velocity.y);
        if (move.action.ReadValue<Vector2>().x > 0)
        {
            // animator.SetTrigger("Right");
            // animator.ResetTrigger("Left");
        }
        else if (move.action.ReadValue<Vector2>().x < 0)
        {
            // animator.SetTrigger("Left");
            // animator.ResetTrigger("Right");
        }
        else
        {
            // animator.ResetTrigger("Right");
            // animator.ResetTrigger("Left");
        }
    }

    public IEnumerator PlayingGame()
    {
        while (!isDead) // Keep looping unless the game ends.
        {
            MoveDirection();
            yield return null; // Wait until the next frame.
        }
    }

    #endregion Gameplay
    #region Start and End
    private void OnEnable()
    {
        //subscribes to our events
        jump.action.performed += OnJumpPerformed;
        shoot.action.performed += OnShootPerformed;
        StartCoroutine(PlayingGame());
    }

    public void OnDisable()
    {
        // Unsubscribe from the movement events and disables input
        jump.action.performed -= OnJumpPerformed;
        shoot.action.performed -= OnShootPerformed;
    }
    #endregion
    #region PlayerFunctions
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
    #endregion
}
