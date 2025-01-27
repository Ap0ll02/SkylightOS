using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// @author Jack
/// @brief Driver task involves the system window then the driver window. This will
/// lead into a minigame, akin to the dino game on google.
public class DriverTask : AbstractTask 
{
    public GameObject system_menu;
    public TMP_Text driver_desc;
    public driver driver_script;
    // Get our references here
    void Awake() {
        taskTitle = "Update Drivers";
        taskDescription = "Looks like our drivers are out of date, perhaps this will solve our issues. Lets use our system's driver menu to update!";
        // ATTENTION: need reference to window canvas' System Menu, could change
        // with Garrett's implementation
        system_menu = FindObjectOfType(WindowCanvas).GetComponentInChildren<SystemResourcesWindow>().gameObject;
    }
    // Broken, non-interactable state loaded here
    new void Start() {
        driver_desc.text = "Driver's out of date. Updates required.";
        // TODO: Steps towards system menu
        // prepare what you can even though the system menu isn't complete
        // Get states ready and design the game.
    }

    public override void startTask() {

    }

    public override void CompleteTask(){

    }

    public override void checkHazards(){

    }

    public override void stopHazards() {

    }

    public override void startHazards(){

    }
}