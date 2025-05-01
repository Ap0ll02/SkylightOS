using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage3Boss3 : AbstractBossStage
{
    public SpawnManagerBoss3 spawnManager;
    public List<GameObject> enemyArray;
    private bool spawning;
    public List<GameObject> LazerArray;
    public GameObject northstar;
    private string Line1 = "<incr>NICE!!!!!</incr> We survived our second wave! The MD5# readings are off the charts — <rainb>rainbow surge</rainb> intensity is now at <shake a = 2>100%</shake>.";
    private string Line2 = "<shake>Nyan Cat is being detected!</shake> His army of early 2000s annoying kittens is on the way! Hyperclocking the CPU and elevating computer fluid. Add more towers — quickly!";
    private string Line3 = "<rainb><shake a = 2>NYAN CAT IS HERE, Hurry up and place down more towers!</shake></rainb>";

    public override void BossStartStage()
    {
        northstar.SetActive(true);
        spawnManager.tm.AddTower();
        spawnManager.tm.AddTower();
        StartCoroutine(PlayStage());
    }
    public IEnumerator PlayStage()
    {
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line1,1f);
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line2,1f);
        yield return StartSpawning();
        if (bossManager is BossManager3 bossManager3)
            bossManager3.musicManager.StartMusic();
        BossEndStage();
    }

    public override void BossEndStage()
    {
        LazersOff();
        bossManager.NextStage();
    }
    public IEnumerator Diologue()
    {
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line1,0.2f);
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line2,0.2f);
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
        yield return spawnManager.spawnAmount(0, 40, 0.25f);
        yield return spawnManager.SpawnRandom(70, 0, enemyArray.Count-1, 2f);
        TurnBasic1LazersOn();
        yield return spawnManager.SpawnRandom(70, 0, enemyArray.Count-1, 1.5f);
        northstar.SetActive(true);
        yield return northstar.GetComponent<NorthStarAdvancedMode>().PlayDialogueLine(Line3,0.1f);
        northstar.SetActive(false);
        if (bossManager is BossManager3 bossManager3)
            bossManager3.musicManager.NyanCatMusic();
        yield return spawnManager.spawnAmount(9, 1, 10f);
        TurnBasic2LazersOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count-1, 1.5f);

        TurnGreatLazerOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count-1, 1.5f);

        TurnNyanLazerOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count-1, 1.0f);

        yield return SpawnEnding();
    }

    public void TurnBasic1LazersOn()
    {
        foreach (var laz in LazerArray)
        {
            var lazer = laz.GameObject().GetComponent<Animator>();
            lazer.SetBool("Basic1",true);
        }
    }
    public void TurnBasic2LazersOn()
    {
        foreach (var laz in LazerArray)
        {
            var lazer = laz.GameObject().GetComponent<Animator>();
            lazer.SetBool("Basic1",false);
            lazer.SetBool("Basic2",true);
        }
    }

    public void TurnGreatLazerOn()
    {
        foreach (var laz in LazerArray)
        {
            var lazer = laz.GameObject().GetComponent<Animator>();
            lazer.SetBool("Basic2",false);
            lazer.SetBool("GreatLazer",true);
        }
    }

    public void TurnNyanLazerOn()
    {
        foreach (var laz in LazerArray)
        {
            var lazer = laz.GameObject().GetComponent<Animator>();
            lazer.SetBool("Basic2",false);
            lazer.SetBool("NyanLazer",true);
        }
    }
    public void LazersOff()
    {
        foreach (var laz in LazerArray)
        {
            laz.SetActive(false);
        }
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
