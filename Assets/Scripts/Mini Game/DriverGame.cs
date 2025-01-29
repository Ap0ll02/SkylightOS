using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject player;
    public GameObject bg;
    // Consider redoing this as an array of possible obstacles to spawn.
    public GameObject obstacle;

    public float percentage = 0;
    public float chance = 0;
    void Awake() {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void StartGame() {

    }
    // Update is called once per frame
    void Update()
    {
        // Don't forget deltaTime with movement     
    }
}
