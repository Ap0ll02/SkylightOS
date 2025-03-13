using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanCatBossManager : BossManager
{
    // This controls the scoring variables, which will affect how many items you can shake out of Nyan cat
    public int playerScore;
    public int winGood = 4000;
    public int winGreat = 6000;
    public int winPerfect = 7000;
    void Start()
    {
        //bossStageArray[currentBossStageIndex].BossStartStage();
    }

}
