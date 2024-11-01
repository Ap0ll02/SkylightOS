using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : AbstractManager
{
    // The window prefabs that can be spawned
    public List<GameObject> popupList = new List<GameObject>();

    // The list of spawned windows
    public List<GameObject> spawnedWindows = new List<GameObject>();

    public int maxWindows = 5;

    public int spawnIntervalMin = 3;
    
    public int spawnIntervalMax = 10;

    // The range of the spawn position
    public float xRangeMin = -5f;
    public float xRangeMax = 5f;
    public float yRangeMin = -3f;
    public float yRangeMax = 3f;

    // The canvas to spawn the windows on
    public Canvas canvas;

    public void Update()
    {
        CleanUpDisabledWindows();
    }

    public override void StartHazard()
    {
        Debug.Log("Starting Popup Manager");
        StartCoroutine(SpawnWindow());
    }

    IEnumerator SpawnWindow()
    {
        while (true)
        {
            int spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);
            SpawnWindow(Random.Range(0, popupList.Count));
        }
    }

    public override void StopHazard()
    {
        Debug.Log("Stopping Popup Manager");
        StopAllCoroutines();
    }

    public override bool CanProgress()
    {
        if(spawnedWindows.Count == 0)
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
        float randomX = Random.Range(xRangeMin, xRangeMax);
        float randomY = Random.Range(yRangeMin, yRangeMax);
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
}
