using System.Collections;
using System.Collections.Generic;
using Febucci.UI.Actions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float moveSpeed = 4f;

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

    public IEnumerator GameUpdate() {
        RectTransform mazePos = mazePath.GetComponent<RectTransform>();
        while(gameRunning){
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            if(Touching(player.gameObject, mazePath)) mazePos.anchoredPosition += moveSpeed * Time.deltaTime * moveValue;
        }
        yield return null;
    }    

    public bool Touching(GameObject player, GameObject mazePath) {
        Rect playerRect = player.GetComponent<RectTransform>().rect;
        Rect mazeRect = mazePath.GetComponent<RectTransform>().rect;

        return mazeRect.Contains(new Vector2(playerRect.xMin, playerRect.yMin)) &&
                mazeRect.Contains(new Vector2(playerRect.xMax, playerRect.yMax));
    }
}
