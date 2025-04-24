using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage9Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public GameObject northstar;
    public string Line1 = "<incr>NICE!!!!!</incr> We Survived our Second wave! The MD5# Reading are off the chart <rainb> rainbow surge </rainb> Intensity is now <shake a = 2> 100%";
    public string Line2 = "<shake>Nyan Cat is being detected!</shake> His army of early 2000's annoying kittens are on the way! Hyperclocking the CPU and elevating computer fluid. Add more towers, Quickly";

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
        yield return spawnManager.spawnAmount(3, 3, 4.0f);
        yield return spawnManager.SpawnRandom(100, 0, enemyArray.Count, 1.0f);
    }
}
