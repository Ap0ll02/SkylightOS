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

    [SerializeField] SystemResourcesWindow systemResourcesWindow;

    [SerializeField] DiagnosisWindow diagnosisWindow;

    [SerializeField] LoadingScript loadingBarScript;

    public float perTime = 1f;

    // 
    private void Awake()
    {
        systemResourcesWindow = FindObjectOfType<SystemResourcesWindow>();
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>();
        loadingBarScript = diagnosisWindow.GetComponentInChildren<LoadingScript>();
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

    }

    // Method to start the task
    public override void startTask()
    {
        systemResourcesWindow.currentRAMStatus = SystemResourcesWindow.RAMStatus.CRITICAL;
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
            if (!hazardManager.CanProgress())
            {
                diagnosisWindow.StopLoadingBar();
                break;
            }
            else
            {
                diagnosisWindow.ContinueLoadingBar();
            }
        }
    }
}
