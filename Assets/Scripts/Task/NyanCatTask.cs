using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NyanCatTask : AbstractBossTask
{
    public GameObject NyanCatbutton;

    public NyanceNyanceRevolution nyanCatRevolution;

    public Northstar northstar;

    // Start is called before the first frame update
    public override void startTask()
    {
        NyanCatbutton.SetActive(true);
        northstar.WriteHint("What is that <pend a=0.5 f=0.8>ominous</pend> icon that just appeared!", Northstar.Style.cold, true);
    }

    public void OnNyanButton()
    {
        NyanCatbutton.SetActive(false);
        nyanCatRevolution.StartNyanceNyanceRevolution();
    }

    public void OnEnable()
    {
        nyanCatRevolution.OnGameEnd += TriggerBossTaskFinished;
    }

    public void OnDisable()
    {
        nyanCatRevolution.OnGameEnd -= TriggerBossTaskFinished;
    }

    public new void TriggerBossTaskFinished()
    {
        base.TriggerBossTaskFinished();
        northstar.WriteHint("That was <pend a=0.5 f=0.8>fun</pend>!", Northstar.Style.hot, true);
        northstar.StartHintCoroutine("I should probably get back to work now...", 10f);
    }

    public override void startHazards()
    {
        // This shit aint got hazards big dawg
    }

    public override void stopHazards()
    {

    }

    public override void checkHazards()
    {

    }
}
