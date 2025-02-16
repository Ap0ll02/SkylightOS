using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSmashGameScoreManager : MonoBehaviour
{
    public int score = 0;
    // Amount of bugs that we need to kill
    public int winScore = 1000;
    // In this rendition this is the amount of hearts the player has
    public int loseScore;
    public bool winReached = false;
    public bool lossReached = false;

    public CatGirlWizard player;

    public void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("CatGirlWizardPlayer").GetComponent<CatGirlWizard>();
        }

        if (player == null)
        {
            throw new Exception("Player not found");
        }
    }

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
        else if (player.isDead)
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
