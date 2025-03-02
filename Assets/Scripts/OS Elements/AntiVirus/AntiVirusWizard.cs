using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirusWizard : MonoBehaviour
{
    public GameObject downloadButton;

    public BugSmashGame bugSmashGame;

    public GameObject installText;

    // Messages to let the task know that we have started and finished loading
    public event Action OnMinigameStart;
    public event Action OnMinigameComplete;

    public enum WizardStatus
    {
        NotDownloaded,
        Downloaded,
        NeedsInstall,
        NeedsInstallInteractable

    }

    void Awake()
    {
        bugSmashGame = FindObjectOfType<BugSmashGame>();
        SetStatus(WizardStatus.NotDownloaded);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        bugSmashGame.BugSmashGameEndWinNotify += GameFinish;
    }

    public void OnDisable()
    {
        bugSmashGame.BugSmashGameEndWinNotify -= GameFinish;
    }

    public void SetStatus(WizardStatus status)
    {
        switch (status)
        {
            case WizardStatus.NotDownloaded:
                downloadButton.SetActive(false);
                installText.SetActive(false);
                break;
            case WizardStatus.Downloaded:
                downloadButton.SetActive(false);
                installText.SetActive(false);
                break;
            case WizardStatus.NeedsInstall:
                downloadButton.SetActive(false);
                installText.SetActive(false);
                break;
            case WizardStatus.NeedsInstallInteractable:
                downloadButton.SetActive(true);
                installText.SetActive(true);
                break;
        }
    }

    public void TryStartGame()
    {
        if(!bugSmashGame.playingGame)
        {
            bugSmashGame.TryStartGame();
            OnMinigameStart?.Invoke();
        }
        else
        {
            Debug.Log("Game already running");
        }
    }

    public void GameFinish()
    {
        OnMinigameComplete?.Invoke();
    }
}
