using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower tm;

    public Vector3 targetPosition;
    protected Vector3 myPosition;
    public float travelTime = 0.3f;

    public void Start()
    {
        targetPosition = GetComponentInParent<Transform>().position;
        tm = GetComponentInParent<Tower>();
        myPosition = GetComponent<Transform>().position;
        if (targetPosition != null)
        {
            transform.DOMove(targetPosition, travelTime).SetEase(Ease.InFlash);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
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
