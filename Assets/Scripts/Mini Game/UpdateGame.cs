
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; // Aliased Unity's Random for convenience.

public class UpdateGame : MonoBehaviour
{
    // Array of mail GameObject prefabs, which will be spawned during gameplay.
    public GameObject[] mail;

    // Reference to the BasicWindow component used to manage opening and closing game menus.
    public BasicWindow window;

    // Reference to the main camera of the scene, crucial for raycasting and player positioning.
    public Camera mainCamera;

    // Bounding area in the game scene for confining elements within a virtual space.
    //public Rect windowBounds = new Rect(-2166, -3000, 4332, 6000);
    public GameObject updateWindow;
    // THIS IS VERY VERY IMPORTANT IS GUIDES WHERE THINGS SPAWN VIA THE WINDOW DIMENSIONS
    public Rect windowArea;

    // This is so we can deactivate the ethernet cable after the game ends
    public GameObject player;
    // Reference to the player RectTransform, used to set movement and positioning.
    public RectTransform playerRectTransform;

    // Reference to an UpdateGameScoreManager, which will manage scoring logic (e.g., wins/losses).
    public UpdateGameScoreManager scoreManager;

    // Reference to UpdatePanel used for controlling the game's UI graphics state.
    // Game state tracking variables.
    public bool isGameOver = false; // Whether the game is over.
    public bool playingGame = false; // Whether the game is currently active.

    // Time interval (in seconds) for spawning mail objects.
    public float spawnInterval = 1f;
    // The action that will notify the task that the minigame has started
    public static event Action updateGameStartNotify;

    // The action that will notify the task that the minigame is done
    public static event Action updateGameEndNotify;

    // This method executes when the script instance is being loaded.
    void Awake()
    {
        // Finds the BasicWindow component on the same GameObject and assigns it.
        window = GetComponent<BasicWindow>();
    }

    // Called before the first frame update.
    void Start()
    {
        // Finds components used for scoring and updates (these are searched automatically in the scene).
        scoreManager = FindObjectOfType<UpdateGameScoreManager>();
        windowArea = updateWindow.GetComponent<RectTransform>().rect;

        // Initially closes the game window.
        window.CloseWindow();
    }

    // Wrapper to prevent starting the game multiple times by accident.
    public void TryStartGame()
    {
        if (!playingGame) // If the game is not currently playing, start it.
        {
            window.isClosable = false;
            StartGame();
        }
        else
        {
            Debug.Log("Game is already playing");
        }
    }

    // Starts the game logic: opens window, sets flags, and calls relevant methods.
    public void StartGame()
    {
        updateGameStartNotify?.Invoke();
        // If they try to play again they need the player character!
        if (!player.activeSelf)
        {
            player.SetActive(true);
        }
        // Opens the game window for the user.
        window.OpenWindow();

        // Set the game states to indicate it is running.
        playingGame = true;
        isGameOver = false;

        // Start the spawning process for mail packets.
        SpawnPackets();

        // Begin the main game loop, managed by coroutine.
        StartCoroutine(PlayingGame());
    }

    // Main gameplay loop, executed every frame while the game is active.
    public IEnumerator PlayingGame()
    {
        while (!isGameOver) // Keep looping unless the game ends.
        {
            // Executes the following checks and methods:
            scoreManager.ScoreCheck(); // Check scores using the score manager.
            WinCheck(); // Check if win or lose conditions are fulfilled.
            MoveCharacter(); // Update the character's position based on mouse input.

            yield return null; // Wait until the next frame.
        }
        player.SetActive(false);
        updateGameEndNotify?.Invoke();
        window.isClosable = true;
        window.CloseWindow();
    }

    // Begins the mail spawning process by invoking the coroutine.
    public void SpawnPackets()
    {
        StartCoroutine(SpawnMail());
    }

    // Updates the player's position on the screen based on mouse input.
    public void MoveCharacter()
    {
        // Converts the mouse position to world coordinates.
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Converts world coordinates to anchored UI local space coordinates (relative to parent).
        Vector2 targetAnchoredPosition = (Vector2)playerRectTransform.parent.InverseTransformPoint(mouseWorldPosition);

        // Constrains the X position of the player within the defined window bounds.
        targetAnchoredPosition.x = Mathf.Clamp(targetAnchoredPosition.x, windowArea.xMin + 217, windowArea.xMax - 217);

        // Locks the Y coordinate at a constant value (-1881).
        targetAnchoredPosition.y = -1883;

        // Assign the calculated constrained position to the player's RectTransform.
        playerRectTransform.anchoredPosition = targetAnchoredPosition;
    }

    // Checks win and lose conditions by utilizing the UpdateGameScoreManager.
    public void WinCheck()
    {
        if (scoreManager.winReached) // If the win condition is met.
        {
            // Stop the game and update the UI to indicate success.
            playingGame = false;
            isGameOver = true;
            scoreManager.ResetScore();

        }
        else if (scoreManager.lossReached) // If the loss condition is met.
        {
            // Stop the game and update the UI to indicate failure.
            playingGame = false;
            isGameOver = true;
            scoreManager.ResetScore();
        }
    }

    // Coroutine responsible for repeatedly spawning mail objects.
    private IEnumerator SpawnMail()
    {
        while (!isGameOver) // Continuously spawn mail while the game is active.
        {
            // Instantiate a random mail prefab from the array.
            GameObject newMail = Instantiate(mail[Random.Range(0, mail.Length)], playerRectTransform.parent);
            newMail.GetComponent<AbstractMail>().window = window;
            // We grab the abstract mail component to get all shared methods between mail items and subscribe to the on close window event
            //window.OnWindowClose += newMail.GetComponent<AbstractMail>().GameOver;

            // Fetch the RectTransform of the spawned mail object.
            RectTransform rectTransform = newMail.GetComponent<RectTransform>();

            // Set its anchored position randomly along the X-axis within bounds, and set Y to 1881.
            rectTransform.anchoredPosition = new Vector2(Random.Range(windowArea.xMin + 165, windowArea.xMax - 165), 1872);

            // Wait for the specified spawn interval before repeating.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}


