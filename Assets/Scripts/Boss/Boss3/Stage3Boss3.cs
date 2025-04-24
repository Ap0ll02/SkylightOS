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
    private string Line1 = "<incr>NICE!!!!!</incr> We Survived our Second wave! The MD5# Reading are off the chart <rainb> rainbow surge </rainb> Intensity is now <shake a = 2> 100%";
    private string Line2 = "<shake>Nyan Cat is being detected!</shake> His army of early 2000's annoying kittens are on the way! Hyperclocking the CPU and elevating computer fluid. Add more towers, Quickly";

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
        Debug.Log("Start Stage 3");
        Debug.Assert(spawnManager != null, "Spawn Manager is null");
        spawnManager.enemies = enemyArray;
        yield return spawnManager.spawnAmount(9, 1, 2.0f);

        TurnBasic1LazersOn();
        yield return spawnManager.spawnAmount(4, 3, 2.0f);
        yield return spawnManager.spawnAmount(5, 2, 3.0f);

        TurnBasic2LazersOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count-1, 2f);

        TurnGreatLazerOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count-1, 1.4f);

        TurnNyanLazerOn();
        yield return spawnManager.SpawnRandom(50, 0, enemyArray.Count-1, 1.0f);
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
