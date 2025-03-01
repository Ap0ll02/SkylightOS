using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirusWindow : MonoBehaviour
{
    public BugSmashGame bugSmashGame;

    private BasicWindow window;

    public BasicWindowPanel wizardPanel;

    public BasicWindowPanel antiVirusPanel;

    public bool isInstalled = false;

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
    }

    void OnEnable()
    {
        window.OnWindowOpen += OnWindowOpen;
        bugSmashGame.BugSmashGameEndNotify += OnGameComplete;
    }

    void OnDisable()
    {
        window.OnWindowOpen -= OnWindowOpen;
        bugSmashGame.BugSmashGameEndNotify -= OnGameComplete;
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

    void OnGameComplete()
    {
        isInstalled = true;
        wizardPanel.ClosePanel();
        antiVirusPanel.OpenPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
