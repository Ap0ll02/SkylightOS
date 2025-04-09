using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// This is what we are going use to set up and clean up all items related to stage one and extra
/// </summary>
public class Stage1Boss3 : AbstractBossStage
{
    bool isSpawning = false;
    public int spawnPoints;
    public List<GameObject> SpawnWave;
    public GameObject enemiesGameObject;


    public override void BossStartStage()
    {
        StartCoroutine(Spawning());
        Debug.Log("Stage 1");
    }

    public override void BossEndStage()
    {
        isSpawning = false;
    }

    public IEnumerator Spawning()
    {
        while (isSpawning)
        {
            if (spawnPoints <= 0)
                isSpawning = false;
            int spawnIndex = Random.Range(0, SpawnWave.Count - 1);
            spawnPoints -= spawnIndex;
            var enemy = Instantiate(SpawnWave[spawnIndex]);
            enemy.transform.SetParent(enemiesGameObject.transform);
        }
        yield return null;
    }
}
