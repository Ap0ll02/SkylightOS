using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeBossFightManager : BossManager
{
    bool PlayingGameOver = false;
    // This method will call the Next stage and start the boss fight
    public void StartBossFight()
    {
        if (!PlayingGameOver)
        {
            PlayingGameOver = true;
            currentBossStageIndex = 0;
            Debug.Log("Starting Boss Fight");
            NextStage();
        }
    }
}
