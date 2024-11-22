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
        //if(OutOfBounds())
            // If we are our of bounds we should explode our arrows 
            //Destroy();
    }
    
    // This is keeping track of the 
    public override void ScoreCheck()
    {
        switch (transform.position.y)
        {
            case float y when (y > 4.4 & y <= 4.6)
                : NyanceNyanceRevolutionSingleton.playerScore += 110;
                Debug.Log(NyanceNyanceRevolutionSingleton.playerScore);
                DestroyArrow();
                break;
            // We need the range below and above 
            case float y when (y > 4.2 & y <= 4.4)
                : NyanceNyanceRevolutionSingleton.playerScore += 60;
                Debug.Log(NyanceNyanceRevolutionSingleton.playerScore);
                DestroyArrow();
                break;
            case float y when (y > 4.0 & y <= 4.2)
                :NyanceNyanceRevolutionSingleton.playerScore += 20;
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
