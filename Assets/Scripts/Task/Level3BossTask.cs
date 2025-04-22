using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3BossTask : AbstractBossTask
{
    public GameObject towerDefenseButton;

    new void Start()
    {
        towerDefenseButton.SetActive(false); // Hide the button at the start
    }

    public override void startTask()
    {
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
