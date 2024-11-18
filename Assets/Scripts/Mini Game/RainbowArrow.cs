using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
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
    }

    // Update is called once per frame
    void Update()
    {
        // Testing out the movement for our poor arrows 
        transform.position += Vector3.up * speed * Time.deltaTime;
        checkDeystroy();
    }

    // Nyaa~ Position Check will be called by Nyance Nyance Revalution and will receive a corresponding Vector3 for the check arrow, nya!
    // This function will make sure the keypress is only considered when close to the check arrow, nya~
    // We want to be kind to our new players, grandmas, and game journalists at innovation day, nya!
    // HELP I am mentally ill and its 2:30AM at the WPEB
    public void PositionCheck(Vector3 check)
    {
        // Math Abs ensures it will always be a postive number so we still check our two cases, if its above or below  
        float distance = Mathf.Abs(transform.position.y - check.y);
        
        if (distance <= 0.5f)
        {
            switch (distance)
            {
                case float perfectDistance when (perfectDistance >= 0 && perfectDistance <= 0.1):
                    Debug.Log("Perfect");
                    break;
                case float greatDistance when (greatDistance > 0.1 && greatDistance <= 0.3):
                    Debug.Log("Great");
                    break;
                case float goodDistance when (goodDistance > 0.3 && goodDistance <= 0.5):
                    Debug.Log("Good");
                    break;
                default:
                    Debug.Log("Out of range");
                    break;
            }
        }
    }
}
