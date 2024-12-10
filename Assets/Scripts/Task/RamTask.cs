using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Garrett Sharp
/// Ram task
/// Basically 'computer too low of ram, install more ram' type beat
/// </summary>
public class RamTask : AbstractTask
{
    // The system resources window
    [SerializeField] SystemResourcesWindow systemResourcesWindow;

    // The diagnosis window
    [SerializeField] DiagnosisWindow diagnosisWindow;

    // Shouldnt be here (performance thief shit)
    [SerializeField] LoadingScript loadingBarScript;

    // 
    public bool canProgress;

    // Reference to nord stard
    public Northstar northstar;

    // Crack (cocainia)
    public float perTime = 1f;

    // Awake my child
    private void Awake()
    {
        systemResourcesWindow = FindObjectOfType<SystemResourcesWindow>();
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>();
        loadingBarScript = diagnosisWindow.GetComponentInChildren<LoadingScript>();
        northstar = FindObjectOfType<Northstar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the task title and description
        taskTitle = "Fix Ram";
        taskDescription = "Your computer is running low on RAM, open the diagnosis window to install ram";
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        checkHazards();
    }

    // Method to start the task
    public override void startTask()
    {
        diagnosisWindow.SetHeaderText("Skylight RAM Downloader");
        systemResourcesWindow.currentRAMStatus = SystemResourcesWindow.RAMStatus.CRITICAL;
        northstar.WriteHint("Let's Diagnose This RAM Issue, Perhaps Go To The Process Manager Button Below?", Northstar.Style.warm);
    }

    public override void CompleteTask()
    {
        systemResourcesWindow.currentRAMStatus = SystemResourcesWindow.RAMStatus.OK;
        systemResourcesWindow.UpdateSystemResourcesText();
        stopHazards();
        base.CompleteTask();
    }

    // Message handler for opening the diagnosis window
    void OnEnable()
    {
        DiagnosisWindow.OnDiagnosisWindowOpened += HandleDiagnosisWindowOpened;
        DiagnosisWindow.LoadingDoneNotify += CompleteTask;
        PerformanceThiefManager.PThiefEnded += PerformanceThiefEnd;
        PerformanceThiefManager.PThiefStarted += PerformanceThiefStart;
        PerformanceThiefManager.PThiefUpdateDelay += DelayHandler;
    }

    // Removing message handler?
    void OnDisable()
    {
        DiagnosisWindow.OnDiagnosisWindowOpened -= HandleDiagnosisWindowOpened;
        DiagnosisWindow.LoadingDoneNotify -= CompleteTask;
        PerformanceThiefManager.PThiefStarted -= PerformanceThiefStart;
        PerformanceThiefManager.PThiefEnded -= PerformanceThiefEnd;
        PerformanceThiefManager.PThiefUpdateDelay -= DelayHandler;
    }

    void PerformanceThiefStart()
    {
        //loadingBarScript.perthiefTime = perTime;
    }

    void PerformanceThiefEnd()
    {
        loadingBarScript.perthiefTime = 1f;
    }

    void DelayHandler()
    {
        loadingBarScript.perthiefTime = UnityEngine.Random.Range(0.01f, 0.29f);
    }

    // When the diagnosis window is opened, start the hazards and loading bar
    void HandleDiagnosisWindowOpened()
    {
        startHazards();
        northstar.WriteHint("OH SHIT WE GOTTA WAIT FOR THE BAR TO LOAD", Northstar.Style.warm);
    }

    // This will request the manager to start a hazard
    public override void startHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            hazardManager.StartHazard();
        }
    }

    // This will request the manager to stop a hazard
    public override void stopHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            hazardManager.StopHazard();
        }
    }

    // Ask the hazard manager if our task can progress
    public override void checkHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            canProgress = hazardManager.CanProgress();
            if (!hazardManager.CanProgress())
            {
                diagnosisWindow.StopLoadingBar();
                break;
                //loadingBarScript.canContinue = false;
            }
            else
            {
                diagnosisWindow.ContinueLoadingBar();
                //loadingBarScript.canContinue = true;
            }
        }
    }
}
