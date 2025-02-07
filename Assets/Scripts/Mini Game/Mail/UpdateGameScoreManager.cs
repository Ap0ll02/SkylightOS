using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameScoreManager : MonoBehaviour
{
    public int score;
    public int winScore = 1000;
    public int loseScore = -100;
    public bool winReached = false;
    public bool lossReached = false;
    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score: {score}");
    }

    // This will be used by to check the win and lose conditions. It should be called every frame since its checking two conditions
    // If they reached the win score or if they reached the lossScore. So make sure you are checking both bools for game condition check
    public void ScoreCheck()
    {
        if (score >= winScore)
        {
            winReached = true;
        }
        else if (score <= loseScore)
        {
            lossReached = true;
        }
    }

    public void ResetScore()
    {
        score = 0;
        winReached = false;
        lossReached = false;
    }


}