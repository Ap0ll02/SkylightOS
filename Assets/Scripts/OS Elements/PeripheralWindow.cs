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
    public WireConnectMinigame minigame;
    public GameObject minigameWindow;

    // Messages to let the task know that we have started and finished loading
    public event Action OnLoadingStart;
    public event Action OnLoadingComplete;

    // Messages to let the task know that we have started and finished connecting
    public event Action OnConnectStart;
    public event Action OnConnectComplete;

    public enum PeripheralState
    {
        Start,
        Downloading,
        Downloaded,
        Connecting,
        Connected
    }

    public PeripheralState currentState;

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(PeripheralState.Start);
    }

    void OnEnable()
    {
        window.OnWindowOpen += StartState;
    }

    private void OnDisable()
    {
        window.OnWindowOpen -= StartState;
    }

    void StartState()
    {
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
        // Update the diagnosis window here
        FinishDownload();
    }

    public void StartConnectMinigame()
    {
        UpdateState(PeripheralState.Connecting);
        OnConnectStart?.Invoke();
        StartCoroutine(CheckMinigameDone());
    }

    public void FinishConnectMinigame()
    {
        UpdateState(PeripheralState.Connected);
        OnConnectComplete?.Invoke();
    }

    private IEnumerator CheckMinigameDone()
    {
        while(minigame.isStarted)
        {
            yield return null;
        }
        FinishConnectMinigame();
    }


    private void UpdateState(PeripheralState newState)
    {
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
}
