using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Garrett Shart
/// Basically just a window that has a download button and a connect button
/// The download button will start a loading bar, and the connect button will start a mini game
/// currently 2am and I'm tired
/// </summary>
public class PeripheralWindow : MonoBehaviour
{
    // Reference to da window
    public BasicWindow window;

    // Button references
    public GameObject downloadButton;
    public GameObject connectButton;

    // loading bar references
    public GameObject loadingBar;
    public LoadingScript loadingScript;

    // Minigame reference
    public GameObject minigameWindow;
    public WireConnectMinigame wireConnectMinigame;

    // Messages to let the task know that we have started and finished loading
    public event Action OnLoadingStart;
    public event Action OnLoadingComplete;

    // Messages to let the task know that we have started and finished connecting
    public event Action OnConnectStart;
    public event Action OnConnectComplete;

    private bool alreadyOpened;
    public enum PeripheralState
    {
        Start,
        Downloading,
        Downloaded,
        Connecting,
        Connected
    }

    public PeripheralState currentState;

    public void Awake()
    {
        window = GetComponent<BasicWindow>();
        wireConnectMinigame = FindObjectOfType<WireConnectMinigame>();
        alreadyOpened = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        window.CloseWindow();
        UpdateState(PeripheralState.Start);
    }

    void StartState()
    {
        window.isClosable = false;
        UpdateState(PeripheralState.Start);
        loadingScript.Reset();
    }

    public void StartDownload()
    {
        UpdateState(PeripheralState.Downloading);
        OnLoadingStart?.Invoke();
        loadingScript.StartLoading();
        StartCoroutine(CheckLoadingDone());
    }
    private void FinishDownload()
    {
        StopAllCoroutines();
        UpdateState(PeripheralState.Downloaded);
        OnLoadingComplete?.Invoke();
    }

    private IEnumerator CheckLoadingDone()
    {

        while (!loadingScript.isLoaded)
        {
            yield return null;
        }
        FinishDownload();
    }

    public void StartConnectMinigame()
    {
        UpdateState(PeripheralState.Connecting);
        OnConnectStart?.Invoke();
        StartCoroutine(CheckMinigameDone());
        wireConnectMinigame.TryStartGame();
    }

    public void FinishConnectMinigame()
    {
        UpdateState(PeripheralState.Connected);
        OnConnectComplete?.Invoke();
        window.isClosable = true;
    }

    private IEnumerator CheckMinigameDone()
    {
        while(!wireConnectMinigame.isComplete)
        {
            yield return null;
        }
        FinishConnectMinigame();
    }


    private void UpdateState(PeripheralState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case PeripheralState.Start:
                downloadButton.SetActive(true);
                connectButton.SetActive(false);
                loadingBar.SetActive(false);
                break;
            case PeripheralState.Downloading:
                downloadButton.SetActive(false);
                connectButton.SetActive(false);
                loadingBar.SetActive(true);
                break;
            case PeripheralState.Downloaded:
                downloadButton.SetActive(false);
                connectButton.SetActive(true);
                loadingBar.SetActive(false);
                break;
            case PeripheralState.Connecting:
                downloadButton.SetActive(false);
                connectButton.SetActive(false);
                loadingBar.SetActive(false);
                break;
            case PeripheralState.Connected:
                downloadButton.SetActive(false);
                connectButton.SetActive(false);
                loadingBar.SetActive(false);
                break;
            default:
                Debug.LogError("You broke it you fuck fuck fuck sandwich fuckhead fuck shit fuck");
                break;
        }
    }

    public void TryOpenWindow()
    {
        if(!alreadyOpened)
        {
            alreadyOpened = true;
            window.OpenWindow();
            StartState();
        }    
        else
        {
            Debug.Log("This shit already opened bruh wtf wrong with u");
        }
    }
}
