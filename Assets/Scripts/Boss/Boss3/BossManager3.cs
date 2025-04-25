using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Author Quinn or something like that
/// Use this class to manage things that need to persist past individual stages
///
public class BossManager3 : BossManager
{
    public void Start()
    {
        currentBossStageIndex = 0;
        Debug.Log("Starting Boss Fight");
        NextStage();
    }

    public override void NextStage()
    {
        Debug.Log("currentBossStageIndex = " + currentBossStageIndex);
        if (currentBossStageIndex >= bossStagePrefabs.Length)
        {
            Debug.Log("Boss is done");
            return;
        }
        if (bossStagePrefabs[currentBossStageIndex] == null || bossStagePrefabs.Length == 0)
        {
            Debug.Log("We are ethier empty or null");
        }
        // we capture a reference to the Boss stage we are currently working with
        bossStagePrefabs[currentBossStageIndex].SetActive(true);
        var BossStage = bossStagePrefabs[currentBossStageIndex].GetComponent<AbstractBossStage>();
        // If we still have boss stages we should incremeant there is some helpful debugging logs if you really need it
        // Debug.Log($"Boss Stage Length: {bossStagePrefabs.Length}");
        if (currentBossStageIndex < bossStagePrefabs.Length)
        {
            // This invokes the start stage of the next Boss stage
            BossStage.BossStartStage();
            // We need to hit the next stage so we increment our index
            currentBossStageIndex++;
            Debug.Log("Boss stage " + (currentBossStageIndex));
        }
        else
        {
            SceneManager.LoadScene("EndingCutscene");
        }
    }
}
