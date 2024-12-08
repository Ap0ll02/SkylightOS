using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Runtime.InteropServices;
using static UnityEngine.GraphicsBuffer;



public class GooseBehavior : MonoBehaviour
{

    // The speed of the goose which is randomized between 1 and 6
    [SerializeField] private float speed = 5;
    // The acceleration which only applies to the gooses speed during the attack state
    [SerializeField] private float acceleration = 1.1f;
    // How fast the goose will rotate towards a given angle 
    [SerializeField] private float rotation = 100f;
    // The Popup the goose will drag in 
    [SerializeField] private GameObject PopUp;
    // The randomized points on the map the goose will fly too
    private Vector3 targetRoam;
    // The point the goose will take the mouse point
    private Vector3 AttackPoint;
    // The current state the goose is set too 
    private GooseState currentState;
    

    public enum GooseState
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
        currentState = GooseState.Attack;
        speed = Random.Range(1.0f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GooseState.Roam:
                Roam();
                break;
            case GooseState.Chaos:
                Chaos();
                break;
            case GooseState.Attack:
                Attack();
                break;
            case GooseState.Grab:
                break;
            case GooseState.Flee:
                Flee();
                break;
        }
    }

    void Roam()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed* Time.deltaTime );
        if (Vector3.Distance(transform.position, targetRoam) <= 0.1f)
        {
            NewPoint(-8.0f,8.0f,-11.0f, 11.0f );
        }
    }
    void Chaos()
    {

    }

    void Attack()
    {
        // This is basically finding the mouse and turing the mouse into world cordinantes 
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime * speed * acceleration);
        // I want the goose to be able to follow the cursor. However, It should not snap to the cursor 
        // First we find the fucking direction or whatever the fuck and normalize this mother fucker
        Vector3 direction = (mousePos - transform.position);
        // Then we fucking find the god damn desired rotation or whatever the fucking shit. Unity 2d rotation is a bit off so -90f
        float angle = Mathf.Clamp(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, -45, 45);
        var targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        // Now just rotate towards our target at a given rate using rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotation * Time.deltaTime);

        // Next step check if we reached the mouse position
        if (Vector3.Distance(transform.position, mousePos) <= 0.1f)
        {
            Debug.Log("Iv got your cursor");
            currentState = GooseState.Flee;
            NewPoint(-10f,-12,10,12);
        }

    }
    void Flee()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed* Time.deltaTime );
        Vector3 direction = (targetRoam - transform.position);
        float angle = Mathf.Clamp(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, -45, 45);
        var targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotation * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetRoam) <= 0.1f)
        {
            gameObject.SetActive(false);
        }

    }

    // Helper roam functions
    void NewPoint(float minX, float maxX,float minY, float maxY)
    {
        // Just creates a new fucking random ass position
        targetRoam = new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY), 0);
    }

}

