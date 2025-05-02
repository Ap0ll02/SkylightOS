using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Garrett Contaldi
/// </summary>
public class DogeBossTask : AbstractBossTask
{
    public GameObject dogeBossButton;

    public DogeBossFightManager dogeBossFight;

    public MusicManager musicManager;

    public Northstar northstar;

    public override void startTask()
    {
        musicManager = MusicManager.Instance;
        musicManager.StopMusic();
        northstar = FindObjectOfType<Northstar>();
        northstar.WriteHint("Holy moly another <pend a=0.5 f=0.8>ominous</pend> button just appeared!", Northstar.Style.cold, true);
        dogeBossButton.SetActive(true);
    }

    public void OnDogeButton()
    {
        dogeBossButton.SetActive(false);
        dogeBossFight.StartBossFight();
    }

    public void OnEnable()
    {
        Doge.OnGameEnd += TriggerBossTaskFinished;
    }

    public void OnDisable()
    {
        Doge.OnGameEnd -= TriggerBossTaskFinished;
    }

    public new void TriggerBossTaskFinished()
    {
        base.TriggerBossTaskFinished();
        musicManager.ResumeMusic();
        northstar.WriteHint("Don't make me jump on platforms like that again. My feet <pend a=0.5 f=0.8>hurt</pend>!", Northstar.Style.cold, true);
        northstar.StartHintCoroutine("Press the power button to go to the next level :)", 18f);
    }

    public override void startHazards()
    {

    }
    public override void stopHazards()
    {

    }

    public override void checkHazards()
    {

    }

}
