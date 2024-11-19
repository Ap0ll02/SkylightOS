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

    // Getting the loading bar script reference
    void Awake()
    {
        loadingBarScript = GetComponentInChildren<LoadingScript>();
    }

    // Starting disabled
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public static event Action OnDiagnosisWindowOpened;

    // Called when the diagnosis window is opened
    public void OpenWindow()
    {
        gameObject.SetActive(true);
        gameObject.transform.SetAsLastSibling();
        OnDiagnosisWindowOpened?.Invoke();
        StartLoadingBar();
    }

    // Starting the loading bar
    public void StartLoadingBar()
    {
        loadingBarScript.StartLoading();
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


}
