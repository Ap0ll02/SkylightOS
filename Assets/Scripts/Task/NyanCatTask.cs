using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NyanCatTask : AbstractBossTask
{
    public GameObject NyanCatBossFightPrefab;
    
    // Start is called before the first frame update
    public override void startTask()
    {
        NyanCatBossFightPrefab.SetActive(true);
    }

    public override void startHazards()
    {
        throw new Exception("We dont have any hazards to start");
    }
    public override void stopHazards()
    {
        NyanCatBossFightPrefab.SetActive(false);
    }

    public override void checkHazards()
    {
        throw new Exception("We dont have any hazards to start");
    }
}
