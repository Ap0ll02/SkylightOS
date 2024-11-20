using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// This is the leftArrow
/// </summary>
public class LeftArrow : Arrows
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // We need to be moving our arrow, This is defined in parent Class
        Move();

        if(OutOfBounds())
            // If we are our of bounds we should explode our arrows 
            Destroy();
    }

    // This is keeping track of the 
    public override void ScoreCheck()
    {
        switch (transform.position.y)
        {
            case float y when (y > 4 & y < 5)
                : Debug.Log("PERFECT Left ARROW");
                Destroy();
                break;
            // We need the range below and above 
            case float y when (y > 3.5 & y <= 4 || y >= 5 & y < 5.5)
                : Debug.Log("Great Left ARROW");
                Destroy();
                break; 
            default
                :
                break;
        }
    }

}
