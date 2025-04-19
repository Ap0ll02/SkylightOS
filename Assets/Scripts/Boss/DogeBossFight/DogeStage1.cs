using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeStage1 : AbstractBossStage
{
    Lever lever;
    // Start is called before the first frame update
    public override void BossStartStage()
    {
        Debug.Log("Stage 1");
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 1 End");
        bossManager.NextStage();
    }

}
