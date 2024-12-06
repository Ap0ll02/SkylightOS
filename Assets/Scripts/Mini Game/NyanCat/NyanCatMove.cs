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

    void Start()
    {
        SetNewTarget();
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
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        float newX;
        float newY = Random.Range(-8f, 5f);
        if (transform.position.x > 0)
            newX = -13.0f;
        else
        {
            newX = 13.0f;
        }
        targetRoam = new Vector3(newX, newY, 1f);
        // Flip the sprite based on the direction
        Vector3 localScale = transform.localScale;
        localScale.x = (newX < 0) ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
}