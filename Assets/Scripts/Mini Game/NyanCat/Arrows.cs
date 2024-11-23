using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static NyanceNyanceRevolution;

/// <summary>
/// Author Quinn Contaldi
/// This will serve as the parent class for all fucking arrows.
/// I'm not quite sure if I want to eventually turn this thing into an abstract base class.
/// Currently, arrows don't have different implementations for the same methods.
/// Thus, having just a parent class and allowing the child arrow objects to extend their behavior is good enough.
/// </summary>
public class Arrows : MonoBehaviour
{
    //current speed of the Arrows 
    public float speed = 1;
    // This is where our current arrow check is located at 
    public float arrowCheckMeasument = 4.5f;
    // Simply checks if we are out of bounds
    public float outOfBounds = 8;
    // our arrows need to have a score for them to return 
    public int score = 0;
    // Holds the arrow events we want to unsubscribe too
    public UnityEvent ArrowEvent;
    // Holds the NyanceNyanceReveloution singelton class so we can update score and do other sick shit
    public NyanceNyanceRevolution NyanceNyanceRevolutionSingleton;


    // Why not just use start in all the children? we want common initialization logic! thats pretty cool!
    protected virtual void Start()
    {
        // We need to get a reference to our singleton 
        NyanceNyanceRevolutionSingleton = NyanceNyanceRevolution.GetInstance();
    }
    public void Move()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }


    // This virtual method allows the game to default to this implementation if derived classes fail to override it.
    // Ensure to override this method in derived classes if you need to change the behavior for score checking.
    public virtual void ScoreCheck()
    {
        Debug.Log("Hey Boss we are for some reason in the parent arrow class. Something is wrong");
    }
    // This function will destroy our arrow
    public void DestroyArrow()
    {
        // We unsubscribe from the proper event, which we stored in the arrow event variable. This works with any arrow
        ArrowEvent.RemoveListener(ScoreCheck);
        // We poof the game object
        Destroy(gameObject);
    }

}




