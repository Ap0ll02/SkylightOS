using System.Collections;
using System.Collections.Generic;
using Febucci.UI.Actions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
        RectTransform mazePos = mazePath.GetComponent<RectTransform>();
        while(gameRunning){
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            if(Touching(player.gameObject, mazePath)) mazePos.anchoredPosition -= moveSpeed * moveValue;
            if(mazePos.anchoredPosition.x < -2645) mazePos.anchoredPosition = new Vector2(-2645, mazePos.anchoredPosition.y);
            else if(mazePos.anchoredPosition.x > 2900) mazePos.anchoredPosition = new Vector2(2900, mazePos.anchoredPosition.y);
            if(mazePos.anchoredPosition.y > 1330) mazePos.anchoredPosition = new Vector2(mazePos.anchoredPosition.x, 1330);
            else if(mazePos.anchoredPosition.y < -1800) mazePos.anchoredPosition = new Vector2(mazePos.anchoredPosition.x, -1800);
           yield return null;
       }
    }    

    // TODO: Fix the issue with player anchoredposition not in the same scope as maze piece anchoredpositions
    public bool Touching(GameObject player, GameObject mazePath) {
        RectTransform playerRect = player.GetComponent<RectTransform>();
        RectTransform[] mazeRects = mazePath.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform mazeRect in mazeRects) {
            if (mazeRect == mazePath.GetComponent<RectTransform>() || mazeRect.gameObject.GetComponent<RawImage>()) continue; // Skip the parent mazePath RectTransform

            Vector2 playerMin = playerRect.anchoredPosition - (playerRect.rect.size * 0.5f);
            Vector2 playerMax = playerRect.anchoredPosition + (playerRect.rect.size * 0.5f);

            Vector2 mazeMin = mazeRect.anchoredPosition - (mazeRect.rect.size * 0.5f);
            Vector2 mazeMax = mazeRect.anchoredPosition + (mazeRect.rect.size * 0.5f);

            if (playerMin.x >= mazeMin.x && playerMax.x <= mazeMax.x &&
                playerMin.y >= mazeMin.y && playerMax.y <= mazeMax.y) {
                return true;
            }
        }
        return false;
    }
}
