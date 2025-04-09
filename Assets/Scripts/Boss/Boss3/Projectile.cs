using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower tm;

    public Vector3 targetPosition;
    protected Vector3 myPosition;
    public float travelTime = 0.7f;

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

        Debug.Log("Enemy Locked: " + temp);
    }

    public void Update()
    {
        Debug.Log("Projectile Update: " );
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            Time.deltaTime * 100f
        );
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision: "+ collision.gameObject);
        if (collision.CompareTag("tdEnemy"))
        {
            tm.HitEnemy(collision.gameObject);
        }
        CleanUp();
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }

    /*if (targetPosition != null)
    {
        transform
            .DOMove(targetPosition, travelTime)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (temp != null)
                {
                    tm.HitEnemy(tm.GetTarget());
                }
                CleanUp();
            });
    } */
}
