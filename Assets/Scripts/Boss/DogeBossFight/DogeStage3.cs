using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeStage3 : AbstractBossStage
{
    public override void BossStartStage()
    {
        Debug.Log("Stage 3");
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 3 End");
        bossManager.NextStage();
    }
}
