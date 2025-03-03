using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NyanCatTask : AbstractBossTask
{
    public GameObject NyanCatBossFight;
    public GameObject BossCanvas;
    public GameObject OsManagerWindow;
    public GameObject SKYBIOSWINDOW;
    // Start is called before the first frame update
    public override void startTask()
    {
        //NyanCatBossFight = GameObject.Find("NyanBossFight");
        //BossCanvas = GameObject.Find("BossCanvas");
        NyanCatBossFight.SetActive(true);
        BossCanvas.SetActive(true);
        OsManagerWindow.SetActive(false);
        SKYBIOSWINDOW.SetActive(false);
    }

    public override void startHazards()
    {
        throw new Exception("We dont have any hazards to start");
    }
    public override void stopHazards()
    {
        NyanCatBossFight.SetActive(false);
        BossCanvas.SetActive(false);
    }

    public override void checkHazards()
    {
        throw new Exception("We dont have any hazards to start");
    }
}
