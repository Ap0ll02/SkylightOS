using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

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

    // Open the window, start update coroutine
    public override void StartGame() {
        foreach(var maze in mazeContainer){
            maze.SetActive(false);
        }
        System.Random rand = new();
        int pickMaze = rand.Next(0, mazeContainer.Count);
        mazeContainer[pickMaze].SetActive(true);
        // SetCollider();
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
    public IEnumerator GameUpdate() {
        RectTransform mazeRect = mazePath.GetComponent<RectTransform>();

        while (gameRunning) {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();

            // Predict the new position
            Vector2 newPos = mazeRect.anchoredPosition - moveSpeed * moveValue;

            // Check if the new position collides with any walls
            if (Touching(player.gameObject, mazePath)) {
                Debug.Log("Colliding with walls");
                mazeRect.anchoredPosition = newPos;
            } else {
                mazeRect.anchoredPosition += moveSpeed * moveValue;
            }

            yield return null;
        }
    }

    // public bool IsCollidingWithWalls(Vector2 newPos) {
    //     RectTransform playerRect = player.GetComponent<RectTransform>();

    //     foreach (RectTransform wall in mazePath.transform) { // Assuming walls are children of mazePath
    //         if (wall == null) continue;

    //         if (RectOverlaps(playerRect, wall, newPos)) {
    //             return true; // Collision detected, movement should be blocked
    //         }
    //     }

    //     return false; // No collision, allow movement
    // }

    // public bool RectOverlaps(RectTransform rectA, RectTransform rectB, Vector2 newPos) {
    //     Vector2 sizeA = rectA.sizeDelta;
    //     Vector2 sizeB = rectB.sizeDelta;

    //     Vector2 minA = newPos - sizeA / 2;
    //     Vector2 maxA = newPos + sizeA / 2;

    //     Vector2 minB = rectB.anchoredPosition - sizeB / 2;
    //     Vector2 maxB = rectB.anchoredPosition + sizeB / 2;

    //     return minA.x < maxB.x && maxA.x > minB.x &&
    //             minA.y < maxB.y && maxA.y > minB.y;
    // }

   

    // Use Rigidbody2D and Collider2D to detect collisions
    public bool Touching(GameObject player, GameObject mazePath) {
        Collider2D playerCollider = player.GetComponent<BoxCollider2D>();
        Collider2D[] mazeColliders = mazePath.GetComponentsInChildren<BoxCollider2D>();

        foreach (Collider2D mazeCollider in mazeColliders) {
            //if (mazeCollider == mazePath.GetComponent<Collider2D>()) continue; // Skip the parent mazePath Collider2D
            if (playerCollider.IsTouching(mazeCollider)) {
                return true;
            }
        }
        return false;
    }

    // public void SetCollider() { 
    //     Canvas.ForceUpdateCanvases(); // Ensure UI is updated before calculations

    //     foreach (BoxCollider2D mp in mazePath.GetComponentsInChildren<BoxCollider2D>()) { 
            
    //         if (mp.TryGetComponent<RectTransform>(out var rt)) {
    //             Vector3[] corners = new Vector3[4];
    //             rt.GetWorldCorners(corners); // Get the 4 world-space corners

    //             float width = Vector2.Distance(corners[0], corners[3]); // Left to Right
    //             float height = Vector2.Distance(corners[0], corners[1]); // Bottom to Top

    //             Vector2 worldSize = new Vector2(width, height);

    //             mp.size = worldSize;
    //             Debug.Log($"Collider Set: {mp.gameObject.name} | World Size: {worldSize}");
    //         }
    //     }
    // }


}
