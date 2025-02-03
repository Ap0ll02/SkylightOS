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

    // The minigame window
    [SerializeField] RamDownloadGame minigameWindow;

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
        minigameWindow = FindObjectOfType<RamDownloadGame>();
        northstar = FindObjectOfType<Northstar>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        // Set the task title and description
        taskTitle = "Fix Ram";
        taskDescription = "Your computer is running low on RAM, check the resources window to view the problem";
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
        systemResourcesWindow.currentRAMStatus = SystemResourcesWindow.RAMStatus.CRITICAL;
        northstar.WriteHint("Let's Diagnose This RAM Issue, Perhaps Go To The Process Manager Button Below?", Northstar.Style.warm);
        systemResourcesWindow.UpdateSystemResourcesText();
    }

    public override void CompleteTask()
    {
        systemResourcesWindow.currentRAMStatus = SystemResourcesWindow.RAMStatus.OK;
        systemResourcesWindow.UpdateSystemResourcesText();
        northstar.WriteHint("We did the ram!!!!!", Northstar.Style.warm);
        stopHazards();
        base.CompleteTask();
    }

    // Message handler for opening the diagnosis window
    void OnEnable()
    {
        RamDownloadGame.RamMinigameStartNotify += HandleMinigameStarted;
        RamDownloadGame.RamMinigameEndNotify += HandleMinigameEnded;
    }

    // Removing message handler?
    void OnDisable()
    {
        RamDownloadGame.RamMinigameStartNotify -= HandleMinigameStarted;
        RamDownloadGame.RamMinigameEndNotify -= HandleMinigameEnded;
    }


    // When the diagnosis window is opened, start the hazards and loading bar
    void HandleMinigameStarted()
    {
        startHazards();
        northstar.WriteHint("OH SHIT WE GOTTA PUT THE RAM IN THE RAM SLOTS", Northstar.Style.warm);
    }

    void HandleMinigameEnded()
    {
        CompleteTask();
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
                break;
                //loadingBarScript.canContinue = false;
            }
            else
            {
                //loadingBarScript.canContinue = true;
            }
        }
    }
}
