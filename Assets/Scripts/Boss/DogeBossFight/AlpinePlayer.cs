using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AlpinePlayer : MonoBehaviour
{
    [Header("Animation and Audio")]
    private Animator animator;
    // We want our catgirl to have a body we can reference
    public Rigidbody2D catGirlBody;
    //The sounds our catgirl will make when doing stuff

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
    public float moveSpeed = 4;
    public float jumpSpeed = 5.4f;
    public bool isGrounded;

    [Header("Camera")]
    public Transform catGirlCamera;
    public float smoothSpeed = 0.5f;
    public Vector2 moveDirection;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update

    [Header("Ground Check")]
    public Transform groundCheckPosition;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public AudioClip[] CatGirlJumpSounds;      // Reference to the sound to play when the lever is turned on
    private AudioSource audioSource;


    public void FixedUpdate()
    {
        catGirlCamera.position = Vector3.SmoothDamp(catGirlCamera.position, new Vector3(transform.position.x, transform.position.y, 0), ref velocity, 0.25f);
    }

    public void CheckGroundStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);
    }

    // These are the listeners the observers! that subscribe or observe the input action reference. So in short when a button is pressed this function will be called

    #region Gameplay
    // Controls the movement of our catgirl wizard and controls the animation controller this is also handled through the input controller
    public void MoveDirection()
    {
        float xInput = 0f;
        var animator = GetComponent<Animator>();
        // Get movement input based on keys pressed
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            xInput = -1f; // Move left
            animator.SetBool("Left", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Right", false);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            xInput = 1f; // Move right
            animator.SetBool("Right", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Left", false);
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                catGirlBody.velocity = new Vector2(catGirlBody.velocity.x, jumpSpeed);
            }
        }
        Vector2 newVelocity = new Vector2(xInput * moveSpeed, catGirlBody.velocity.y);
        if (Math.Abs(newVelocity.x) < 0.01f && Math.Abs(newVelocity.y) < 0.01f)
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
        }
        catGirlBody.velocity = newVelocity; // Set the character’s velocity
    }

    public IEnumerator PlayingGame()
    {
        while (!isDead) // Keep looping unless the game ends.
        {
            MoveDirection();
            CheckGroundStatus();
            yield return null; // Wait until the next frame.
        }
    }

    #endregion Gameplay
    #region Start and End
    private void OnEnable()
    {
        StartCoroutine(PlayingGame());
    }

    public void OnDisable()
    {
        // Unsubscribe from the movement events and disables input
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
