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
    public Northstar northstar;

    public float perTime = 1f;

    // Initialization
    public void Awake()
    {
        // Set the task title and description
        taskTitle = "Fix wifi";
        taskDescription = "Connect to the wifi by opening the wifi menu and connecting to the wifi";
        // Assigning all of the references must be done on awake so that they actually work
        wifiPopUpMenu = FindObjectOfType<ExpandedWifiMenu>().gameObject;
        wifiPopUpMenuWifiState = wifiPopUpMenu.GetComponent<ExpandedWifiMenu>();
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>().gameObject;
        loadingBarScript = diagnosisWindow.GetComponentInChildren<LoadingScript>();
        northstar = GameObject.Find("WindowCanvas").GetComponentInChildren<Northstar>();
    }

    public new void Start()
    {
        // Automatically turn off the game object at the start of the scene.
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        checkHazards();
    }

    // Message handler for opening the diagnosis window
    public void OnEnable()
    {
        DiagnosisWindow.OnDiagnosisWindowOpened += HandleDiagnosisWindowOpened;
        DiagnosisWindow.LoadingDoneNotify += CompleteTask;
        PerformanceThiefManager.PThiefEnded += PerformanceThiefEnd;
        PerformanceThiefManager.PThiefStarted += PerformanceThiefStart;
        PerformanceThiefManager.PThiefUpdateDelay += DelayHandler;
    }

    // Removing message handler?
    public void OnDisable()
    {
        DiagnosisWindow.OnDiagnosisWindowOpened -= HandleDiagnosisWindowOpened;
        DiagnosisWindow.LoadingDoneNotify -= CompleteTask;
        PerformanceThiefManager.PThiefStarted -= PerformanceThiefStart;
        PerformanceThiefManager.PThiefEnded -= PerformanceThiefEnd;
        PerformanceThiefManager.PThiefUpdateDelay -= DelayHandler;
    }

    void DelayHandler() {
        loadingBarScript.perthiefTime = UnityEngine.Random.Range(0.01f, 0.29f);
    }

    // When the diagnosis window is opened, start the hazards and loading bar
    void HandleDiagnosisWindowOpened()
    {
        //loadingBarScript.StartLoading();
        //loadingBarScript.perthiefTime = perTime;
        startHazards();
    }

    void PerformanceThiefStart() {
        //loadingBarScript.perthiefTime = perTime;
    }

    void PerformanceThiefEnd() {
        loadingBarScript.perthiefTime = 1f;
    }

    // Actually starting the task, this should be called from the OS Manager
    public override void startTask()
    {
        wifiPopUpMenuWifiState.SetWifiState(ExpandedWifiMenu.WifiState.Disconnected);
        northstar.WriteHint("Let's Diagnose This Wifi Issue, Perhaps Go To The Button Below?", Northstar.Style.hot);
    }

    public override void CompleteTask()
    {
        wifiPopUpMenuWifiState.SetWifiState(ExpandedWifiMenu.WifiState.Connected);
        stopHazards();
        base.CompleteTask();
        // TODO: Note, this line below is the only way input works for the next task
        // I have no idea why yet.
        gameObject.SetActive(false);
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
                break;
            }
            else
            {
                //loadingBarScript.perthiefTime = perTime;
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
