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
    // This holds the animator for the explosion!
    public Animator explosionAnimator;
    // Holds the NyanceNyanceReveloution singelton class so we can update score and do other sick shit
    public NyanceNyanceRevolution NyanceNyanceRevolutionSingleton;
    // This all the variables that control the scoring range, they can be adjusted in the child start classes UNDER base.start() 
    public float perfectTop = 4.6f;
    public float perfectBottom = 4.5f;
    public float greatTop = 4.5f;
    public float greatBottom = 4.2f;
    public float goodTop = 4.2f;
    public float goodBottom = 4.0f;


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
    public void ScoreCheck()
    {
        switch (transform.position.y)
        {
            case float y when (y > perfectBottom && y <= perfectTop)
                :
                explosionAnimator.SetTrigger("Perfect");
                NyanceNyanceRevolutionSingleton.playerScore += 100;
                DestroyArrow();
                break;
            // We need the range below and above 
            case float y when (y > greatBottom && y <= greatTop)
                :
                explosionAnimator.SetTrigger("Great");
                NyanceNyanceRevolutionSingleton.playerScore += 50;
                DestroyArrow();
                break;
            case float y when (y > goodBottom && y <= goodTop)
                :
                explosionAnimator.SetTrigger("Good");
                NyanceNyanceRevolutionSingleton.playerScore += 25;
                DestroyArrow();
                break;
            case float y when (y > outOfBounds)
                :
                DestroyArrow();
                break;
            default
                :
                break;
        }
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




