using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public GameObject northstar;
    private string Line1 = "Heads up! It seems the bugs are organizing for another wave. Hurry, place down some more towers and get ready for the bugs.";
    private string Line2 = "We are more than halfway there. You just need to hold them off until I finish running the Virus Exploder 3000 protocol.";

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
        BossEndStage();
    }
    public override void BossEndStage()
    {
        bossManager.NextStage();
    }

    public IEnumerator StartSpawning()
    {
        northstar.GetComponent<NorthStarAdvancedMode>().Turnoff();
        Debug.Log("Start Stage 5");
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        yield return spawnManager.SpawnRandom(300, 0, enemyArray.Count-1, 0.5f);
        yield return spawnManager.spawnAmount(0, 20, 0.15f);
        yield return spawnManager.SpawnRandom(300, 0, enemyArray.Count-1, 0.25f);
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
