using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Boss3 : AbstractBossStage
{
    public override void BossStartStage()
    {
        Debug.Log("Start Stage 3");
        StartCoroutine(seconds());
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 3 End");
        bossManager.NextStage();
    }

    IEnumerator seconds()
    {
        yield return new WaitForSeconds(1);
        BossEndStage();
    }
}
