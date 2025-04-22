using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeBossTask : AbstractBossTask
{
    public GameObject dogeBossButton;

    public DogeBossFightManager dogeBossFight;

    public override void startTask()
    {
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
