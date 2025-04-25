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
        yield return seconds();
        BossEndStage();
    }

    public override void BossEndStage()
    {
        //Debug.Log("Stage 1 End");
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
        yield return spawnManager.spawnAmount(0, 10, 1.5f);
        yield return spawnManager.spawnAmount(1, 1, 4f);
        yield return spawnManager.spawnAmount(0, 15, 1f);
        yield return spawnManager.spawnAmount(2, 1, 4f);
        yield return spawnManager.spawnAmount(0, 15, 0.75f);
        yield return spawnManager.spawnAmount(3, 1, 4f);
        yield return spawnManager.spawnAmount(0, 20, 0.75f);
        yield return spawnManager.spawnAmount(4, 1, 3f);
        yield return spawnManager.spawnAmount(0, 15, 0.50f);
        yield return spawnManager.spawnAmount(5, 1, 6f);
        yield return spawnManager.spawnAmount(0, 25, 0.25f);
    }
}
