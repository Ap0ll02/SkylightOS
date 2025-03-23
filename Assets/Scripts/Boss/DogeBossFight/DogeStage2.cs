using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeStage2 : AbstractBossStage
{
    public override void BossStartStage()
    {
        Debug.Log("Stage 2");
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 2 End");
        bossManager.NextStage();
    }

}
