using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeStage4 : AbstractBossStage
{
    public override void BossStartStage()
    {
        Debug.Log("Stage 4");
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 4 End");
        bossManager.NextStage();
    }

}

