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
        antiVirusWindow.SetStatus(AntiVirusWindow.AntiVirusState.NeedsInstallInteractable);
    }

    public override void CompleteTask()
    {
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
        throw new System.NotImplementedException();
    }

    public override void stopHazards()
    {
        throw new System.NotImplementedException();
    }

    public override void checkHazards()
    {
        throw new System.NotImplementedException();
    }

}
