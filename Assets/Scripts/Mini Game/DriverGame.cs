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
    public Vector2 speed = new Vector2(80, 0);
    public RectTransform bg_cpos;
    public RectTransform bg_width;

    InputAction moveAction;

    void Awake() {
        bg_cpos = bg.GetComponent<RectTransform>();
        bg_pos = bg_cpos.anchoredPosition;
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
        // TODO Change this to a spawn system on horizontal layout and delete after every X position through spawn list.
        // between -210 and -240
        if(bg_cpos.anchoredPosition.x < -221.86436) {
            bg_cpos.anchoredPosition = bg_pos;
        }
        // Don't forget deltaTime with movement     
        bg_cpos.anchoredPosition -= (speed * Time.deltaTime);
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        player.anchoredPosition += moveValue;
        CheckBounds();
    }

    void CheckBounds() {
        if(player.anchoredPosition.x < -1700) {
            player.anchoredPosition = new Vector2(-1700, player.anchoredPosition.y);
        } else if (player.anchoredPosition.x > 1700) {
            player.anchoredPosition = new Vector2(1700, player.anchoredPosition.y);
        }

        if(player.anchoredPosition.y < -900) {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x, -900);
        } else if (player.anchoredPosition.y > 900) {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x, 900);
        }
    }
}
