using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Arrowgame : AbstractMinigame
{
    /// @var arrow & arrowDir, leave these alone, references to scene GameObjects
    /// and components.
    public GameObject arrow;
    public Transform arrowDir;
    public RawImage arrowColor;
    public Transform parent;
    public PlayerInput pInput;
    public int curArrow = 0;
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
        arrow = GameObject.Find("Arrow");
        arrowDir = arrow.GetComponent<Transform>();
        parent = GameObject.Find("ArrowGame").GetComponent<Transform>();
        arrowDirs.Insert(0, Direction.Right);
        arrowColor = arrow.GetComponent<RawImage>();
        pInput = new PlayerInput();
        arrowColors.Insert(0, arrowColor);
        arrowColors[0].color = Color.white;
    }

    public void Start() {
        gameObject.SetActive(false);
    }

    public void SpawnArrow(int numToSpawn) {
        int[] dir = new int[]{0, 90, 180, 270};

        UnityEngine.Random.InitState(System.Environment.TickCount);
        for(int i = 1; i <= numToSpawn; i++) {
            int dirNum = UnityEngine.Random.Range(0, 4);

            static Direction P(int dir) => 
                dir switch {
                    0 => Direction.Right,
                    1 => Direction.Up,
                    2 => Direction.Left,
                    3 => Direction.Down,
                    _ => throw new ArgumentException("Invalid Random Number Cannot Match Enum", nameof(dir)),
                };

            GameObject newArrow = Instantiate(arrow, parent);
            RectTransform rt = newArrow.GetComponent<RectTransform>();
            rt.localPosition = UnityEngine.Vector3.zero;
            rt.localRotation = UnityEngine.Quaternion.Euler(0, 0, dir[dirNum]);
            arrowDirs.Insert(i, P(dirNum));
            arrowColors.Insert(i, newArrow.GetComponent<RawImage>());
            arrowColors[i].color = Color.white;
        }
    }

    public override void StartGame() {
        SpawnArrow(numArrows);
    }

    public override bool CanContinue() {

        return true;
    }

    public void HandleInput(InputAction.CallbackContext context) {
        if(curArrow > numArrows) {
            Debug.Log("Completed Task!");
        }
        else {
            // Right Arrow
            if(context.action.ReadValue<float>() == 1 && context.phase == InputActionPhase.Performed) {
                if(CanContinue() && arrowDirs[curArrow] == Direction.Right) {
                    Debug.Log("Correct Arrow");
                    arrowColors[curArrow].color = Color.green;
                    curArrow++;
                }
                else {
                    Debug.Log("Wrong Arrow"); 
                    arrowColors[curArrow].color = Color.red;
                }
            }
            // Left Arrow
            if(context.action.ReadValue<float>() == -1 && context.phase == InputActionPhase.Performed) {
                if(CanContinue() && arrowDirs[curArrow] == Direction.Left) {
                    Debug.Log("Correct Arrow");
                    arrowColors[curArrow].color = Color.green;
                    curArrow++;
                }
                else {
                    Debug.Log("Wrong Arrow");
                    arrowColors[curArrow].color = Color.red;
                }
            }
            // Up Arrow
            if(context.action.ReadValue<float>() == 25 && context.phase == InputActionPhase.Performed) {
                if(CanContinue() && arrowDirs[curArrow] == Direction.Up) {
                    Debug.Log("Correct Arrow");
                    arrowColors[curArrow].color = Color.green;
                    curArrow++;
                }
                else {
                    Debug.Log("Wrong Arrow");
                    arrowColors[curArrow].color = Color.red;
                }       
            }
            // Down Arrow
            if(context.action.ReadValue<float>() == -25 && context.phase == InputActionPhase.Performed) {
                if(CanContinue() && arrowDirs[curArrow] == Direction.Down) {
                    Debug.Log("Correct Arrow");
                    arrowColors[curArrow].color = Color.green;
                    curArrow++; 
                }
                else {
                    Debug.Log("Wrong Arrow");
                    arrowColors[curArrow].color = Color.red;
                }           
            }
        }
    }
}
