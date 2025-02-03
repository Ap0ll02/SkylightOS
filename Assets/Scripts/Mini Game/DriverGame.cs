using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Linq;
using System;
using TMPro;
using System.Diagnostics.Tracing;

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

    public int pCount = 0;
   public RectTransform player;
    public player pscript;
    public GameObject bg;
    public GameObject bg_alt;
    // Consider redoing this as an array of possible obstacles to spawn.
    public GameObject obstacle;

    public float percentage = 0;
    public float ob_speed = 0;

    public GameObject parent;
    public Vector2 speed = new(80, 0);
    public RectTransform bg_width;
    public Component[] obs;

    // Spawn notes: -1200 < y < 1200
    // x > 2300
    public GameObject obstacle_prefab;
    public List<GameObject> obs_list = new();
    public List<GameObject> bgs = new();
    public TMP_Text pBar;

    InputAction moveAction;
    public Action OnGameEnd;
    public int difficulty_p_reduction = 5;

    void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        pBar.text = "0%";   
        bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
        bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(244.5f, 0.91f, 90);
        moveAction = InputSystem.actions.FindAction("Move");           
        // Remove comment for testing convenience.
        //StartCoroutine(Progression());
        this.gameObject.SetActive(false);
    }

    public override void StartGame() {
        this.gameObject.SetActive(true);
        obs = obstacle.GetComponentsInChildren<RectTransform>(); 
        pBar.text = "0%";   
        bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
        bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(244.5f, 0.91f, 90);
        obs_list.Add(Instantiate(obstacle_prefab, parent: obstacle.GetComponent<RectTransform>()));
        gameRunning = true;
        StartCoroutine(Progression());
        StartCoroutine(SpawnObstacle());
    }

    public void OnEnable() {
        pscript.OnTObs += PlayerHit;
    }

    public void OnDisable() {
            pscript.OnTObs -= PlayerHit;
    }
    public void PlayerHit() {
        pCount -= difficulty_p_reduction;
        pCount = pCount < 0 ? 0 : pCount;
    }
    public bool gameRunning = false;
    // Update is called once per frame
    void Update()
    {
        while(gameRunning) {
            // Gotta loop because ideal scrolling leaves 2 bg instances in scene
            // at once.
            foreach (var b in bgs) {
                b.GetComponent<RectTransform>().anchoredPosition -= speed * Time.deltaTime;
            }
            
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            player.anchoredPosition += moveValue;
            CheckBounds();
            HandleObs();
        }
    }

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
        foreach (var ob in obs_list) {
            if(ob.GetComponent<RectTransform>().anchoredPosition.x < -2300) {
                Destroy(ob);
            }
        }

    }

    void HandleObs() {
        obs = obstacle.GetComponentsInChildren<RectTransform>(); 
        foreach (RectTransform ob in obs.Cast<RectTransform>())
        {
            ob.anchoredPosition -= 5 * Time.deltaTime * speed;                       
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
            float y_pos = rnd.Next(-1200, 1200);
            float x_pos = rnd.Next(2300, 4000);
            yield return new WaitForSeconds(ob_speed);
            obs_list.Add(Instantiate(obstacle_prefab, parent: obstacle.GetComponent<RectTransform>()));
            obs_list[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(x_pos, y_pos);
        }
    }

    public void GameEnd() {
        foreach (var b in bgs){
            Destroy(b);
        }
        bgs.RemoveRange(0, bgs.Count);
        obs_list.RemoveRange(0, obs_list.Count);
        StopCoroutine(Progression());
        StopCoroutine(SpawnObstacle());
    }
}
