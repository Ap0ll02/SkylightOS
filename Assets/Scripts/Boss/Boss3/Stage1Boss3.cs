using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// This is what we are going use to set up and clean up all items related to stage one and extra
/// </summary>
public class Stage1Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public override void BossStartStage()
    {

        Debug.Log("Start Stage 1");
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        StartCoroutine(spawnManager.SpawnRandom(100, 0, 7, 2.0f));
    }

    public override void BossEndStage()
    {
        //Debug.Log("Stage 1 End");
        bossManager.NextStage();
    }
    IEnumerator seconds()
    {
        yield return new WaitForSeconds(1);
        BossEndStage();
    }

}
