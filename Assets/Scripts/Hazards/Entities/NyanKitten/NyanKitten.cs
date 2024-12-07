using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanKitten : Hazards
{
    // This will control the current state of our nyan kitten
    public NyanKittenState currentState;
    // The speed of the goose which is randomized between 1 and 6
    [SerializeField] private float speed = 5;
    // The acceleration which only applies to the gooses speed during the attack state
    [SerializeField] private float acceleration = 1.1f;
    // The randomized points on the map the goose will fly too
    private Vector3 targetRoam;
    // The point the goose will take the mouse point
    private Vector3 AttackPoint;
    // The current state the goose is set too 
    public enum NyanKittenState
    {
        Roam,
        Chaos,
        Attack,
        Flee
    }
    // Start is called before the first frame update
    void Start()
    {
        // We want the cat to be roaming around and just kinda being annoying nothing crazy
        currentState = NyanKittenState.Roam;
    }

    // Update is called once per frame
    void Update()
    {
        // Controls the current state of Nyan Cat and calls the relevant functions. 
        switch (currentState)
        {
            case NyanKittenState.Roam:
                Roam();
                break;
            case NyanKittenState.Chaos:
                Chaos();
                break;
            case NyanKittenState.Attack:
                Attack();
                break;
            case NyanKittenState.Flee:
                Flee();
                break;
            default:
                break;
        }
    }

    public void Roam()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetRoam) <= 0.1f)
        {
            NewPoint(-9f,9f,-8f,5f);
        }
    }

    public void Chaos()
    {

    }

    public void Attack()
    {
        // This is basically finding the mouse and turing the mouse into world coordinates
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime * speed * acceleration);
        // I want the goose to be able to follow the cursor. However, It should not snap to the cursor 
        // First we find the fucking direction or whatever the fuck and normalize this Mother Fucker
        Vector3 direction = (mousePos - transform.position);
        // Then we fucking find the God Damn desired rotation or whatever the fucking shit. Unity 2d rotation is a bit off so -90f
    }

    public void Flee()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed* Time.deltaTime );
        if (Vector3.Distance(transform.position, targetRoam) <= 0.1f)
        {
            gameObject.SetActive(false);
        }
    }

    // Sets a new roam point once the previous one is reached
    void NewPoint(float minX, float maxX,float minY, float maxY)
    {
        // Just creates a new fucking random ass position
        targetRoam = new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY), 0);
        Vector3 localScale = transform.localScale;
        Vector3 direction = (targetRoam - transform.position);
        // Depending on the X value we will flip our sprite appropriately
        localScale.x = ( direction.x > 0) ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        // We use local scale to edit the local scale... not really anything cool here 
        transform.localScale = localScale;
    }

    IEnumerator GrabCursor()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Wait for a second
        yield return new WaitForSeconds(1f);

        // Move the cursor to a new position
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Vector3 newCursorPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Cursor.SetCursor(null, newCursorPos, CursorMode.Auto);

        // Change state to Flee
        currentState = NyanKittenState.Flee;
    }
    void SetNewTarget()
    {
        // We generate a random X
        float newX= Random.Range(-9,9);
        // We also generate a random Y
        float newY = Random.Range(-8f,5f);
        // Then we set our target variable based on the new x and y components we generated. Keep 1 for the z value so our object appears
        targetRoam = new Vector3(newX, newY, 1f);
        // Flip the sprite based on the direction
        Vector3 localScale = transform.localScale;
        // Depending on the X value we will flip our sprite appropriately
        localScale.x = (newX > 0) ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        // We use local scale to edit the local scale... not really anything cool here 
        transform.localScale = localScale;
    }
}
