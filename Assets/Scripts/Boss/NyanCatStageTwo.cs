using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = Unity.Mathematics.Random;

public class NyanCatStageTwo : AbstractBossStage
{
    //This is the current arrow count
    private int arrowCount = 0;
    // This will change the amount of arrows that will spawn
    private int maxArrowCount = 135;
    public GameObject canvas;
    // The Flying Nyan Cat is for the second stage of our level
    public GameObject nyanCatFlyingPrefab;
    private GameObject nyanCatFlying;
    // This is the arrow array which will spawn the different arrow types
    public GameObject[] arrowsTypesArray = new GameObject[4];
    // This is what plays the cool explosions when the player hits the arrow. We got different types so they are shoved in an array
    public GameObject[] ExplosionFeedbackPrefab = new GameObject[4];
    private GameObject[] ExplosionFeedback = new GameObject[4];
    // This will be the animator for the lines that will be spawned and will be used to show progression of the level
    public GameObject[] LazerPrefab = new GameObject[5];
    private GameObject[] Lazer = new GameObject[5];
    public AudioSource nyanCatSong;
    // This handles the generation of the text prefabs
    public GameObject[] scoreTextPrefab = new GameObject[4];
    private GameObject[] scoreText = new GameObject[4];
    //This creates a public event for all of our keys
    public UnityEvent UpArrow;
    public UnityEvent DownArrow;
    public UnityEvent LeftArrow;
    public UnityEvent RightArrow;
    // This provides us with the ending text once we beat Nyan Cat.
    public GameObject endConditionPrefab;
    private GameObject endCondition = null;
    // These are the colors of the text that will be associated with scores
    [SerializeField] public Color perfectColor;
    [SerializeField] public Color greatColor;
    [SerializeField] public Color goodColor;
    [SerializeField] public Color missColor;

    void Start()
    {
        // NorthStar = GameObject.Find("Northstar");
        // northstar = NorthStar.GetComponent<Northstar>();
    }

    void Update()
    {
        // CheckForKeyPresses();
        // UpdateLazers();
    }

    public override void BossStartStage()
    {
        Debug.Log("Stage 2");
        //northstar.WriteHint("Hes is <incr>FIRING</incr> HIS <shake> LAZERS</shake> <waitfor=0.5> We got to stop those arrows from hitting our CPU",Northstar.Style.cold,true);
        nyanCatSong = GetComponent<AudioSource>();
        if (nyanCatSong == null)
            Debug.LogError("we cant instantiate nyanCatSong");
        SpawnLazers();
        SpawnCheckArrows();
        SpawnExplosionFeedback();
        SpawnScoreText();
        nyanCatFlying = Instantiate(nyanCatFlyingPrefab);
        //StartCoroutine(SpawnArrows());
        StartCoroutine(PlayMusic());
    }

    // This spawns in our prefabs and sets the checkArrows array equal to the instantiated object
    public void SpawnLazers()
    {
        for (int i = 0; i < LazerPrefab.Length; i++)
        {
            Lazer[i] = Instantiate(LazerPrefab[i]);
        }
    }

    // This spawns in our prefabs and sets the checkArrows array equal to the instantiated object
    public void SpawnCheckArrows()
    {
        // for (int i = 0; i < checkArrowsPrefab.Length; i++)
        // {
        //     checkArrows[i] = Instantiate(checkArrowsPrefab[i]);
        // }
    }

    // This spawns in our prefabs and sets the checkArrows array equal to the instantiated object
    public void SpawnScoreText()
    {
        for (int i = 0; i < scoreTextPrefab.Length; i++)
        {
            scoreText[i] = Instantiate(scoreTextPrefab[i],canvas.transform);
        }
    }

    public void SpawnExplosionFeedback()
    {
        for (int i = 0; i < ExplosionFeedbackPrefab.Length; i++)
        {
            ExplosionFeedback[i] = Instantiate(ExplosionFeedbackPrefab[i]);
        }
    }
    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(2.5f);
        nyanCatSong.Play();
    }
    IEnumerator ShowWinText()
    {
        yield return new WaitForSeconds(4f);
        CheckWin();
    }

    void CheckWin()
    {
        var winGood = bossManager.GetComponent<NyanCatBossManager>().winGood;
        var winGreat = bossManager.GetComponent<NyanCatBossManager>().winGreat;
        var winPerfect = bossManager.GetComponent<NyanCatBossManager>().winPerfect;
        endCondition = Instantiate(endConditionPrefab, canvas.transform);
        if (endConditionPrefab == null)
        {
            Debug.LogError("Failed to instantiate endConditionPrefab.");
            return;
        }
        else
        {
            // We need to grab the relevant text component from the Instantiated prefab
            var endText = endCondition.GetComponent<TextMeshProUGUI>();

            if (endText == null)
            {
                Debug.Log("endText right");
            }
            //Check if player score is between winGood and winGreat
            if (bossManager.GetComponent<NyanCatBossManager>().playerScore >= winGood && bossManager.GetComponent<NyanCatBossManager>().playerScore < winGreat)
            {
                // the tags like shake and a are behavior tags shake causes text to shake a is the amplitude of the shake
                endText.text = "<shake a=0.05>GOOD</shake>";
                endText.color = goodColor;
                //itemsCanAccess = itemsCanAccessGood;
            }
            // Check if player score is between winGreat and winPerfect
            else if(bossManager.GetComponent<NyanCatBossManager>().playerScore >= winGreat && bossManager.GetComponent<NyanCatBossManager>().playerScore <= winPerfect)
            {
                endText.text = "<shake a=0.2>GREAT</shake>";
                endText.color = greatColor;
                //itemsCanAccess = itemsCanAccessGreat;
            }
            // Check if player score is greater than or equal to winPerfect
            else if (bossManager.GetComponent<NyanCatBossManager>().playerScore >= winPerfect)
            {
                endText.text = "<incr a=0.5><shake a=0.2>PERFECT</shake></incr>";
                endText.color = perfectColor;
                //itemsCanAccess = itemsCanAccessPerfect;
            }
            // If player score is less than winGood then we lose
            else
            {
                endText.text = "<bounce a=0.02>Game Over!!!!</bounce>";
                endText.color = missColor;
                //itemsCanAccess = itemsCanAccessFail;
            }
        }
    }

    public void UpdateLazers()
    {
        switch (arrowCount)
        {
            case 5:
                FirstPower();
                break;
            case 40:
                SecondPower();
                break;
            case 70:
                ThirdPower();
                break;
            case 120:
                FullPower();
                break;
        }
    }

        public void FirstPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var lazer in Lazer)
            {
                var lazerAnimator =  lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("Basic1",true);
            }
        }
    }

    // Full power lazers turns off the previous on in the animation controller then enables the next lazer
    public void SecondPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var lazer in Lazer)
            {
                var lazerAnimator =  lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("Basic1",false);
                lazerAnimator.SetBool("Basic2",true);
            }
        }
    }

    // Full power lazers turns off the previous on in the animation controller then enables the next lazer
    public void ThirdPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var lazer in Lazer)
            {
                var lazerAnimator =  lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("Basic2",false);
                lazerAnimator.SetBool("GreatLazer",true);
            }
        }
    }

    // Full power lazers turns off the previous on in the animation controller then enables the next lazer
    public void FullPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var lazer in Lazer)
            {
                var lazerAnimator =  lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("GreatLazer",false);
                lazerAnimator.SetBool("NyanLazer",true);
            }
        }
    }

        // This coroutine spawns arrows at regular intervals until the maximum arrow count is reached.
        // *** BE VERY CAREFUL. This function utilizes inheritance from the Arrows class, coding to a generic type and having children specify their behavior.
        // I would not recommend changing this function until you understand how the inheritance is working within the Arrows class. To extend behavior,
        // go to the virtual Start function in the Arrows class and add additional functionality there. For this function to work, logical associations for the arrays must be followed.
        // 0 is Up, 1 is Down, 2 is Left, 3 is Right. Any additional keys or reworks should utilize logical associations.
        private void CheckForKeyPresses()
        {
            // Check if the W key or UpArrow key is pressed
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpArrow.Invoke(); // Invoke the UpArrow event
            }

            // Check if the S key or DownArrow key is pressed
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                DownArrow.Invoke(); // Invoke the DownArrow event
            }

            // Check if the A key or LeftArrow key is pressed
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LeftArrow.Invoke(); // Invoke the LeftArrow event
            }

            // Check if the D key or RightArrow key is pressed
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                RightArrow.Invoke(); // Invoke the RightArrow event
            }
        }

        // This coroutine updates the position of the lazers and checks for collisions with the player.
        public IEnumerator SpawnArrows()
        {
            // Continue spawning arrows until the arrow count reaches the maximum limit
            while (arrowCount < maxArrowCount)
            {
                // Generate a random index to select an arrow type from the array
                int randIndex = UnityEngine.Random.Range(0, arrowsTypesArray.Length);

                // Instantiate a new arrow object based on the randomly selected arrow type
                GameObject newArrowObject = Instantiate(arrowsTypesArray[randIndex]);

                // Get the Arrows component from the newly instantiated arrow object
                Arrows newArrow = newArrowObject.GetComponent<Arrows>();

                // Set the appropriate event, explosion prefab, and scoring text prefab based on the random index
                switch (randIndex)
                {
                    case 0:
                        newArrow.ArrowEvent = UpArrow;
                        newArrow.explosionPrefab = ExplosionFeedback[0];
                        newArrow.scoringTextPrefab = scoreText[0];
                        UpArrow.AddListener(newArrow.ScoreCheck);
                        break;
                    case 1:
                        newArrow.ArrowEvent = DownArrow;
                        newArrow.explosionPrefab = ExplosionFeedback[1];
                        newArrow.scoringTextPrefab = scoreText[1];
                        DownArrow.AddListener(newArrow.ScoreCheck);
                        break;
                    case 2:
                        newArrow.ArrowEvent = LeftArrow;
                        newArrow.explosionPrefab = ExplosionFeedback[2];
                        newArrow.scoringTextPrefab = scoreText[2];
                        LeftArrow.AddListener(newArrow.ScoreCheck);
                        break;
                    case 3:
                        newArrow.ArrowEvent = RightArrow;
                        newArrow.explosionPrefab = ExplosionFeedback[3];
                        newArrow.scoringTextPrefab = scoreText[3];
                        RightArrow.AddListener(newArrow.ScoreCheck);
                        break;
                    default:
                        break;
                }

                // Increment the arrow count
                arrowCount++;

                // Wait for a specified duration before spawning the next arrow
                yield return new WaitForSeconds(0.43f);
            }

            // Start the coroutine to show the win text after all arrows have been spawned
            StartCoroutine(ShowWinText());
        }

    public override void BossEndStage()
    {
        // LazerOff();
        // CheckArrowsOff();
        // ExplosionFeedbackOff();
        // ScoreTextOff();
        // Destroy(nyanCatFlying);
    }
}
