using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// @author Jack
/// @brief Driver task involves the system window then the driver window. This will
/// lead into a minigame, akin to the dino game on google.
public class DriverTask : AbstractTask
{
    public bool gameRunning = false;
    public GameObject system_menu;
    public TMP_Text driver_desc;
    public DriverGame driver_script;
    public GameObject driver_btn;
    public driver driverPanel;

    // Get our references here
    void Awake()
    {
        taskTitle = "Update Drivers";
        taskDescription =
            "Looks like our drivers are out of date, perhaps this will solve our issues. Lets use our system's driver menu to update!";
        // ATTENTION: need reference to window canvas' System Menu, could change
        // with Garrett's implementation
        system_menu = GameObject
            .Find("WindowCanvas")
            .GetComponentInChildren<SystemWindow>()
            .gameObject;
        driverPanel = FindObjectOfType<driver>();
        //driver_script = system_menu.GetComponentInChildren<DriverGame>();
        //driver_desc = driver_script.gameObject.GetComponentInChildren<TMP_Text>();
        //driver_btn = driver_script.GetComponentInChildren<Button>().gameObject;
    }

    // Broken, non-interactable state loaded here
    new void Start()
    {
        driverPanel.UpdateState(driver.DriversState.NotWorking);
        // TODO: Steps towards system menu
        // prepare what you can even though the system menu isn't complete
        // Get states ready and design the game.
    }

    // Interactable, broken state init here
    public override void startTask()
    {
        gameRunning = true;
        driverPanel.UpdateState(driver.DriversState.NotWorkingInteractable);
    }

    public void OnEnable()
    {
        driver_script.OnGameStart += startHazards;
        driver_script.OnGameEnd += CompleteTask;
    }

    public void OnDisable()
    {
        driver_script.OnGameStart -= startHazards;
        driver_script.OnGameEnd -= CompleteTask;
    }

    // Non-interactable, OS standard state here.
    public override void CompleteTask()
    {
        gameRunning = false;
        stopHazards();
        driverPanel.UpdateState(driver.DriversState.Working);
        base.CompleteTask();
    }

    public override void checkHazards() { }

    public override void stopHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            hazardManager.StopHazard();
        }
    }

    public override void startHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            hazardManager.StartHazard();
        }
    }
}

