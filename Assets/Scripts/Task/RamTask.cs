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

    // 
    private void Awake()
    {
        systemResourcesWindow = FindObjectOfType<SystemResourcesWindow>();
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        startHazards();
    }

    // This will request the manager to start a hazard
    public override void startHazards()
    {
        
    }

    // This will request the manager to stop a hazard
    public override void stopHazards()
    {
        
    }

    // Ask the hazard manager if our task can progress
    public override void checkHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            if (!hazardManager.CanProgress())
            {
                
            }
            else
            {

            }
        }
    }
}
