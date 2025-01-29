using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DriverGame : AbstractMinigame
{
    /*
    Thoughts: 
    ---
    Side scroller, dino game clone with:
    - Jump
    - Crouch
    - Collect points to avoid driver glitches like screen blackout etc.
    ---
    */

    public RectTransform player;
    public GameObject bg;
    public GameObject bg_alt;
    // Consider redoing this as an array of possible obstacles to spawn.
    public GameObject obstacle;

    public float percentage = 0;
    public float chance = 0;
    public Vector2 bg_pos;
    public Vector2 speed = new Vector2(-10, 0);
    public RectTransform bg_cpos;
    public RectTransform bg_width;

    InputAction moveAction;

    void Awake() {
        bg_pos = bg.transform.position;
        bg_cpos = bg.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");   
    }

    public override void StartGame() {

    }
    // Update is called once per frame
    void Update()
    {
        // Don't forget deltaTime with movement     
        bg_cpos.anchoredPosition -= speed * Time.deltaTime;
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        player.anchoredPosition += new Vector2(moveValue.x, 0);
    }
}
