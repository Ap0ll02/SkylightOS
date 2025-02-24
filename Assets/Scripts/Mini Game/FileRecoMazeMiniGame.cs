using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO: Currently, the player can still move out of the maze.

public class FileRecoMazeMiniGame : AbstractMinigame
{
    // Maze Initials
    public GameObject mazePath;
    public GameObject bg;
    public List<GameObject> mazeContainer;
    public GameObject filePrefab;
    public List<GameObject> filePiece; 
    public RectTransform player;

    // GamePlay Initials
    public Coroutine updateGame;
    public InputAction moveAction;
    public bool gameRunning = false;
    public float moveSpeed = 2f;

    // Get any non-inspector references here
    void Start()
    {
        StartGame();
    }

    // MARK: - Game Initialization
    public override void StartGame() {
        foreach(var maze in mazeContainer){
            maze.SetActive(false);
        }
        System.Random rand = new();
        int pickMaze = rand.Next(0, mazeContainer.Count);
        mazeContainer[pickMaze].SetActive(true);
        Canvas.ForceUpdateCanvases(); // Co-Pilot tip for updating UI before calculations
        SetColliders(); // Sets the boxcolliders automatically, much love, kisses, thanks for the help copilot
        GetComponent<BasicWindow>().OpenWindow();        
        gameRunning = true;
        moveAction = InputSystem.actions.FindAction("Move"); 
        updateGame = StartCoroutine(GameUpdate());
    }

    public void GameOver() {
        gameRunning = false;
        StopCoroutine(updateGame);
        GetComponent<BasicWindow>().CloseWindow();
    }
    // -2645 x, 1330 y, is the maximum right and bottom we allow the maze to move
    // MARK: - Game Update
    public IEnumerator GameUpdate() {
        RectTransform mazeRect = mazePath.GetComponent<RectTransform>();

        while (gameRunning) {
            // Read input, move player
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            Vector2 pPosBefore = player.anchoredPosition;
            player.anchoredPosition += -1f * moveSpeed * moveValue;
            Vector2 pPosAfter = player.anchoredPosition;
            if(pPosBefore != pPosAfter){
                mazeRect.anchoredPosition += -1f * moveSpeed * moveValue;
            }
            yield return null;
        }
    }

    // MARK: - Collision Detection

    public void SetColliders() { 
        Canvas.ForceUpdateCanvases(); // Ensure UI is updated before calculations

        foreach (BoxCollider2D collider in mazePath.GetComponentsInChildren<BoxCollider2D>()) { 
            if (collider.TryGetComponent<RectTransform>(out var rt)) { // Finally get to use out variables woohoo
                Vector3[] corners = new Vector3[4];
                rt.GetWorldCorners(corners); // Get the 4 world-space corners

                // Calculate the size in world space
                float width = Vector3.Distance(corners[0], corners[3]); // Left to Right
                float height = Vector3.Distance(corners[0], corners[1]); // Bottom to Top

                // Convert world size to local size, this part is critical, as the nesting makes world scale pointless
                Vector2 localSize = new(width / rt.lossyScale.x, height / rt.lossyScale.y);

                collider.size = localSize;
                collider.offset = rt.rect.center;
            }
        }
    }
}
