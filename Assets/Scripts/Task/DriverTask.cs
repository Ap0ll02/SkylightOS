using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// @author Jack
/// @brief Driver task involves the system window then the driver window. This will
/// lead into a minigame, akin to the dino game on google.
public class DriverTask : AbstractTask 
{
    public GameObject system_menu;
    public TMP_Text driver_desc;
    public DriverGame driver_script;
    public GameObject driver_btn;
    // Get our references here
    void Awake() {
        taskTitle = "Update Drivers";
        taskDescription = "Looks like our drivers are out of date, perhaps this will solve our issues. Lets use our system's driver menu to update!";
        // ATTENTION: need reference to window canvas' System Menu, could change
        // with Garrett's implementation
        system_menu = GameObject.Find("WindowCanvas").GetComponentInChildren<SystemWindow>().gameObject;
        driver_script = system_menu.GetComponentInChildren<DriverGame>();
        driver_desc = driver_script.gameObject.GetComponentInChildren<TMP_Text>();
        driver_btn = driver_script.GetComponentInChildren<Button>().gameObject;
    }
    // Broken, non-interactable state loaded here
    new void Start() {
        driver_desc.text = "Drivers out of date. Updates required.";
        // TODO: Steps towards system menu
        // prepare what you can even though the system menu isn't complete
        // Get states ready and design the game.
        driver_btn.GetComponent<CanvasGroup>().alpha = 0;
        driver_btn.GetComponent<CanvasGroup>().interactable = false;
    }

    // Interactable, broken state init here
    public override void startTask() {
        driver_btn.GetComponent<CanvasGroup>().alpha = 1;
        driver_btn.GetComponent<CanvasGroup>().interactable = true;
        driver_desc.text = "Drivers out of date. Updates required.";
   }

    public void OnEnable(){
        driver_script.OnGameEnd += CompleteTask;
    }

    public void OnDisable(){
        driver_script.OnGameEnd -= CompleteTask;
    }

    // Non-interactable, OS standard state here.
    public override void CompleteTask(){
        driver_desc.text = "Your drivers are up to date.";
        driver_btn.GetComponent<CanvasGroup>().alpha = 0;
        driver_btn.GetComponent<CanvasGroup>().interactable = false;
    }

    public override void checkHazards(){

    }

    public override void stopHazards()
    {
        foreach (var hazardManager in hazardManagers) {
            hazardManager.StopHazard();
        }
    }

    public override void startHazards()
    {
        foreach (var hazardManager in hazardManagers) {
            hazardManager.StartHazard();
        }
    }
    
}