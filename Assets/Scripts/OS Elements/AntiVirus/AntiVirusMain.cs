using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Garrett Shart
// Fuckass antivirus script
// Idk dawg just read this shit

public class AntiVirusMain : MonoBehaviour
{
    // Messages to let the task know that we have started and finished loading
    public event Action OnLoadingStart;
    public event Action OnLoadingComplete;

    public GameObject scanBar;
    public LoadingScript loadingScript;
    public GameObject scanButton;

    public GameObject infectedText;
    public TMP_Text infectedTextStr;
    public GameObject removeVirusButton;

    public string numViruses;
    // Prefab to spawn
    public GameObject prefabToSpawn;

    // Canvas
    public Canvas windowCanvas;

    // Popup window count
    private int popupCount = 0;
    private int maxPopups = 8;

    public BasicWindow evidenceWindow;

    // Start is called before the first frame update
    void Start()
    {
        numViruses = UnityEngine.Random.Range(0, 1000000).ToString();
    }


    public void OnScanButton()
    {
        scanButton.SetActive(false);
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
        if(popupCount == maxPopups)
        {
            popupCount++;
            evidenceWindow.OpenWindow();
        }
        else
        {
            popupCount++;
            SpawnPrefab();
        }

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
                infectedTextStr.text = "Your system is infected!\r\nThere is " + numViruses + " Malicious Programs!";
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
            GameObject instance = Instantiate(prefabToSpawn, windowCanvas.transform);
            RectTransform canvasRect = windowCanvas.GetComponent<RectTransform>();
            RectTransform instanceRect = instance.GetComponent<RectTransform>();

            float randomX = UnityEngine.Random.Range(0, canvasRect.rect.width) - canvasRect.rect.width / 2;
            float randomY = UnityEngine.Random.Range(0, canvasRect.rect.height) - canvasRect.rect.height / 2;

            instanceRect.anchoredPosition = new Vector2(randomX, randomY);
        }
    }
}
