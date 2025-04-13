using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower tm;

    public Vector3 targetPosition;
    protected Vector3 myPosition;
    public float speed;
    public bool seeStealth;

    public void Start()
    {
        if (tm == null)
        {
            tm = GetComponentInParent<Tower>();
        }

        // Reference To Target Enemy
        GameObject temp = tm.GetTarget();
        targetPosition = temp.transform.position;
        myPosition = transform.position;
        seeStealth = tm.isSpecial;
        Debug.Log("Enemy Locked: " + temp);
    }

    public void Update()
    {
        Debug.Log("Projectile Update: ");
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            Time.deltaTime * speed
        );
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision: " + collision.gameObject);
        if (
            collision.CompareTag("tdEnemy")
            || (seeStealth && collision.CompareTag("StealthEnemyTD"))
        )
        {
            tm.HitEnemy(collision.gameObject);
        }
        CleanUp();
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }
}
