using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Garrett Sharp
/// Diagnosis window
/// Script currently only needed so that reference can be grabbed.
/// Any unique behavior that is not like a window should be implemented here though
/// </summary>
public class DiagnosisWindow : MonoBehaviour
{
    // Loading bar script reference
    [SerializeField] LoadingScript loadingBarScript;

    // Reference to window script
    [SerializeField] BasicWindow window;

    // Getting the loading bar script reference
    void Awake()
    {
        loadingBarScript = GetComponentInChildren<LoadingScript>();
        window = GetComponent<BasicWindow>();
    }

    // Starting disabled
    void Start()
    {
        gameObject.SetActive(false);
    }

    public static event Action OnDiagnosisWindowOpened;

    // Called when the diagnosis window is opened
    public void OpenWindow()
    {
        gameObject.SetActive(true);
        gameObject.transform.SetAsLastSibling();
        OnDiagnosisWindowOpened?.Invoke();
        window.isClosable = false;
        StartLoadingBar();
    }

    public void OnDisable()
    {
        ResetLoadingBar();
        gameObject.SetActive(false);
    }
    // Starting the loading bar
    public void StartLoadingBar()
    {
        loadingBarScript.StartLoading();
        StartCoroutine(UpdateDiagnosisWindow());
    }

    // Continuing the loading bar
    public void ContinueLoadingBar()
    {
        loadingBarScript.canContinue = true;
    }

    // Stopping the loading bar
    public void StopLoadingBar()
    {
        loadingBarScript.canContinue = false;
    }

    public static event Action LoadingDoneNotify;

    public void LoadingDone()
    {
        Debug.Log("LoadingDone");
        window.isClosable = true;
        LoadingDoneNotify?.Invoke();
    }



    // Coroutine to update the diagnosis window when the loading bar is loaded
    private IEnumerator UpdateDiagnosisWindow()
    {
        while (!loadingBarScript.isLoaded)
        {
            yield return null;
        }
        // Update the diagnosis window here
        LoadingDone();
    }

    public void ResetLoadingBar()
    {
        loadingBarScript.Reset();
    }
}
