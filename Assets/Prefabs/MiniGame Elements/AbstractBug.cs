using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractBug : MonoBehaviour
{
    public int hearts;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hearts -= damage;
    }

    public void TrackPlayer()
    {

    }

    public void Move()
    {
        //Vector2Distance(this.transform.position, )
    }

    public void MoveUp()
    {

    }

    public void MoveDown()
    {

    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    public void HandleCollision(Collision2D collision)
    {
        var CatGirl = collision.gameObject.GetComponent<CatGirlWizard>();
        if (CatGirl != null)
        {
            CatGirl.TakeDamage(damage);
        }
    }
}
