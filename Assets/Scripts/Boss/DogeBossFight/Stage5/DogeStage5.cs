using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeStage5 : AbstractBossStage
{
    public override void BossStartStage()
    {
        Debug.Log("Stage 5");
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 5 End");
        bossManager.NextStage();
    }
}
