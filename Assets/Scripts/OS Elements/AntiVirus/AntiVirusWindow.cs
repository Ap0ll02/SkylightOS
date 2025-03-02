using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirusWindow : MonoBehaviour
{
    // Messages to let the task know that we have started and finished loading
    public event Action OnLoadingStart;
    public event Action OnLoadingComplete;

    // Messages to let the task know that we have started and finished Minigaming
    public event Action OnMinigameStart;
    public event Action OnMinigameComplete;

    public BugSmashGame bugSmashGame;

    private BasicWindow window;

    public BasicWindowPanel wizardPanel;

    public BasicWindowPanel antiVirusPanel;

    public AntiVirusWizard antiVirusWizard;

    public AntiVirusMain antiVirusMain;

    public bool isInstalled = false;

    public enum AntiVirusState
    {
        NotDownloaded,
        Downloaded,
        NeedsInstall,
        NeedsInstallInteractable,
        Installed,
        InstalledRan
    }

    void Awake()
    {
        bugSmashGame = FindObjectOfType<BugSmashGame>();
        window = GetComponent<BasicWindow>();
        wizardPanel.ClosePanel();
        antiVirusPanel.ClosePanel();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnWindowOpen();
        window.ForceCloseWindow();
    }

    void OnEnable()
    {
        window.OnWindowOpen += OnWindowOpen;
        antiVirusWizard.OnMinigameStart += OnGameStart;
        antiVirusWizard.OnMinigameComplete += OnGameComplete;
        antiVirusMain.OnLoadingStart += OnLoadingBegin;
        antiVirusMain.OnLoadingComplete += OnLoadingFinish;
    }

    void OnDisable()
    {
        window.OnWindowOpen -= OnWindowOpen;
        antiVirusWizard.OnMinigameStart -= OnGameStart;
        antiVirusWizard.OnMinigameComplete -= OnGameComplete;
        antiVirusMain.OnLoadingStart -= OnLoadingBegin;
        antiVirusMain.OnLoadingComplete -= OnLoadingFinish;
    }

    void OnWindowOpen()
    {
        if (isInstalled)
        {
            antiVirusPanel.OpenPanel();
        }
        else
        {
            wizardPanel.OpenPanel();
        }
    }

    void OnGameStart()
    {
        OnMinigameStart?.Invoke();
        window.isClosable = false;
    }

    void OnGameComplete()
    {
        isInstalled = true;
        OnMinigameComplete?.Invoke();
        wizardPanel.ClosePanel();
        antiVirusPanel.OpenPanel();
    }

    void OnLoadingBegin()
    {
        OnLoadingStart?.Invoke();
    }

    void OnLoadingFinish()
    {
        OnLoadingComplete?.Invoke();
        window.isClosable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStatus(AntiVirusState state)
    {
        if (!isInstalled)
        {
            switch (state)
            {
                case(AntiVirusState.NotDownloaded):
                    // get rid of icon or something idk
                    break;
                case (AntiVirusState.Downloaded):
                    antiVirusWizard.SetStatus(AntiVirusWizard.WizardStatus.Downloaded);
                    break;
                case (AntiVirusState.NeedsInstall):
                    antiVirusWizard.SetStatus(AntiVirusWizard.WizardStatus.NeedsInstall);
                    break;
                case (AntiVirusState.NeedsInstallInteractable):
                    antiVirusWizard.SetStatus(AntiVirusWizard.WizardStatus.NeedsInstallInteractable);
                    break;
            }
        }
        else
        {
            antiVirusMain.SetStatus(state);
        }

    }
}
