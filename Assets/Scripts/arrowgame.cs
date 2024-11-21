using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Arrowgame : AbstractMinigame
{
    /// @var arrow & arrowDir, leave these alone, references to scene GameObjects
    /// and components.
    public GameObject arrow;
    public Transform arrowDir;
    public Transform parent;
    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }
    public List<Direction> arrowDirs = new();

    /// @var numArrows is the number of arrows to spawn on the screen.
    public readonly int numArrows = 20;

    public void Awake() {
        arrow = GameObject.Find("Arrow");
        arrowDir = arrow.GetComponent<Transform>();
        parent = GameObject.Find("ArrowGame").GetComponent<Transform>();
        arrowDirs.Insert(0, Direction.Right);
    }

    public void Start() {
        //gameObject.SetActive(false);
        SpawnArrow(numArrows);
    }

    public void SpawnArrow(int numToSpawn) {
        int[] dir = new int[]{0, 90, 180, 270};


        for(int i = 1; i <= numToSpawn; i++) {
            int dirNum = UnityEngine.Random.Range(0, 3);

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
        }
        
    }

    public override void StartGame() {

    }

    public override void CanContinue() {

    }

}
