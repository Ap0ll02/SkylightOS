using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public GameObject northstar;
    private string Line1 = "We are on wave four! I do not detect Nyan Cat... however, there are lots of kittens remaining. I am detecting something strange. A dog? A Doge.";
    private string Line2 = "Heads up, Operator! I am detecting bugs. This is strange — they seem to be organized. I think Doge is leading them. Get ready!!!";

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
        yield return spawnManager.spawnAmount(9, 30, 0.25f);
        yield return spawnManager.spawnAmount(0, 40, 0.15f);
        yield return spawnManager.SpawnRandom(300, 0, enemyArray.Count -1, 1.0f);
    }
}
