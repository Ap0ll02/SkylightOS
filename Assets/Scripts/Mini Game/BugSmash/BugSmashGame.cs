using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BugSmashGame : MonoBehaviour
{

    // Controls the spawn of bugs
    // Time interval (in seconds) for spawning mail objects.
    public float spawnInterval = 1f;
    public int spawnCount = 0;
    public int spawnMax = 10;
    public GameObject[] bugs;

    // Reference to the BasicWindow component used to manage opening and closing game menus.
    public BasicWindow window;

    public GameObject updateWindow;
    // THIS IS VERY VERY IMPORTANT IS GUIDES WHERE THINGS SPAWN VIA THE WINDOW DIMENSIONS
    public Rect windowArea;

    // This is so we can deactivate the ethernet cable after the game ends
    public GameObject player;
    // Reference to the player RectTransform, used to set movement and positioning.
    public RectTransform playerRectTransform;

    // Reference to an UpdateGameScoreManager, which will manage scoring logic (e.g., wins/losses).
    public BugSmashGameScoreManager scoreManager;

    // Reference to UpdatePanel used for controlling the game's UI graphics state.
    // Game state tracking variables.
    public bool isGameOver = false; // Whether the game is over.
    public bool playingGame = false; // Whether the game is currently active.

    // The action that will notify the task that the minigame has started
    public static event Action BugSmashGameStartNotify;

    // The action that will notify the task that the minigame is done
    public static event Action BugSmashGameEndNotify;

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
        window.CloseWindow();
        scoreManager = FindObjectOfType<BugSmashGameScoreManager>();
        windowArea = updateWindow.GetComponent<RectTransform>().rect;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("CatGirlWizardPlayer");
        }

        if (player == null)
        {
            throw new Exception("Player not found");
        }
        player.GameObject().GetComponent<CatGirlWizard>().window = window;
        // Initially closes the game window.
    }

    // Wrapper to prevent starting the game multiple times by accident.
    public void TryStartGame()
    {
        if (!playingGame) // If the game is not currently playing, start it.
        {
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
        // Opens the game window for the user.
        window.OpenWindow();
        //BugSmashGameStartNotify?.Invoke();
        // If they try to play again they need the player character!
        if (!player.activeSelf)
        {
            player.SetActive(true);
        }
        // Set the game states to indicate it is running.
        playingGame = true;
        isGameOver = false;

        // Start the spawning process for mail packets.
        StartCoroutine(SpawnBugs());

        // Begin the main game loop, managed by coroutine.
        StartCoroutine(PlayingGame());
    }

    // Main gameplay loop, executed every frame while the game is active.
    public IEnumerator PlayingGame()
    {
        while (!isGameOver) // Keep looping unless the game ends.
        {
            // Executes the following checks and methods:
            WinCheck(); // Check if win or lose conditions are fulfilled.
            yield return null; // Wait until the next frame.
        }
        player.SetActive(false);
        //BugSmashGameEndNotify?.Invoke();
        window.CloseWindow();
    }

    // Checks win and lose conditions by utilizing the UpdateGameScoreManager.
    public void WinCheck()
    {

        scoreManager.ScoreCheck(); // Check scores using the score manager.
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
    private IEnumerator SpawnBugs()
    {
        while (!isGameOver) // Continuously spawn mail while the game is active.
        {
            // Instantiate a random Bug prefab from the array.
            GameObject bug = Instantiate(bugs[UnityEngine.Random.Range(0, bugs.Length)],
            playerRectTransform.parent);
            bug.GetComponent<AbstractBug>().window = window;
            // Fetch the RectTransform of the spawned mail object.
            RectTransform rectTransform = bug.GetComponent<RectTransform>();

            // Wait for the specified spawn interval before repeating.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
