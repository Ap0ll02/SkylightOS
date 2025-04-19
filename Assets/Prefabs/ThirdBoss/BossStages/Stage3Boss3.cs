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
    public GameObject NyanCatBoss;
    public override void BossStartStage()
    {

        Debug.Log("Start Stage 23");
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        StartCoroutine(StartSpawning());
    }

    public override void BossEndStage()
    {
        LazersOff();
        bossManager.NextStage();
    }
    IEnumerator seconds()
    {
        yield return new WaitForSeconds(1);
        BossEndStage();
    }

    public IEnumerator StartSpawning()
    {
        TurnBasic1LazersOn();
        yield return spawnManager.spawnAmount(4, 3, 2.0f);
        yield return spawnManager.spawnAmount(5, 2, 3.0f);
        TurnBasic2LazersOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count, 2f);
        TurnGreatLazerOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count, 1.4f);
        TurnNyanLazerOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count, 1.0f);
        BossEndStage();
    }

    public void TurnBasic1LazersOn()
    {
        foreach (var laz in LazerArray)
        {
            var lazer = laz.GameObject().GetComponent<Animator>();
            lazer.SetBool("basic1",true);
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
            var lazer = laz.GameObject().GetComponent<Animator>();
            lazer.SetBool("NyanLazer",false);
        }
    }
}
