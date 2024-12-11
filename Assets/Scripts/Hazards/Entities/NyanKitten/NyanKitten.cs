using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Febucci.UI.Examples;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class NyanKitten : Hazards
{
    // This will control the current state of our nyan kitten
    public NyanKittenState currentState;
    // The speed of the Kitten which is randomized between 1 and 6
    [SerializeField] private float speed;
    // The randomized points on the map the Kitten will fly too
    [SerializeField] private float acceleration;
    // The random point in which in the nyan kitten will travel 
    private Vector3 targetRoam;
    // The point the Nyan Kitten will travel
    private Vector3 AttackPoint;
    // The current state the Nyan Kitten is set too 
    public Vector3 offset;
    // This is so we don't keep calling the grab coroutine
    public bool hazGrabbed = false;
    // This is the variable that keeps track of generating points 
    public bool generateNewPoint = true;
    // This contains all off the audio sources for our nyan kitten 
    public AudioSource[] meowSounds = new AudioSource[2];
    public enum NyanKittenState
    {
        Roam,
        Chaos,
        Attack, 
        Grab,
        Flee
    }
    // Start is called before the first frame update
    void Start()
    {
        meowSounds[0].Play();
        // This just sets the random range 
        speed = Random.Range(2,4);
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
            case NyanKittenState.Grab:
                break;
            case NyanKittenState.Flee:
                Flee();
                break;
            default:
                break;
        }
    }
    // So our Nyan Cat will float around and go from point to point
    public void Roam()
    {
        hazGrabbed = false;
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
        Vector3 direction = (mousePos - transform.position);
        // condition ? true Result : false Result
        offset = direction.x > 0 ? new Vector3(-1.3f, -0.2f, 0) : new Vector3(1.3f, -0.2f, 0);
        Vector3 localScale = transform.localScale;
        localScale.x = ( direction.x > 0) ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
        transform.position = Vector3.MoveTowards(transform.position, mousePos + offset, Time.deltaTime * speed);
        if ((Vector3.Distance(transform.position, mousePos + offset) <= 0.2f) && hazGrabbed == false)
        {
            meowSounds[1].Play();
            hazGrabbed = true;
            StartCoroutine(GrabCursor());
        }
        // I want the cat to be able to follow the cursor. However, It should not snap to the cursor 
        // First we find the fucking direction or whatever the fuck and normalize this Mother Fucker
    }

    public void Flee()
    {
        if (generateNewPoint == true)
        {
            NewPoint(-12f,12f,-5f,5f); 
            generateNewPoint = false;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed * Time.deltaTime);
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
        // Random point we want to move to
        NewPoint(-9f, 9f, -8f, 5f);
        // We need to create a variable to keep track of time
        float elapsedTime = 0f;
        // A while loop is needed to ensure that the mouse and Nyan kitten's position is updated every frame
        while (elapsedTime < 2f)
        {
            // Move the Nyan kitten towards the target roam point
            transform.position = Vector3.MoveTowards(transform.position, targetRoam + offset, Time.deltaTime * speed);
            // Convert the Nyan kitten's position to screen coordinates
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            // This will set the cursor's position to Nyan kitten
            Mouse.current.WarpCursorPosition(screenPos);
            // Update the elapsed time
            elapsedTime += Time.deltaTime;
            // Wait for the next frame
            yield return null;
        }
        //hazGrabbed = false;
    }
}
