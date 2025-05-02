using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Boss3 : AbstractBossStage
{
    public AudioSource audioSource;
    public AudioClip clip;
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
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
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
        yield return spawnManager.spawnAmount(9, 15, 0.25f);
        yield return spawnManager.spawnAmount(0, 15, 0.25f);
        yield return spawnManager.spawnAmount(9, 25, 0.25f);
        yield return spawnManager.spawnAmount(0, 25, 0.25f);
        yield return spawnManager.SpawnRandom(300, 0, enemyArray.Count -1, .5f);
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
