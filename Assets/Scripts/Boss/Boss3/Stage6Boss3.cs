using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage6Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public GameObject northstar;
    private string Line1 = "I'm detecting Shiba Inu activity with my dog monitoring system... Is that... <shake>Doge!</shake>";
    private string Line2 = "Alright, Operator! Finish this fight. Put down towers and buy me some more time — we're almost there!";

    public override void BossStartStage()
    {
        if (bossManager is BossManager3 bossManager3)
            bossManager3.musicManager.DogeBossMusic();
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
        spawnManager.enemies = enemyArray;
        yield return spawnManager.spawnAmount(17, 1, 1f);
        yield return spawnManager.SpawnRandom(800, 0, enemyArray.Count-2, 0.25f);
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
