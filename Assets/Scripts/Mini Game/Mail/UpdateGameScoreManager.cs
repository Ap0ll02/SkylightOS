using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameScoreManager : MonoBehaviour
{
    public int score;
    public void update()
    {

    }
    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score: {score}");
    }

}
