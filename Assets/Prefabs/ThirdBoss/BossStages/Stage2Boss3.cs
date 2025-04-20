using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public GameObject northstar;
    public string Line1 = "We Survived our first wave! Im detecting <shake a = 2> Nyan cats pressence!</shake> The MD5# Protocol is detecting A <rainb> rainbow surge </rainb>";
    public string Line2 = "More Nyan kittens are inbound operator! I recommend you upgrade our towers. Click on the tower and look for the upgrade button on the bottom of the screen.";

    public override void BossStartStage()
    {
        northstar.SetActive(true);
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
        yield return spawnManager.spawnAmount(3, 2, 3.0f);
        yield return spawnManager.SpawnRandom(30, 0, enemyArray.Count, 2.0f);
        yield return spawnManager.SpawnRandom(30, 0, enemyArray.Count, 1.7f);
    }
}
