using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public GameObject northstar;
    private string Line1 = "We survived our first wave! I'm detecting <shake a = 2>Nyan Cat's presence!</shake> The MD5# Protocol is detecting a <rainb>rainbow surge</rainb>.";
    private string Line2 = "More Nyan kittens are inbound, Operator! I recommend you upgrade our towers. Click on a tower and look for the upgrade button at the bottom of the screen.";

    public override void BossStartStage()
    {
        northstar.SetActive(true);
        spawnManager.tm.AddTower();
        StartCoroutine(PlayStage());
    }
    public IEnumerator PlayStage()
    {
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line1,1f);
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line2,1f);
        yield return StartSpawning();
        BossEndStage();
    }

    public override void BossEndStage()
    {
        //Debug.Log("Stage 1 End");
        bossManager.NextStage();
    }

    public IEnumerator StartSpawning()
    {
        northstar.GetComponent<NorthStarAdvancedMode>().Turnoff();
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        yield return spawnManager.spawnAmount(0, 15, 1.25f);
        yield return spawnManager.spawnAmount(1, 1, 2f);
        yield return spawnManager.spawnAmount(0, 15, 1f);
        yield return spawnManager.spawnAmount(2, 2, 2f);
        yield return spawnManager.spawnAmount(0, 20, 0.75f);
        yield return spawnManager.spawnAmount(3, 2, 2f);
        yield return spawnManager.spawnAmount(0, 30, 0.50f);
        yield return spawnManager.spawnAmount(4, 2, 3f);
        yield return spawnManager.spawnAmount(0, 10, 0.50f);
        yield return spawnManager.SpawnRandom(25,0, enemyArray.Count -1, 1f);
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
