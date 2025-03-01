using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirusWizard : MonoBehaviour
{
    public GameObject downloadButton;

    public BugSmashGame bugSmashGame;

    public GameObject installText;

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
        bugSmashGame.TryStartGame();
    }
}
