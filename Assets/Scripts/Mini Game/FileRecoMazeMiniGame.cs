using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FileRecoMazeMiniGame : AbstractMinigame
{
    // Maze Initials

    public List<GameObject> mazeContainer;
    public GameObject player;
    public GameObject cameraPos;

    // GamePlay Initials
    public Coroutine updateGame;
    public InputAction moveAction;
    public bool gameRunning = false;
    readonly float moveSpeed = 1.7f;

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
    Vector3 velocity = Vector3.zero;
    public IEnumerator GameUpdate() {
        while (gameRunning) {
            // Read input, move player
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            player.transform.position += moveSpeed * Time.deltaTime * (Vector3)moveValue;
            // cameraPos.transform.position = Vector3.Lerp(cameraPos.transform.position, player.transform.position, 0.1f);
            cameraPos.transform.position = Vector3.SmoothDamp(cameraPos.transform.position, player.transform.position, ref velocity, 0.1f);
            
            yield return new WaitForFixedUpdate();
        }
    }
}
