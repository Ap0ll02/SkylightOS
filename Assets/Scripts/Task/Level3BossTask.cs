using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3BossTask : AbstractBossTask
{
    public GameObject towerDefenseButton;

    public Northstar northstar;

    new void Start()
    {
        towerDefenseButton.SetActive(false); // Hide the button at the start
    }

    public override void startTask()
    {
        northstar = FindObjectOfType<Northstar>();
        northstar.WriteHint("A third <pend a=0.5 f=0.8>ominous</pend> button just dropped :(", Northstar.Style.cold, true);
        northstar.StartHintCoroutine("Press the button so we can finish the game little bro", 18f);
        towerDefenseButton.SetActive(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
