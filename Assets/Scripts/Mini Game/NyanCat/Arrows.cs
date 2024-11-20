using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Author Quinn Contaldi
/// This will serve as the parent class for all fucking arrows.
/// I'm not quite sure if I want to eventually turn this thing into an abstract base class.
/// Currently, arrows don't have different implementations for the same methods.
/// Thus, having just a parent class and allowing the child arrow objects to extend their behavior is good enough.
/// </summary>
public class Arrows : MonoBehaviour
{
    public float speed = 3;
    public float arrowCheckMeasument = 4.5f;
    public float outOfBounds = 8;
    public UnityEvent arrowEvent;
    
    // All child arrows will need to be able to move
    public void Move()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public bool OutOfBounds()
    {
        if(transform.position.y > outOfBounds);
        return true;
    }

    // This virtual method allows the game to default to this implementation if derived classes fail to override it.
    // Ensure to override this method in derived classes if you need to change the behavior for score checking.
    public virtual void ScoreCheck()
    {
        Debug.Log("Hey Boss we are for some reason in the parent arrow class. Something is wrong");
    }

    public void Destroy()
    {
        // We poof the game object
        Destroy(gameObject);
        // We unsubscribe from the proper event 
        arrowEvent.RemoveListener(this.ScoreCheck);
    }

}




