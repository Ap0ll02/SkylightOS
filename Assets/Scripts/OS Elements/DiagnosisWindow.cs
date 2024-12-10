using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
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

    // Button that appears after loading bar, used for finishing the diagnosis
    [SerializeField] GameObject finishDiagButton;

    // Reference to header text component
    [SerializeField] TMP_Text headerText;

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
        finishDiagButton.SetActive(false);
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
        finishDiagButton.SetActive(true);
    }

    public void FinishDiagnosis()
    {
        window.isClosable = true;
        finishDiagButton.SetActive(false);
        LoadingDoneNotify?.Invoke();
        gameObject.SetActive(false);
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

    // Method to set the header text
    public void SetHeaderText(string text)
    {
        if (headerText != null)
        {
            headerText.text = text;
        }
    }
}
