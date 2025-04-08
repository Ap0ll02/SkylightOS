using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower tm;

    public Vector3 targetPosition;
    protected Vector3 myPosition;
    public float travelTime = 0.1f;

    public void Start()
    {
        tm = GetComponentInParent<Tower>();

        // Reference To Target Enemy
        GameObject temp = tm.GetTarget();
        targetPosition = temp.GetComponent<Transform>().position;
        myPosition = GetComponent<Transform>().position;
        Debug.Log("Enemy Locked: " + temp);
        if (targetPosition != null)
        {
            transform
                .DOMove(targetPosition, travelTime)
                .SetEase(Ease.InFlash)
                .OnComplete(() =>
                {
                    tm.HitEnemy(tm.GetTarget());
                });
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided at all");
        if (collision.gameObject.CompareTag("tdEnemy"))
        {
            tm.HitEnemy(collision.gameObject);
        }
        CleanUp();
    }

    public float velocity;

    public void CleanUp()
    {
        Destroy(gameObject);
    }
}
