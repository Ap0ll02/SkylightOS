using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeStage1 : AbstractBossStage
{
    public override void BossStartStage()
    {
        Debug.Log("Stage 1");
        StartCoroutine(seconds());
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 1 End");
        bossManager.NextStage();
        //gameObject.SetActive(false);
    }

    IEnumerator seconds()
    {
        yield return new WaitForSeconds(1);
        BossEndStage();
    }
}
