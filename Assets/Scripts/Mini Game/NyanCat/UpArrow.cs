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
        NyanceNyanceRevolutionSingleton = NyanceNyanceRevolution.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (ArrowEvent == null)
        {
            Debug.Log("we got a problem in RIGHT arrow");
        }
        // We need to be moving our arrow, This is defined in parent Class
        Move();
    }

    // This is keeping track of the 
    public override void ScoreCheck()
    {
        switch (transform.position.y)
        {
            case float y when (y > 4.4 & y <= 4.6)
                : NyanceNyanceRevolutionSingleton.playerScore += 105;
                Debug.Log(NyanceNyanceRevolutionSingleton.playerScore);
                
                DestroyArrow();
                break;
            // We need the range below and above 
            case float y when (y > 4.2 & y <= 4.4)
                : NyanceNyanceRevolutionSingleton.playerScore += 70;
                Debug.Log(NyanceNyanceRevolutionSingleton.playerScore);
                DestroyArrow();
                break;
            case float y when (y > 4.0 & y <= 4.2)
                : NyanceNyanceRevolutionSingleton.playerScore += 10;
                Debug.Log(NyanceNyanceRevolutionSingleton.playerScore);
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

}
