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
    private string Line1 = "<bounce>Welcome Operator! </bounce>We chased the viruses all the way to the mother board.They are desperate, and lunching a full on assualt on the GPU";
    private string Line2 =
        "We Have to <shake>stop them!</shake> Im activating the computer defense system! Start grabbing towers and placing them down on the mother board.";
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

    IEnumerator seconds()
    {
        yield return new WaitForSeconds(1);
    }

    public IEnumerator StartSpawning()
    {
        northstar.GetComponent<NorthStarAdvancedMode>().Turnoff();
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        yield return spawnManager.spawnAmount(0, 4, 3.0f);
        yield return spawnManager.spawnAmount(1, 2, 2.0f);
        yield return spawnManager.SpawnRandom(40, 0, enemyArray.Count, 2.0f);
    }


}
