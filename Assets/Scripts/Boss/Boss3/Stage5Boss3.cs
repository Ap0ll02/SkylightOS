using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public GameObject northstar;
    private string Line1 = "Heads up! It seems that the bugs are organizing for another wave. Hurry place down some more towers and get ready for the bugs";
    private string Line2 = "We are more then half way there, you just need to hold them off until I am able to finish running the virus Exploder 3000 protocol";

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
        BossEndStage();
    }

    public IEnumerator StartSpawning()
    {
        yield return spawnManager.spawnAmount(3, 3, 4.0f);
        yield return spawnManager.SpawnRandom(100, 0, enemyArray.Count, 1.0f);
        BossEndStage();
    }
}
