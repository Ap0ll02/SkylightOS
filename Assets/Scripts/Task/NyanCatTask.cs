using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NyanCatTask : AbstractBossTask
{
    public GameObject NyanCatbutton;

    public NyanceNyanceRevolution nyanCatRevolution;

    // Start is called before the first frame update
    public override void startTask()
    {
        NyanCatbutton.SetActive(true);
    }

    public void OnNyanButton()
    {
        NyanCatbutton.SetActive(false);
        nyanCatRevolution.StartNyanceNyanceRevolution();
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
