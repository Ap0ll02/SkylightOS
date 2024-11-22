using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
/// <summary>
/// Author L:ord Quinn Barron of catgirls
/// Down arrow to hold our specific down arrow features
/// This class inherates from the Arrows parent class  
/// </summary>
public class DownArrow : Arrows
{
    // Start is called before the first frame update
    void Start()
    {
        NyanceNyanceRevolutionSingleton = NyanceNyanceRevolution.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        // We need to be moving our arrow, This is defined in parent Class
        Move();
        if (ArrowEvent == null)
        {
            Debug.Log("we got a problem in RIGHT arrow");
        }
    }

    // This is keeping track of the 
    public override void ScoreCheck()
    {
        switch (transform.position.y)
        {
            case float y when (y > 4.4 & y <= 4.6)
                : NyanceNyanceRevolutionSingleton.playerScore +=130;
                Debug.Log(NyanceNyanceRevolutionSingleton.playerScore);
                DestroyArrow();
                break;
            // We need the range below and above 
            case float y when (y > 4.2 & y <= 4.4)
                : NyanceNyanceRevolutionSingleton.playerScore += 40;
                Debug.Log(NyanceNyanceRevolutionSingleton.playerScore);
                DestroyArrow();
                break;
            case float y when (y > 4.0 & y <= 4.2)
                : NyanceNyanceRevolutionSingleton.playerScore += 5;
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
