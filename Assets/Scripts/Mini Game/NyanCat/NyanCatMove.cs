using UnityEngine;
public class NyanCatMove : MonoBehaviour
{

    // The speed of the goose which is randomized between 1 and 6
    [SerializeField] private float speed = 5;
    // The acceleration which only applies to the gooses speed during the attack state
    [SerializeField] private float acceleration = 1.1f;
    // The randomized points on the map the goose will fly too
    private Vector3 targetRoam;
    // The point the goose will take the mouse point
    private Vector3 AttackPoint;
    // The current state the goose is set too 

    public int index;

    public Vector3[] positionArray = new Vector3[4];
    void Start()
    {
        positionArray = new Vector3[4] 
        {
           new Vector3(21f,-12.85f,1f),
           new Vector3(-28,12.85f,1),
           new Vector3(21,-6.09f,1),
           new Vector3(-28,6.09f,1), 
        };
        index = 0;
        targetRoam = positionArray[index];
    }

    // Update is called once per frame
    void Update()
    {
        Roam();
    }

    void Roam()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetRoam) <= 0.1f)
        {
            position();
        }
    }

// Helper roam functions

    public void position()
    {
        index++;
        if (index == 5)
            index = 0;
        targetRoam = positionArray[index];
    }
}

