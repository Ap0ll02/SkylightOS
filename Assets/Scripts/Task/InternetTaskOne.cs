using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Jack Ratermann
// Refactored by Garrett Sharp
// Main Internet Task 1 Script
// Depends on Abstract Task
// Interacts with multiple UI elements
// 

public class InternetTaskOne : AbstractTask
{
    // References to the extended wifi pop up menu, and the diagnosis menu.
    [SerializeField] GameObject wifiPopUpMenu;
    [SerializeField] ExpandedWifiMenu wifiPopUpMenuWifiState;
    [SerializeField] GameObject diagnosisWindow;

    // Reference To Progress Bar Script
    [SerializeField] LoadingScript loadingBarScript;

    // Initialization
    public void Awake()
    {
        // Assigning all of the references must be done on awake so that they actually work
        wifiPopUpMenu = FindObjectOfType<ExpandedWifiMenu>().gameObject;
        wifiPopUpMenuWifiState = wifiPopUpMenu.GetComponent<ExpandedWifiMenu>();
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>().gameObject;
        loadingBarScript = diagnosisWindow.GetComponentInChildren<LoadingScript>();
    }

    public void Start()
    {
        // Set the task title and description
        taskTitle = "Fix wifi";
        taskDescription = "Connect to the wifi by opening the wifi menu and connecting to the wifi";
        // Automatically turn off the game object at the start of the scene.
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        checkHazards();
    }

    // Message handler for opening the diagnosis window
    void OnEnable()
    {
        DiagnosisWindow.OnDiagnosisWindowOpened += HandleDiagnosisWindowOpened;
        DiagnosisWindow.LoadingDoneNotify += CompleteTask;
    }

    // Removing message handler?
    void OnDisable()
    {
        DiagnosisWindow.OnDiagnosisWindowOpened -= HandleDiagnosisWindowOpened;
        DiagnosisWindow.LoadingDoneNotify -= CompleteTask;
    }

    // When the diagnosis window is opened, start the hazards and loading bar
    void HandleDiagnosisWindowOpened()
    {
        //loadingBarScript.StartLoading();
        loadingBarScript.perthiefTime = perthiefTime;
        startHazards();
    }

    // Actually starting the task, this shoud be called from the OS Manager
    public override void startTask()
    {
        wifiPopUpMenuWifiState.SetWifiState(ExpandedWifiMenu.WifiState.Disconnected);
    }

    public override void CompleteTask()
    {
        wifiPopUpMenuWifiState.SetWifiState(ExpandedWifiMenu.WifiState.Connected);
        stopHazards();
        base.CompleteTask();
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

    // this will request our manager to start making hazards
    public override void startHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            hazardManager.StartHazard();
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

}
