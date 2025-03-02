using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirusTask : AbstractTask
{
    public AntiVirusWindow antiVirusWindow;


    void Awake()
    {
        taskTitle = "Fix and run antivirus";
        taskDescription = "The download of the antivirus is riddled with bugs. Use the desktop icon to access the wizard.";
        antiVirusWindow = FindObjectOfType<AntiVirusWindow>();
    }

    // Start is called before the first frame update
    new void Start()
    {
        antiVirusWindow.SetStatus(AntiVirusWindow.AntiVirusState.NeedsInstall);
    }

    public override void startTask()
    {
        startHazards();
        antiVirusWindow.SetStatus(AntiVirusWindow.AntiVirusState.NeedsInstallInteractable);
    }

    public override void CompleteTask()
    {
        stopHazards();
        base.CompleteTask();
    }

    public void OnEnable()
    {
        antiVirusWindow.OnLoadingStart += OnLoadingBegin;
        antiVirusWindow.OnLoadingComplete += OnLoadingComplete;
        antiVirusWindow.OnMinigameStart += OnGameStart;
        antiVirusWindow.OnMinigameComplete += OnGameComplete;
    }

    public void OnDisable()
    {
        antiVirusWindow.OnLoadingStart -= OnLoadingBegin;
        antiVirusWindow.OnLoadingComplete -= OnLoadingComplete;
        antiVirusWindow.OnMinigameStart -= OnGameStart;
        antiVirusWindow.OnMinigameComplete -= OnGameComplete;
    }

    public void OnGameStart()
    {
        
    }

    public void OnGameComplete()
    {
        antiVirusWindow.SetStatus(AntiVirusWindow.AntiVirusState.Installed);    
    }

    public void OnLoadingBegin()
    {

    }

    public void OnLoadingComplete()
    {
        antiVirusWindow.SetStatus(AntiVirusWindow.AntiVirusState.InstalledRan);
        CompleteTask();
    }

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

    public override void checkHazards()
    {
        // Not implemented ;)
    }

}
