using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower tm;

    public void Start()
    {
        Coroutine shoot = StartCoroutine(Shoot());
        tm = GetComponentInParent<Tower>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tdEnemy"))
        {
            tm.HitEnemy(collision.gameObject);
        }
        CleanUp();
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }

    public IEnumerator Shoot()
    {
        yield return null;
    }
}
