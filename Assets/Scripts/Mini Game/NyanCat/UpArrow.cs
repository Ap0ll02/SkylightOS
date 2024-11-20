using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// Don't yell at me for all the similarities between these classes; the object-oriented programmer in me needs to make these separate.
/// The reason being is that I want to define different graphics based on different arrows.
/// So making different arrows have their own class and be extendable is important.
/// Anyways, enough yapping, this is the up arrow class.
/// </summary>
public class ArrowUp : Arrows
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
            //If we are our of bounds we should explode our arrows 
            Destroy();
    }

    // This is keeping track of the 
    public override void ScoreCheck()
    {
        switch (transform.position.y)
        {
            case float y when (y > 4 & y < 5)
                : Debug.Log("PERFECT UP ARROW");
                Destroy();
                break;
            // We need the range below and above 
            case float y when (y > 3.5 & y <= 4 || y >= 5 & y < 5.5)
                : Debug.Log("Great UP ARROW");
                Destroy();
                break; 
            default
                :
                break;
        }
    }

}
