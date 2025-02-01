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

    // Reference to the BoxCollider2D
    [SerializeField] BoxCollider2D boxCollider;

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

    private void Start()
    {
        window.CloseWindow();
        boxCollider.enabled = false; // Ensure the collider is initially disabled
    }

    // On enable
    void OnEnable()
    {
        window.OnWindowOpen += StartGame;
        window.OnWindowOpen += EnableCollider;
        window.OnWindowClose += DisableCollider;
    }

    // On disable
    void OnDisable()
    {
        window.OnWindowOpen -= StartGame;
        window.OnWindowOpen -= EnableCollider;
        window.OnWindowClose -= DisableCollider;
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

    // Spawn a new window within the canvas area but outside the current window
    void SpawnNewWindow()
    {
        Vector3 spawnPosition = GetRandomPositionInsideCanvas();
        if (!IsOverlappingCurrentWindow(spawnPosition))
        {
            GameObject newWindow = Instantiate(windowPrefab, spawnPosition, Quaternion.identity);
            newWindow.transform.SetParent(windowCanvas.transform, false);
            newWindow.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
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

        float canvasWidth = canvasRect.rect.width * canvasRect.localScale.x;
        float canvasHeight = canvasRect.rect.height * canvasRect.localScale.y;

        float x = Random.Range(0, canvasWidth);
        float y = Random.Range(0, canvasHeight);
         
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
