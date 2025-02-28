using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FileRecoMazeMiniGame : AbstractMinigame
{
    // Maze Initials

    public List<GameObject> mazeContainer;
    public GameObject player;

    // GamePlay Initials
    public Coroutine updateGame;
    public InputAction moveAction;
    public bool gameRunning = false;
    public float moveSpeed = 1f;

    // Get any non-inspector references here
    void Start()
    {
        StartGame();
    }

    // MARK: - Game Initialization
    public override void StartGame() {
        Canvas.ForceUpdateCanvases(); // Co-Pilot tip for updating UI before calculations
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
        while (gameRunning) {
            // Read input, move player
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            player.transform.position += moveSpeed * Time.deltaTime * (Vector3)moveValue;
            yield return null;
        }
    }
}
