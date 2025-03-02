using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirusMain : MonoBehaviour
{
    // Messages to let the task know that we have started and finished loading
    public event Action OnLoadingStart;
    public event Action OnLoadingComplete;

    public GameObject scanBar;
    public LoadingScript loadingScript;
    public GameObject scanButton;

    public GameObject infectedText;
    public GameObject removeVirusButton;

    // Prefab to spawn
    public GameObject prefabToSpawn;

    // Canvas
    public Canvas windowCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnScanButton()
    {
        OnLoadingStart?.Invoke();
        loadingScript.StartLoading();
        StartCoroutine(CheckLoadingDone());
    }

    public IEnumerator CheckLoadingDone()
    {
        while (!loadingScript.isLoaded)
        {
            yield return null;
        }
        CompleteLoading();
    }

    public void CompleteLoading()
    {
        OnLoadingComplete?.Invoke();
        SetStatus(AntiVirusWindow.AntiVirusState.InstalledRan);
        
    }

    public void OnRemoveButton()
    {
        SpawnPrefab();
    }

    public void SetStatus(AntiVirusWindow.AntiVirusState state)
    {
        switch (state)
        {
            case AntiVirusWindow.AntiVirusState.Installed:
                scanBar.SetActive(true);
                scanButton.SetActive(true);
                infectedText.SetActive(false);
                removeVirusButton.SetActive(false);
                break;
            case AntiVirusWindow.AntiVirusState.InstalledRan:
                scanBar.SetActive(false);
                scanButton.SetActive(false);
                infectedText.SetActive(true);
                removeVirusButton.SetActive(true);
                break;
        }
    }


    public void SpawnPrefab()
    {
        if (prefabToSpawn != null && windowCanvas != null)
        {
            Instantiate(prefabToSpawn, windowCanvas.transform);
        }
    }
}
