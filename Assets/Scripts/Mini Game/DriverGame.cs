using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Linq;
using System;

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

    public GameObject parent;
    public Vector2 speed = new(80, 0);
    public RectTransform bg_width;
    public Component[] obs;
    public List<GameObject> bgs = new();

    InputAction moveAction;

    void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
        bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(244.5f, 0.91f, 90);
        moveAction = InputSystem.actions.FindAction("Move");   
    }

    public override void StartGame() {
        obs = obstacle.GetComponentsInChildren<RectTransform>(); 
        foreach(var ob in obs) {
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        // TODO Change this to a spawn system on horizontal layout and delete after every X position through spawn list.
        // between -210 and -240
        // if(bg_cpos.anchoredPosition.x < -221.86436) {
        //     bg_cpos.anchoredPosition = bg_pos;
        // }
        // Don't forget deltaTime with movement     
        foreach (var b in bgs) {
            b.GetComponent<RectTransform>().anchoredPosition -= speed * Time.deltaTime;
        }
        
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        player.anchoredPosition += moveValue;
        CheckBounds();
        HandleObs();
    }

    void CheckBounds() {
        if(player.anchoredPosition.x < -1700) {
            player.anchoredPosition = new Vector2(-1700, player.anchoredPosition.y);
        } else if (player.anchoredPosition.x > 1700) {
            player.anchoredPosition = new Vector2(1700, player.anchoredPosition.y);
        }

        if(player.anchoredPosition.y < -980) {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x, -980);
        } else if (player.anchoredPosition.y > 900) {
            player.anchoredPosition = new Vector2(player.anchoredPosition.x, 900);
        }

        if(bgs[^1].GetComponent<RectTransform>().anchoredPosition.x < -243) {
            bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
            bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(643.8f, 0.91f, 90);
        }
        try{if(bgs[^2].GetComponent<RectTransform>().anchoredPosition.x < -643){
            Destroy(bgs[^2]);
            bgs.RemoveAt(bgs.Count-2);
        }} catch (Exception e) when (e is ArgumentOutOfRangeException) {
            Debug.Log("idk hope it works");
        }

    }

    void HandleObs() {
        obs = obstacle.GetComponentsInChildren<RectTransform>(); 
        foreach (RectTransform ob in obs.Cast<RectTransform>())
        {
            ob.anchoredPosition -= 5 * Time.deltaTime * speed;                       
        }

    }
}
