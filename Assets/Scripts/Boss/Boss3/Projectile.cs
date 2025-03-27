using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
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
        Destroy(this.gameObject);
    }

    IEnumerator Shoot()
    {
        yield return null;
    }
}
