using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// Garrett Sharp
/// Popup Manager
/// Give it the list of popup prefabs, and then assign the tasks the reference to the manager.
/// </summary>
public class PopupManager : AbstractManager
{
    // The window prefabs that can be spawned
    public List<GameObject> popupList = new List<GameObject>();

    // The list of spawned windows
    public List<GameObject> spawnedWindows = new List<GameObject>();

    // Max windows
    public int maxWindows;

    // Minumum spawn interval, so atleast 3 'secs' (idk if its actually seconds) between spawns
    public int spawnIntervalMin;

    // Maximum spawn interval
    public int spawnIntervalMax;

    // The range of the spawn position
    public float xRangeMin = -5f;
    public float xRangeMax = 5f;
    public float yRangeMin = -3f;
    public float yRangeMax = 3f;

    // The canvas to spawn the windows on
    public Canvas canvas;

    // 
    public static event Action PopupCanContinue;
    public static event Action PopupCantContinue;

    // Getting rid of disabled windows.
    // Calling this method in update is kinda stupid will probably change later
    public void Update()
    {
        CleanUpDisabledWindows();
    }

    // Starts the hazard, to be called by the task
    public override void StartHazard()
    {
        Debug.Log("Starting Popup Manager");
        UpdateSpawnInterval();
        StartCoroutine(SpawnWindow());
        StartCoroutine(CheckWindows());
    }

    IEnumerator CheckWindows()
    {
        while (true)
        {
            if (CanProgress())
            {
                PopupCanContinue?.Invoke();
            }
            else
            {
                PopupCantContinue?.Invoke();
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    // Spawns the window based off of the IEnumerator
    IEnumerator SpawnWindow()
    {
        while (true)
        {
            int spawnInterval = UnityEngine.Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);
            SpawnWindow(UnityEngine.Random.Range(0, popupList.Count));
        }
    }

    // Stops the hazard, to be called by task
    public override void StopHazard()
    {
        Debug.Log("Stopping Popup Manager");
        PopupCanContinue?.Invoke();
        StopAllCoroutines();
    }

    // Checks if any windows are spawned, called by task
    public override bool CanProgress()
    {
        if (spawnedWindows.Count == 0)
        {
            return true;
        }
        return false;
    }

    public void SpawnWindow(int index)
    {
        if (spawnedWindows.Count < maxWindows)
        {
            GameObject newWindow = Instantiate(popupList[index], transform);
            spawnedWindows.Add(newWindow);
            newWindow.transform.SetParent(canvas.transform, false);
            newWindow.transform.SetAsLastSibling();
            newWindow.transform.position = GetSpawnPosition();

            Debug.Log("Spawned Popup at " + newWindow.transform.position.ToString());
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float randomX = UnityEngine.Random.Range(xRangeMin, xRangeMax);
        float randomY = UnityEngine.Random.Range(yRangeMin, yRangeMax);
        return new Vector3(randomX, randomY, 0);
    }

    private void CleanUpDisabledWindows()
    {
        for (int i = spawnedWindows.Count - 1; i >= 0; i--)
        {
            if (!spawnedWindows[i].activeSelf)
            {
                Destroy(spawnedWindows[i]);
                spawnedWindows.RemoveAt(i);
            }
        }
    }

    private void UpdateSpawnInterval()
    {
        if (difficulty == OSManager.Difficulty.Easy)
        {
            spawnIntervalMin = 6;
            spawnIntervalMax = 13;
            maxWindows = 3;
        }
        else if (difficulty == OSManager.Difficulty.Medium)
        {
            spawnIntervalMin = 5;
            spawnIntervalMax = 10;
            maxWindows = 5;
        }
        else if (difficulty == OSManager.Difficulty.Hard)
        {
            spawnIntervalMin = 4;
            spawnIntervalMax = 8;
            maxWindows = 7;
        }
    }

}
