using UnityEngine;

public class NyanCatMove : MonoBehaviour
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
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Roam();
    }

    void Roam()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed* Time.deltaTime );
        if (Vector3.Distance(transform.position, targetRoam) <= 0.1f)
        {
            NewPoint(-8.0f,8.0f,-11.0f, 11.0f );
        }
    }

    // Helper roam functions
    void NewPoint(float minX, float maxX,float minY, float maxY)
    {
        // Just creates a new Point
        targetRoam = new Vector3(Random.Range(minX, maxX),Random.Range(minY, maxY), -1);
    }

}

