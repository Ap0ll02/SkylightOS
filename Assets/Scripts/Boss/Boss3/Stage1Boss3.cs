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
    public GameObject northstar;
    private string Line1 = "<bounce>Welcome, Operator!</bounce> We chased the viruses all the way to the motherboard. They are desperate and launching a full-on assault on the GPU.";
    private string Line2 = "We have to <shake>stop them!</shake> I'm activating the computer defense system! Start grabbing towers and placing them down on the motherboard.";
    public override void BossStartStage()
    {
        northstar.SetActive(true);
        spawnManager.tm.AddTower();
        StartCoroutine(PlayStage());
    }

    public IEnumerator PlayStage()
    {
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line1,0.25f);
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line2,0.25f);
        yield return StartSpawning();
        BossEndStage();
    }

    public override void BossEndStage()
    {
        bossManager.NextStage();
    }

    public IEnumerator StartSpawning()
    {
        northstar.GetComponent<NorthStarAdvancedMode>().Turnoff();
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        yield return spawnManager.spawnAmount(0, 15, 1.25f);
        yield return spawnManager.spawnAmount(1, 1, 2.0f);
        yield return spawnManager.spawnAmount(0, 15, 1.15f);
        yield return spawnManager.spawnAmount(1, 1, 2.0f);
        yield return spawnManager.spawnAmount(0, 10, 1f);
        yield return spawnManager.spawnAmount(2, 1, 2.0f);
        yield return spawnManager.spawnAmount(0, 15, 1f);
        yield return spawnManager.spawnAmount(3, 1, 3.0f);
        yield return SpawnEnding();
    }

    public IEnumerator SpawnEnding()
    {
        bool stillEnemies = false;
        while (spawnManager.enemyContainer.GetComponent<Transform>().childCount > 0)
        {
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }


}
