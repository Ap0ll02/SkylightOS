using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTestManager : BossManager
{
    public void StartBossFight()
    {
        currentBossStageIndex = 0;
        Debug.Log("Starting Boss Fight");
        NextStage();
    }
}