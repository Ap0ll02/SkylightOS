using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// ##Author Quinn Contaldi Lord Knight Commander of Castle Catgirl
/// IDK its another fucking arrow
/// </summary>
public class RainbowArrow : Arrows
{
    //@var speed controls the speed 
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        NyanceNyanceRevalution.OnKeyPress += HandleKeyPress;
    }

    // Update is called once per frame
    void Update()
    {
        // Testing out the movement for our poor arrows 
        transform.position += Vector3.up * speed * Time.deltaTime;
        checkDestroy(false);
    }

    // Nyaa~ Position Check will be called by Nyance Nyance Revalution and will receive a corresponding Vector3 for the check arrow, nya!
    // This function will make sure the keypress is only considered when close to the check arrow, nya~
    // We want to be kind to our new players, grandmas, and game journalists at innovation day, nya!
    // HELP I am mentally ill and its 2:30AM at the WPEB
    public void positionCheck()
    {
        // Math Abs ensures it will always be a postive number so we still check our two cases, if its above or below  
        // arrowCheckMeasument is the Y position of the check arrow and inherated from the Parent class Arrows
        float distance = Mathf.Abs(transform.position.y - arrowCheckMeasument);
        
        switch (distance)
        {
            case float perfectDistance when (perfectDistance >= 0 && perfectDistance <= 0.1):
                Debug.Log("Perfect");
                checkDestroy(true);
                break;
            case float greatDistance when (greatDistance > 0.1 && greatDistance <= 0.3):
                Debug.Log("Great");
                checkDestroy(true);
                break;
            case float goodDistance when (goodDistance > 0.3 && goodDistance <= 0.5):
                Debug.Log("Good");
                checkDestroy(true);
                break;
            default:
                Debug.Log("Out of range");
                break;
        }
    }

    // Handles deystruction of arrows and unsubscribing from events 
    // I have to clean this shit up later, but it works for now. It has two conditions, being greater then 8
    // or it was called for check position
    public void checkDestroy(bool checkPress)
    {
        // The arrows only move up so we just compare its Y location 
        if (transform.position.y > 8 || checkPress)
        {
            Destroy(gameObject);
            NyanceNyanceRevalution.OnKeyPress -= HandleKeyPress;
        }
    }

    private void HandleKeyPress(KeyCode keyCode)
    {
        positionCheck();
    }
}
