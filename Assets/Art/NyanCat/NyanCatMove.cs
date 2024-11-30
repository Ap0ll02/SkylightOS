using UnityEngine;
public class NyanCatMove : MonoBehaviour
{

    // The speed of the goose which is randomized between 1 and 6
    [SerializeField] private float speed = 5;
    // The acceleration which only applies to the gooses speed during the attack state
    [SerializeField] private float acceleration = 1.1f;
    // How fast the goose will rotate towards a given angle 
    // The Popup the goose will drag in 
    [SerializeField] private GameObject PopUp;
    // The randomized points on the map the goose will fly too
    private Vector3 targetRoam;
    // The point the goose will take the mouse point
    private Vector3 AttackPoint;
    // The current state the goose is set too 
    public bool isFlying;

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
        if (isFlying)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetRoam, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetRoam) <= 0.1f)
            {
                NewPoint(-13.0f, 15.0f, -11.0f, 11.0f);
            }
        }
    }

// Helper roam functions
    void NewPoint(float xLeft, float xRight, float minY, float maxY)
    {
        if (transform.position.x >= xRight)
        {
            FlipSprite();
            targetRoam = new Vector3(xLeft, Random.Range(minY, maxY), -1);
        }
        else if (transform.position.x <= xLeft)
        {
            FlipSprite();
            targetRoam = new Vector3(xRight, Random.Range(minY, maxY), -1);
        }
    }

    void FlipSprite()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}

