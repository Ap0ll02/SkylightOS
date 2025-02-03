using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RamDownloadGame : AbstractMinigame
{
    // Reference to the RamDownloadGame window
    [SerializeField] BasicWindow window;

    // Counter for downloads
    public int downloadCounter = 0;

    // Reference to the window prefab
    [SerializeField] GameObject windowPrefab;

    // Reference to the window canvas
    [SerializeField] Canvas windowCanvas;

    // Reference to the BoxCollider2D
    [SerializeField] BoxCollider2D boxCollider;

    // The action that will notify the task that the minigame has started
    public static event Action RamMinigameStartNotify;

    // The action that will notify the task that the minigame is done
    public static event Action RamMinigameEndNotify;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        window = GetComponent<BasicWindow>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Find the object with the name "WindowCanvas" and get the Canvas component
        GameObject canvasObject = GameObject.Find("WindowCanvas");
        if (canvasObject != null)
        {
            windowCanvas = canvasObject.GetComponent<Canvas>();
        }
        else
        {
            Debug.LogError("WindowCanvas not found");
        }
    }

    // Start to set values and make sure the window is closed
    private void Start()
    {
        window.CloseWindow();
        boxCollider.enabled = false; // Ensure the collider is initially disabled
    }

    // On enable
    void OnEnable()
    {
        window.OnWindowOpen += EnableCollider;
        window.OnWindowClose += DisableCollider;
    }

    // On disable
    void OnDisable()
    {
        window.OnWindowOpen -= EnableCollider;
        window.OnWindowClose -= DisableCollider;
    }

    // Basically make sure that we cant restart the Minigame
    public void tryStartGame()
    {
        if (!isStarted)
            StartGame();
        else
            Debug.Log("Game already started, you trying to break it or something?");
    }

    // Starts the game
    public override void StartGame()
    {
        RamMinigameStartNotify?.Invoke();
        isStarted = true;
        Debug.Log("Ram Minigame Started");
        window.OpenWindow();
        StartSpawningWindows();
    }

    // What to happen when the game is ending
    public void EndGame()
    {
        isStarted = false;
        StopAllCoroutines();
        window.CloseWindow();
        RamMinigameEndNotify?.Invoke();
    }

    // Start spawning windows
    public void StartSpawningWindows()
    {
        StartCoroutine(SpawnWindows());
    }

    // Spawn windows
    IEnumerator SpawnWindows()
    {
        while (downloadCounter <= 5)
        {
            SpawnNewWindow();
            int spawntime = UnityEngine.Random.Range(1, 10);
            yield return new WaitForSeconds(spawntime);
        }
    }

    // Spawn a new window within the canvas area but outside the current window
    void SpawnNewWindow()
    {
        Vector3 spawnPosition = GetRandomPositionInsideCanvas();
        if (!IsOverlappingCurrentWindow(spawnPosition))
        {
            GameObject newWindow = Instantiate(windowPrefab, spawnPosition, Quaternion.identity);
            newWindow.transform.SetParent(windowCanvas.transform, false);
            newWindow.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
            newWindow.GetComponent<RectTransform>().position = spawnPosition;
        }
    }

    // Check if the position is overlapping with the current window
    bool IsOverlappingCurrentWindow(Vector3 position)
    {
        RectTransform windowRect = window.GetComponent<RectTransform>();
        Rect windowBounds = new Rect(windowRect.anchoredPosition, windowRect.sizeDelta);

        return windowBounds.Contains(position);
    }

    // Get a random position within the canvas area
    Vector3 GetRandomPositionInsideCanvas()
    {
        RectTransform canvasRect = windowCanvas.GetComponent<RectTransform>();
        RectTransform windowRect = windowPrefab.GetComponent<RectTransform>();

        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        float windowWidth = windowRect.rect.width;
        float windowHeight = windowRect.rect.height;

        // Get the local scale of the canvas
        Vector3 canvasScale = canvasRect.localScale;

        // Calculate the actual width and height considering the scale
        float actualWidth = canvasWidth * canvasScale.x;
        float actualHeight = canvasHeight * canvasScale.y;

        // Get the pivot point of the canvas
        Vector2 pivot = canvasRect.pivot;

        // Calculate the offset based on the pivot
        float xOffset = actualWidth * pivot.x;
        float yOffset = actualHeight * pivot.y;

        // Generate random positions within the canvas bounds, considering the window size
        float x = UnityEngine.Random.Range(-xOffset + windowWidth / 2, actualWidth - xOffset - windowWidth / 2);
        float y = UnityEngine.Random.Range(-yOffset + windowHeight / 2, actualHeight - yOffset - windowHeight / 2);

        return new Vector3(x, y, 0);
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Downloadable"))
        {
            Debug.Log("RamDownloaded");
            downloadCounter++;
            Destroy(other.gameObject);
            OnFileDownloaded();
        }
    }

    // When a file gets downloaded, check win condition
    public void OnFileDownloaded()
    {
        if (downloadCounter > 5)
        {
            EndGame();
        }
    }

    // Enable the BoxCollider2D
    void EnableCollider()
    {
        boxCollider.enabled = true;
    }

    // Disable the BoxCollider2D
    void DisableCollider()
    {
        boxCollider.enabled = false;
    }
}
