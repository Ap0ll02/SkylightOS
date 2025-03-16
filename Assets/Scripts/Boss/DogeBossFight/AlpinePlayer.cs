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

    [Header("Varibles")]
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
    private bool isGrounded;

    [Header("Camera")]
    public Transform catGirlCamera;
    public float smoothSpeed = 0.5f;
    public Vector2 moveDirection;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    private void Update()
    {
        MoveDirection();
        catGirlCamera.position = Vector3.SmoothDamp(catGirlCamera.position, new Vector3(transform.position.x, transform.position.y, 0), ref velocity, 0.1f);

    }

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
        animator.SetBool("isJumping", false);
    }
    #endregion
    #region Gameplay
    // Controls the movement of our catgirl wizard and controls the animation controller this is also handled through the input controller
    public void MoveDirection()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Debug.Log(move.action.ReadValue<Vector2>().x * moveSpeed);
            // Debug.Log(catGirlBody.velocity);
            catGirlBody.velocity = new Vector2(move.action.ReadValue<Vector2>().x * moveSpeed, catGirlBody.velocity.y);
            if (catGirlBody.velocity.x > 0.2f)
            {
                animator.SetFloat("VelocityX", catGirlBody.velocity.x);
                animator.SetBool("isRunning", true);
            }
            else if (catGirlBody.velocity.x < -0.2f)
            {
                animator.SetFloat("VelocityX", catGirlBody.velocity.x);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
            if (Math.Abs(catGirlBody.velocity.y) < 0.2f)
            {
                animator.SetBool("isJumping", false);
            }
        }
        else
        {
            Debug.Log("Animator is null");
        }
        //move.action.ReadValue<Vector2>().x * moveSpeed
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
        //StartCoroutine(PlayingGame());
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
