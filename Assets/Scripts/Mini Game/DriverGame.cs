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
    // COment for merhe

    void CheckBounds() {
        // Player bounds, so they can't go off screen
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

        // Handles background scrolling instantiation and deletion
        if(bgs[^1].GetComponent<RectTransform>().anchoredPosition.x < -243) {
            bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
            bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(643.8f, 0.91f, 90);
        }
        try{
            if(bgs[^2].GetComponent<RectTransform>().anchoredPosition.x < -643){
            Destroy(bgs[^2]);
            bgs.RemoveAt(bgs.Count-2);
        }} catch (Exception e) when (e is ArgumentOutOfRangeException) {
            Debug.Log("Ignore: Early index error handling. Driver Game: line 97");
        }

        obs_list.RemoveAll(ob => {
            if(ob.GetComponent<RectTransform>().anchoredPosition.x < -2300) {
                Destroy(ob);
                return true;
            }
            return false;
        });

    }

    void HandleObs() {
        foreach (var ob in obs_list)
        {
            ob.GetComponent<RectTransform>().anchoredPosition -= ob_speed * Time.deltaTime * speed;
        }
    }
    public IEnumerator Progression() {
        while(pCount < 100) {
            pCount++;
            pBar.text = pCount + "%";
            yield return new WaitForSeconds(0.33f); 
       }
       GameEnd();
       OnGameEnd?.Invoke();
    }

    public IEnumerator SpawnObstacle() {
        while(true) {
            System.Random rnd = new();
            int to_spawn = rnd.Next(1, ob_max_spawn);
            for(int i = 0; i < to_spawn; i++) {
                float y_pos = rnd.Next(-1200, 1200);
                float x_pos = rnd.Next(2300, 6000);
                float spawn_time = rnd.Next((int)ob_spawntime, (int)(1.5f * ob_spawntime));
                yield return new WaitForSeconds(spawn_time);
                obs_list.Add(Instantiate(obstacle_prefab, parent: obstacle.GetComponent<RectTransform>()));
                obs_list[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(x_pos, y_pos, 0);
            }
        }
    }

    public void GameEnd() {
        foreach (var b in bgs){
            Destroy(b);
        }
        gameRunning = false;
        bgs.RemoveRange(0, bgs.Count);
        obs_list.RemoveRange(0, obs_list.Count);
        StopCoroutine(Progression());
        StopCoroutine(SpawnObstacle());
        window.CloseWindow();
    }
}
