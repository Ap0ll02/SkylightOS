using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrowgame : AbstractMinigame
{
    /// @var arrow & arrowDir, leave these alone, references to scene GameObjects
    /// and components.
    public GameObject arrow;
    public Transform arrowDir;

    /// @var numArrows is the number of arrows to spawn on the screen.
    int numArrows = 20;

        public enum Direction {
        Up,
        Left,
        Down,
        Right
    }

    public void Awake() {
        arrow = GameObject.Find("Arrow");
        arrowDir = arrow.GetComponent<Transform>();
    }

    public void Start() {
        gameObject.SetActive(false);
    }

    public void SpawnArrow(Direction direction) {

    }

    public override void StartGame() {

    }

    public override void CanContinue() {

    }

}
