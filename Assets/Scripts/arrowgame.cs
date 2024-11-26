using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Jack Ratermann
/// Arrow mini game, in this case implemented in as general a way as I could, with a couple hard references to the terminal.
/// Depends on a spawnArea being created somewhere.
/// This game should be made a variant for now if you plan to implement it into another task other than TerminalTask.
/// </summary>
public class Arrowgame : AbstractMinigame
{
    // CLEANUP NOTES: Make it get from task bro

    /// @var arrow & arrowDir, leave these alone, references to scene GameObjects
    /// and components.
    [SerializeField] GameObject arrow; // Hard reference in Unity, is OKAY because arrow is PreFab'd
    // Events for the arrow to broadcast for the task to do things. {End game and checkHazards()}
    public static event Action OnGameEnd;
    public static event Action OnArrowPress;
    // Where to spawn the arrows
    public Transform spawnArea;
    public PlayerInput pInput;
    public int curArrow = 0;
    public bool GameOver = true;
    // easier readability enum
    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }
    public List<Direction> arrowDirs = new();
    public List<RawImage> arrowColors = new();

    /// @var numArrows is the number of arrows to spawn on the screen.
    public readonly int numArrows = 20;

    public void Awake() {
        // Not hardcode Terminal
        spawnArea = FindObjectOfType<Terminal>().GetComponentInChildren<GridLayoutGroup>().gameObject.GetComponent<Transform>();
        pInput = new PlayerInput();
    }

    public void Start() {
        GameOver = true;
        gameObject.SetActive(false);
    }

    public void SpawnArrow(int numToSpawn) {
        // Matching Z euler rotations to enum for readability and simpler coding
        int[] dir = new int[]{0, 90, 180, 270};
        UnityEngine.Random.InitState(System.Environment.TickCount);
        for(int i = 0; i <= numToSpawn; i++) {
            int dirNum = UnityEngine.Random.Range(0, 4);

            static Direction P(int dir) => 
                dir switch {
                    0 => Direction.Right,
                    1 => Direction.Up,
                    2 => Direction.Left,
                    3 => Direction.Down,
                    _ => throw new ArgumentException("Invalid Random Number Cannot Match Enum", nameof(dir)),
                };
            // Create the arrow with desired (Random) rotations and other initializations
            // Lists give simpler management of colors and direction
            GameObject newArrow = Instantiate(arrow, spawnArea);
            RectTransform rt = newArrow.GetComponent<RectTransform>();
            rt.localPosition = UnityEngine.Vector3.zero;
            rt.localRotation = UnityEngine.Quaternion.Euler(0, 0, dir[dirNum]);
            arrowDirs.Insert(i, P(dirNum));
            arrowColors.Insert(i, newArrow.GetComponent<RawImage>());
            arrowColors[i].color = Color.white;
        }
        curArrow = 0;
    }

    public override void StartGame() {
        // Allows game to be started on a function call, rather than on normal Startup
        Debug.Log(GameOver);
        if(GameOver == true) {
            SpawnArrow(numArrows);
            GameOver = false;
        }
        else {
            Debug.Log("Game is already playing.");
        }
        
    }

    public void EndGame() {
        // Allows task function to recognize when the game is over
        foreach (RawImage arrow in arrowColors) {
            Destroy(arrow.gameObject);
        }
        OnGameEnd?.Invoke();
    }


    public void HandleInput(InputAction.CallbackContext context) {
        // Updates colors for more fun experience, broadcasts to allow for input validation to be done
        // in an event based fashion, instead of in Update().
        OnArrowPress?.Invoke();
        if(curArrow > numArrows && GameOver == false) {
            Debug.Log("Completed Task!");
            GameOver = true;
            spawnArea.gameObject.SetActive(false);
            EndGame();
        }
        else {
            // Right Arrow
            if(context.action.ReadValue<float>() == 1 && context.phase == InputActionPhase.Performed) {
                if(CanContinue && arrowDirs[curArrow] == Direction.Right) {
                    Debug.Log("Correct Arrow " + context.action.ReadValue<float>());
                    arrowColors[curArrow].color = Color.green;
                    curArrow++;
                }
                else if(CanContinue){
                    Debug.Log("Wrong Arrow" + context.action.ReadValue<float>()); 
                    arrowColors[curArrow].color = Color.red;
                }
            }
            // Left Arrow
            if(context.action.ReadValue<float>() == -1 && context.phase == InputActionPhase.Performed) {
                if(CanContinue && arrowDirs[curArrow] == Direction.Left) {
                    Debug.Log("Correct Arrow " + context.action.ReadValue<float>());
                    arrowColors[curArrow].color = Color.green;
                    curArrow++;
                }
                else if(CanContinue){
                    Debug.Log("Wrong Arrow" + context.action.ReadValue<float>());
                    arrowColors[curArrow].color = Color.red;
                }
            }
            // Up Arrow
            if(context.action.ReadValue<float>() > 1 && context.phase == InputActionPhase.Performed) {
                if(CanContinue && arrowDirs[curArrow] == Direction.Up) {
                    Debug.Log("Correct Arrow" + context.action.ReadValue<float>());
                    arrowColors[curArrow].color = Color.green;
                    curArrow++;
                }
                else if(CanContinue){
                    Debug.Log("Wrong Arrow" + context.action.ReadValue<float>());
                    arrowColors[curArrow].color = Color.red;
                }       
            }
            // Down Arrow
            if(context.action.ReadValue<float>() < -1 && context.phase == InputActionPhase.Performed) {
                if(CanContinue && arrowDirs[curArrow] == Direction.Down) {
                    Debug.Log("Correct Arrow" + context.action.ReadValue<float>());
                    arrowColors[curArrow].color = Color.green;
                    curArrow++; 
                }
                else if(CanContinue){
                    Debug.Log("Wrong Arrow" + context.action.ReadValue<float>());
                    arrowColors[curArrow].color = Color.red;
                }           
            }
        }
    }
}
