using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    public int pCount = 0;
    public RectTransform player;
    public player pscript;
    public GameObject bg;
    public DriverTask dgTask;

    // Consider redoing this as an array of possible obstacles to spawn.
    public GameObject obstacle;
    InputAction moveAction;
    public Vector2 target;
    public float percentage = 0;
    readonly float ob_speed = 34f;
    readonly int ob_max_spawn = 8;
    readonly float ob_spawntime = 2f;

    public GameObject parent;
    public Vector2 speed = new(80, 0);
    public Component[] obs;
    public BasicWindow window;

    // Spawn notes: -1200 < y < 1200
    // x > 2300
    public GameObject obstacle_prefab;
    public List<GameObject> obs_list = new();
    public List<GameObject> bgs = new();
    public TMP_Text pBar;

    public CanvasGroup my_cg;
    public Action OnGameStart;
    public Action OnGameEnd;
    public int difficulty_p_reduction = 20;

    // HazardManager shiet
    private bool popupContinue = true;
    private bool lockdownContinue = true;

    public GameObject boundingBox;

    public AudioSource loopSoundEffect; // Audio source for looping sound
    public AudioClip[] obstacleHitSounds; // Array of sound effects for obstacle hits
    public AudioClip startSound;
    public AudioSource soundEffectSource; // Audio source for playing obstacle hit sounds

    void Awake()
    {
        gameRunning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        window.ForceCloseWindow();
        difficulty_p_reduction = 20;
    }

    public override void StartGame()
    {
        OnGameStart?.Invoke();
        pBar.text = "0%";
        bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
        bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(244.5f, 0.91f, 90);
        moveAction = InputSystem.actions.FindAction("Move");
        gameRunning = true;
        my_cg.alpha = 1;
        my_cg.interactable = true;
        obs = obstacle.GetComponentsInChildren<RectTransform>();
        pBar.text = "0%";
        bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
        bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(244.5f, 0.91f, 90);
        obs_list.Add(Instantiate(obstacle_prefab, parent: obstacle.GetComponent<RectTransform>()));
        StartCoroutine(Progression());
        StartCoroutine(SpawnObstacle());
        window.OpenWindow();

        if (startSound != null && soundEffectSource != null)
        {
            soundEffectSource.PlayOneShot(startSound);
        }

        // Start looping sound effect after a delay  
        if (loopSoundEffect != null)
        {
            StartCoroutine(PlayLoopSoundWithDelay());
        }
    }

    private IEnumerator PlayLoopSoundWithDelay()
    {
        yield return new WaitForSeconds(0.75f); // Delay for 1 second  
        loopSoundEffect.loop = true;
        loopSoundEffect.Play();
    }

    public void OnEnable()
    {
        pscript.OnTObs += PlayerHit;
        PopupManager.PopupCanContinue += () => popupContinue = true;
        PopupManager.PopupCantContinue += () => popupContinue = false;
        LockdownManager.LockdownCanContinue += () => lockdownContinue = true;
        LockdownManager.LockdownCantContinue += () => lockdownContinue = false;
    }

    public void OnDisable()
    {
        pscript.OnTObs -= PlayerHit;
    }

    public void PlayerHit(Collider2D ob_hit)
    {
        obs_list.Remove(ob_hit.gameObject);
        Destroy(ob_hit.gameObject);
        pCount -= difficulty_p_reduction;
        pCount = pCount < 0 ? 0 : pCount;

        // Play a random sound effect from the array
        if (obstacleHitSounds != null && obstacleHitSounds.Length > 0 && soundEffectSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, obstacleHitSounds.Length);
            soundEffectSource.PlayOneShot(obstacleHitSounds[randomIndex]);
        }
    }

    public bool gameRunning = false;

    // Update is called once per frame  
    void Update()
    {
        if (gameRunning)
        {
            // Gotta loop because ideal scrolling leaves 2 bg instances in scene  
            // at once.  
            foreach (var b in bgs)
            {
                try
                {
                    b.GetComponent<RectTransform>().anchoredPosition -= speed * Time.deltaTime * 2;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            // Stop input if popup/lockdown is active  
            if (popupContinue && lockdownContinue)
            {
                Vector2 moveValue = moveAction.ReadValue<Vector2>();
                player.anchoredPosition +=
                    new Vector2(moveValue.x * 3, moveValue.y * 8) * Time.deltaTime * 100;

                // Adjust loop sound pitch based on movement input  
                if (loopSoundEffect != null)
                {
                    float pitch = Mathf.Clamp(1 + moveValue.magnitude * 0.5f, 0.8f, 2.0f);
                    loopSoundEffect.pitch = pitch;
                }
            }
            CheckBounds();
            HandleObs();
        }
    }

    void CheckBounds()
    {
        // Gave this function the copilot special, seems to work the same with slightly better performance
        // Player bounds, so they can't go off screen
        player.anchoredPosition = new Vector2(
            Mathf.Clamp(player.anchoredPosition.x, -1700, 1700),
            Mathf.Clamp(player.anchoredPosition.y, -980, 900)
        );

        // Handles background scrolling instantiation and deletion
        if (bgs[^1].GetComponent<RectTransform>().anchoredPosition.x < -243)
        {
            bgs.Add(Instantiate(bg, parent: parent.GetComponent<RectTransform>()));
            bgs[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(643.8f, 0.91f, 90);
        }

        if (bgs.Count > 1 && bgs[^2].GetComponent<RectTransform>().anchoredPosition.x < -643)
        {
            Destroy(bgs[^2]);
            bgs.RemoveAt(bgs.Count - 2);
        }

        obs_list.RemoveAll(ob =>
        {
            if (ob.GetComponent<RectTransform>().anchoredPosition.x < -2300)
            {
                Destroy(ob);
                return true;
            }
            return false;
        });
    }

    void HandleObs()
    {
        foreach (var ob in obs_list)
        {
            ob.GetComponent<RectTransform>().anchoredPosition -= ob_speed * Time.deltaTime * speed;
        }
    }

    public IEnumerator Progression()
    {
        while (pCount < 100)
        {
            pCount++;
            pBar.text = pCount + "%";
            yield return new WaitForSeconds(0.33f);
        }
        GameEnd();
        OnGameEnd?.Invoke();
    }

    public IEnumerator SpawnObstacle()
    {
        while (true)
        {
            System.Random rnd = new();
            int to_spawn = rnd.Next(1, ob_max_spawn);
            for (int i = 0; i < to_spawn; i++)
            {
                float y_pos = rnd.Next(-1200, 1200);
                float x_pos = rnd.Next(2300, 6000);
                float spawn_time = rnd.Next((int)ob_spawntime, (int)(1.5f * ob_spawntime));
                yield return new WaitForSeconds(spawn_time);
                obs_list.Add(
                    Instantiate(obstacle_prefab, parent: obstacle.GetComponent<RectTransform>())
                );
                obs_list[^1].GetComponent<RectTransform>().anchoredPosition = new Vector3(
                    x_pos,
                    y_pos,
                    0
                );
            }
        }
    }

    public void GameEnd()
    {
        foreach (var b in bgs)
        {
            Destroy(b);
        }
        gameRunning = false;
        bgs.RemoveRange(0, bgs.Count);
        foreach (var ob in obs_list)
        {
            Destroy(ob);
        }
        StopCoroutine(Progression());
        StopCoroutine(SpawnObstacle());
        boundingBox.SetActive(false);
        window.CloseWindow();

        // Stop looping sound effect
        if (loopSoundEffect != null)
        {
            loopSoundEffect.Stop();
        }
    }
}
