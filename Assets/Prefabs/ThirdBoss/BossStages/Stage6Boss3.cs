using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Boss3 : AbstractBossStage
{
    public override void BossStartStage()
    {
        Debug.Log("Start Stage 6");
        StartCoroutine(seconds());
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 6 End");
        bossManager.NextStage();
    }

    IEnumerator seconds()
    {
        yield return new WaitForSeconds(1);
        BossEndStage();
    }
}
