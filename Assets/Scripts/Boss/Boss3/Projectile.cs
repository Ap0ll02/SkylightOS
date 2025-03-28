using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void Start()
    {
        Coroutine shoot = StartCoroutine(Shoot());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tdEnemy"))
        {
            AttackEnemy();
        }
    }

    public void AttackEnemy() { }

    public void CleanUp()
    {
        Destroy(gameObject);
    }

    public IEnumerator Shoot()
    {
        yield return null;
    }
}
