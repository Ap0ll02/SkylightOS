using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public override void BossStartStage()
    {

        Debug.Log("Start Stage 6");
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        StartCoroutine(StartSpawning());
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

    public IEnumerator StartSpawning()
    {
        yield return spawnManager.spawnAmount(3, 3, 4.0f);
        yield return spawnManager.SpawnRandom(100, 0, enemyArray.Count, 1.0f);
        BossEndStage();
    }
}
