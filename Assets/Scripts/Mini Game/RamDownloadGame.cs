using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        window = GetComponent<BasicWindow>();
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

    //
    private void Start()
    {
        window.CloseWindow();
    }

    // On enable
    void OnEnable()
    {
        window.OnWindowOpen += StartGame;
    }

    // On disable
    void OnDisable()
    {
        window.OnWindowOpen -= StartGame;
    }

    public override void StartGame()
    {
        StartSpawningWindows();
    }

    public void EndGame()
    {
        StopAllCoroutines();
        window.CloseWindow();
    }

    // Start spawning windows
    public void StartSpawningWindows()
    {
        StartCoroutine(SpawnWindows());
    }

    // Spawn windows
    IEnumerator SpawnWindows()
    {
        while (downloadCounter < 5)
        {
            SpawnNewWindow();
            yield return new WaitForSeconds(5);
        }
    }

    // Spawn a new window outside the current window area
    void SpawnNewWindow()
    {
        Vector3 spawnPosition = GetRandomPositionOutsideWindow();
        GameObject newWindow = Instantiate(windowPrefab, spawnPosition, Quaternion.identity);
        newWindow.transform.SetParent(windowCanvas.transform, false);
    }

    // Get a random position outside the current window area but within the screen bounds
    Vector3 GetRandomPositionOutsideWindow()
    {
        Vector3 windowPosition = window.transform.position;
        Vector3 windowSize = window.GetComponent<RectTransform>().sizeDelta;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float x, y;

        do
        {
            x = Random.Range(0, screenWidth);
            y = Random.Range(0, screenHeight);
        } while (x > windowPosition.x - windowSize.x / 2 && x < windowPosition.x + windowSize.x / 2 &&
                 y > windowPosition.y - windowSize.y / 2 && y < windowPosition.y + windowSize.y / 2);

        return new Vector3(x, y, 0);
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Downloadable"))
        {
            downloadCounter++;
            Destroy(other.gameObject);
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
}
