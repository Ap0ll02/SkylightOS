using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Jack Ratermann
// Main Internet Task 1 Script
// Depends on Abstract Task
// Interacts with multiple UI elements
// Modified heavily by Garrett

public class InternetTaskOne : AbstractTask
{
    // References to the extended wifi pop up menu, and the diagnosis menu.
    [SerializeField] GameObject wifiPopUpMenu;
    [SerializeField] GameObject diagnosisWindow;

    // Reference To Progress Bar Script
    [SerializeField] LoadingScript loadingBarScript;

    // Booleans for when to allow menus to show
    bool wifiPopUp = false;
    bool diagnoseWindow = false;

    // Initialization
    public void Start()
    {
        wifiPopUpMenu = FindObjectOfType<ExpandedWifiMenu>().gameObject;
        wifiPopUpMenu.SetActive(false);
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>().gameObject;
        diagnosisWindow.SetActive(false);
        loadingBarScript = diagnosisWindow.GetComponentInChildren<LoadingScript>();
    }

    // Update is called once per frame
    public void Update()
    {
        checkHazards();
    }

    // Toggle Visibility Of Wifi Pop Up Menu
    public void OnWifiBtnClick()
    {
        // Toggles Visibility With Button Press
        if (wifiPopUp == false && !diagnosisWindow.activeSelf ) {
            wifiPopUpMenu.SetActive(true);
            wifiPopUp = true;
        }
        else
        {
            wifiPopUpMenu.SetActive(false);
            wifiPopUp = false;
        }
    }

    // Pull up diagnosis menu
    public void OnDiagnosisBtnClick()
    {
        // Toggles Visibility With Button Press
        if (diagnoseWindow == false)
        {
            diagnosisWindow.SetActive(true);
            wifiPopUp = false;
            wifiPopUpMenu.SetActive(false);
            diagnoseWindow = true;
        }
        else
        {
            diagnosisWindow.SetActive(false);
            diagnoseWindow = false;
        }
        startTask();
        startHazards();
    }

    // Start Internet Task 1
    public override void startTask()
    {
        loadingBarScript.StartLoading();
    }

    // Ask the hazard manager if our task can progress
    // Idea use a percentage to slow down the task progress instead of completely stopping it
    public override void checkHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            if (!hazardManager.CanProgress())
            {
                loadingBarScript.canContinue = false;
            }
            else
            {
                loadingBarScript.canContinue = true;
            }
        }
    }
    // This will request the manager to stop / end a hazard
    public override void stopHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            hazardManager.StopHazard();
        }
    }

    // this will request our manager to start making hazards
    public override void startHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            hazardManager.StartHazard();
        }
    }

}
