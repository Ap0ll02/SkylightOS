using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Vector2 moveValue = moveAction.ReadValue<Vector2>();

            // Predict the new position
            Vector2 newPos = mazeRect.anchoredPosition - moveSpeed * moveValue;
            Vector2 playerPos = player.anchoredPosition + moveSpeed * moveValue;

            // Check if the new position stays wihtin path
            if (Touching(player.gameObject, mazePath, playerPos)) {
                mazeRect.anchoredPosition = newPos;
            }
            yield return null;
        }
    }

    // MARK: - Collision Detection
    // Use Rigidbody2D and Collider2D to detect collisions
    public bool Touching(GameObject player, GameObject mazePath, Vector2 newPos) {
        RectTransform playerRect = player.GetComponent<RectTransform>();
        RectTransform mazeRect = mazePath.GetComponent<RectTransform>();

        // Convert the player's new position to the local space of the maze
        Vector2 localPlayerPos = mazeRect.InverseTransformPoint(playerRect.TransformPoint(newPos));

        // Check if the player's new position is within the bounds of the maze
        Vector2 mazeMin = mazeRect.rect.min;
        Vector2 mazeMax = mazeRect.rect.max;

        Vector2 playerMin = localPlayerPos - playerRect.rect.size / 2;
        Vector2 playerMax = localPlayerPos + playerRect.rect.size / 2;

        if (playerMin.x < mazeMin.x || playerMax.x > mazeMax.x || playerMin.y < mazeMin.y || playerMax.y > mazeMax.y) {
            return false; // Player is out of bounds
        }

        // Check if the player is touching any maze colliders
        Collider2D playerCollider = player.GetComponent<BoxCollider2D>();
        Collider2D[] mazeColliders = mazePath.GetComponentsInChildren<BoxCollider2D>();

        foreach (Collider2D mazeCollider in mazeColliders) {
            if (playerCollider.IsTouching(mazeCollider)) {
                return true;
            }
        }

        return false;
    }

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
                // Debug.Log($"Collider Set: {collider.gameObject.name} | Local Size: {localSize}");
            }
        }
    }
}
