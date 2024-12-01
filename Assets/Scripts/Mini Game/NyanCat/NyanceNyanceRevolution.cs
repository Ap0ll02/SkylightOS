using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Random = UnityEngine.Random;

/// <summary>
/// Author Quinn Contaldi
/// FIRST NOTE!!!!!!! THIS IS A SINGLETON CLASS. There should only be one!
/// Nyan cat boss is a rhythem game and the short version of the song will be used
/// Thus the arrows will spawn on half the nyan cat BPM 72 arrows per minute for game jurnalisim mode but 142 for real gamers
/// Different Types of arrows will be spawned thus a generic arrow class is used
/// </summary>
public class NyanceNyanceRevolution : AbstractBossTask
{
    // This will change the amount of arrows that will spawn  
    public static int max = 135;
    // This controls the scoring variables, which will affect how many items you can shake out of Nyan cat 
    public int winGood = 4000;
    public int winGreat = 6000;
    public int winPerfect = 7000;

    // Makes sure we dont exceed our arrow count  
    private int arrowCount = 0;

    // We Have to score the player!
    public int playerScore;

    // Currently we are using 4 different types of arrows
    // This is where the different types of arrow prefabs will be held.This will be invoked to give a new arrow its prefab before instanction 
    public GameObject[] arrowsTypesArray = new GameObject[4];

    // This is what plays the cool explosions when the player hits the arrow. We got different types so they are shoved in an array
    public GameObject[] ExplosionFeedbackPrefab = new GameObject[4];
    public GameObject[] ExplosionFeedback = new GameObject[4];

    // This will be the animator for the lines that will be spawned and will be used to show progression of the level
    public GameObject[] LazerPrefab = new GameObject[5];
    public GameObject[] Lazer = new GameObject[5];

    // Score Text Prefabs that will allow us to give some cool player feedback! Very important for the juice
    public GameObject[] scoreTextPrefab = new GameObject[4];
    public GameObject[] scoreText = new GameObject[4];

    // We need to spawn in our Check Arrows Prefab so the players can reference the arrows for score i.e these are the arrows used to compare for scoring metric 
    public GameObject[] checkArrowsPrefab = new GameObject[4];
    public GameObject[] checkArrows = new GameObject[4];

    public GameObject canvasPrefab;
    public GameObject canvas;
    public Transform CanvasTransform;
    // This provides us with the ending text once we beat Nyan Cat. 
    public GameObject endConditionPrefab;
    public GameObject endCondition = null;
    // Currently not used 
    public GameObject damageTextPrefab;
    
    // This is for our flying Nyan Cat how cool!
    public GameObject NyanCatPrefab;
    public GameObject NyanCat = null;

    // This will start the next stage of our game
    public GameObject struggleNyanCat;

    // This is the wonderful nyan cat music we play
    public AudioSource nyanCatSong;

    // This creates a public event for all of our keys
    public UnityEvent UpArrow;
    public UnityEvent DownArrow;
    public UnityEvent LeftArrow;
    public UnityEvent RightArrow;

    // All the colors we will use for scoring and end game condition text
    [SerializeField] public Color perfectColor;
    [SerializeField] public Color greatColor;
    [SerializeField] public Color goodColor;
    [SerializeField] public Color missColor;
    // We create a static variable so it only holds one NyanceNyanceRevalution variable at a time 
    private static NyanceNyanceRevolution NyanceNyanceRevolutionSingleton;

    // Its Contstructor is private so we can present others from instantiating the object.
    private NyanceNyanceRevolution() {}

    // We have a method that will enable others to get our singleton instance If you want to know how a singleton works let me know
    // Its hard to describe its behavior in comments, however it just ensures only one instance of it exist at a time!
    public static NyanceNyanceRevolution GetInstance()
    {
        if (NyanceNyanceRevolutionSingleton == null)
            NyanceNyanceRevolutionSingleton = FindObjectOfType<NyanceNyanceRevolution>();
        // if a NyanceNyanceRevalution has not been made yet we simply instantiate one
        if (NyanceNyanceRevolutionSingleton == null)
        {
            Debug.Log("Creating new NyanceNyanceRevolution instance");
            GameObject singletonNyanCat = new GameObject();
            NyanceNyanceRevolutionSingleton = singletonNyanCat.AddComponent<NyanceNyanceRevolution>();
            singletonNyanCat.name = typeof(NyanceNyanceRevolution).ToString();
        }

        // else we return the single instance we have 
        return NyanceNyanceRevolutionSingleton;
    }

    // We want to make sure everything is intalized before we start the game 
    private void Awake()
    {
        NyanceNyanceRevolutionSingleton = GetInstance();
        nyanCatSong = GetComponent<AudioSource>();
        // All these for loops spawn in the game items
        SpawnLazers();
        SpawnCheckArrows();
        SpawnExplosionFeedback();
        SpawnScoreText();
        endCondition = Instantiate(endConditionPrefab);
        NyanCat = Instantiate(NyanCatPrefab);
        canvas = Instantiate(canvasPrefab);
        CanvasTransform = canvas.transform;
    }

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(SpawnArrows());
        StartCoroutine(PlayMusic());
    }

    public void Update()
    {
        CheckForKeyPresses();
        UpdateLazers();
    }

    public override void startTask()
    {

    }

    // Ask the hazard manager if our task can progress
    // Idea use a percentage to slow down the task progress instead of completely stopping it
    public override void checkHazards()
    {
        //meow
    }

    // This will request the manager to stop / end a hazard
    public override void stopHazards()
    {
        //meow 
    }

    // this will request our manager to start making hazards
    public override void startHazards()
    {
        //meow
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
        for (int i = 0; i < checkArrowsPrefab.Length; i++)
        {
            checkArrows[i] = Instantiate(checkArrowsPrefab[i]);
        }
    }

    // This spawns in our prefabs and sets the checkArrows array equal to the instantiated object 
    public void SpawnScoreText()
    {
        for (int i = 0; i < scoreTextPrefab.Length; i++)
        {
            scoreText[i] = Instantiate(scoreTextPrefab[i],CanvasTransform);
        }
    }

    public void SpawnExplosionFeedback()
    {
        for (int i = 0; i < ExplosionFeedbackPrefab.Length; i++)
        {
            ExplosionFeedback[i] = Instantiate(ExplosionFeedbackPrefab[i]);
        }
    }

    // This coroutine spawns arrows at regular intervals until the maximum arrow count is reached.
    // *** BE VERY CAREFUL. This function utilizes inheritance from the Arrows class, coding to a generic type and having children specify their behavior.
    // I would not recommend changing this function until you understand how the inheritance is working within the Arrows class. To extend behavior,
    // go to the virtual Start function in the Arrows class and add additional functionality there. For this function to work, logical associations for the arrays must be followed.
    // 0 is Up, 1 is Down, 2 is Left, 3 is Right. Any additional keys or reworks should utilize logical associations.
    public IEnumerator SpawnArrows()
{
    // Continue spawning arrows until the arrow count reaches the maximum limit
    while (arrowCount < max)
    {
        // Generate a random index to select an arrow type from the array
        int randIndex = Random.Range(0, arrowsTypesArray.Length);
        
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
    yield break;
}

// We need a way to invoke our observers in order to update the states of our subjects.
// When this method is invoked, associated arrows will perform a score check.
// Remember this is the second step! And has multiple layers of abstraction so be careful and make sure you fully understand the implementation before changing it.
// These are Observer functions that will have listeners added to their function. These listeners are added via a logical association with array elements.
// Look at the SpawnArrows function. Notice that every array in each case has the same number. You need to store all necessary components this way.
// If you violate the logical association (Up is 0, Down is 1, Left is 2, Right is 3), the incorrect arrows will be assigned to incorrect events.
// Make sure to always have a consistent logical association with elements for arrows or any other input that will be spawned and checked.
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

        public void UpdateText(string scoreCondition, GameObject TextPrefab)
    {
        if (TextPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a text prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            var textMesh = TextPrefab.GetComponent<TextMeshProUGUI>();
            var ScoringTextAnimator = TextPrefab.GetComponent<Animator>();
            switch (scoreCondition)
            {
                case "Perfect":
                    textMesh.text = "PERFECT!";
                    textMesh.color = perfectColor;
                    ScoringTextAnimator.SetTrigger("Perfect");
                    break;
                case "Great":
                    textMesh.text = "GREAT";
                    textMesh.color = greatColor;
                    ScoringTextAnimator.SetTrigger("Great");
                    break;
                case "Good":
                    textMesh.text = "GOOD";
                    textMesh.color = goodColor;
                    ScoringTextAnimator.SetTrigger("Good");
                    break;
                case "Miss":
                    textMesh.text = "MISS";
                    textMesh.color = missColor;
                    ScoringTextAnimator.SetTrigger("Miss");
                    break;

            }
        }
    }

// This function will cause the explosion feedback animation to play based on user score 
    public void UpdateExplosion(string explosionCondition, GameObject explosionPrefab)
    {
        // Check if the ExplosionPrefab is null
        if (explosionPrefab == null)
        {
            Debug.LogWarning("Trying to pull from an Explosion prefab, not an instantiated object. Have you tried putting the prefab in the scene?");
        }
        else
        {
            // Notice we are setting the triggers of variables stored inside the corresponding animation controller
            var explosionAnimator = explosionPrefab.GetComponent<Animator>();
        
            // Set the appropriate trigger based on the explosion condition
            switch (explosionCondition)
            {
                case "Perfect":
                    explosionAnimator.SetTrigger("Perfect");
                    break;
                case "Great":
                    explosionAnimator.SetTrigger("Great");
                    break;
                case "Good":
                    explosionAnimator.SetTrigger("Good");
                    break;
                default:
                    break;
            }
        }
    }

    // Full power lazers turns off the previous on in the animation controller then enables the next lazer 
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

    // This function is responsible for incrementing the behavior of the lazers so they increase with the amount of arrows that go by
    public void UpdateLazers()
    {
        switch (arrowCount)
        {
            case 5:

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

    // This is not active as of yet. I may use it later, as of now I will leave it unactive. 
    public void showDamageText(int value)
    {
        if (damageTextPrefab == null)
        {
            Debug.LogError("We are trying to call componentes from an uninstantated object. Drag DamageText prefab into scene then into our damaged text object into nyancatbosstask");
        }
        var textMesh = damageTextPrefab.GetComponent<TextMeshProUGUI>();
        textMesh.text = value.ToString();
        // Assign the new material to the TextMeshProUGUI component

        // Set the alpha value of the text color
        switch (value)
        {
            case 25:
                textMesh.color = goodColor;
                break;
            case 50:
                textMesh.color = greatColor;
                break;
            case 100:
                textMesh.color = perfectColor;
                break;
        }
        // Spawns damage text where Nyan Cat is currently located 
        Instantiate(damageTextPrefab, NyanCat.transform.position, Quaternion.identity, NyanCat.transform);
    }
    
    // This function simply sets all of the Arrows off 

    public void LazerOff()
    {
        foreach (var laser in Lazer)
        {
            Destroy(laser);
        }
    }
    public void CheckArrowsOff()
    {
        foreach (var checkArrow in checkArrows)
        {
            Destroy(checkArrow);
        }
    }

    public void ExplosionFeedbackOff()
    {
        foreach (var explosionFeedback in ExplosionFeedback)
        {
            Destroy(explosionFeedback);
        }
    }

    public void ScoreTextOff()
    {
        foreach (var text in scoreText)
        {
            Destroy(text);
        }
    }
    // This function checks for the win condition and spawns the corresponding text based on score 
    void CheckWin()
    {
        // If it is Null we have a Problem
        if (endConditionPrefab == null)
        {
            Debug.LogError("I would Recommend you put the textPrefab in the scene, just set it inactive");
        }
        else
        {
            // We need to grab the relevant text component from the Instantiated prefab 
            var text = endConditionPrefab.GetComponent<TextMeshPro>();
            // Check if player score is between winGood and winGreat
            if (playerScore >= winGood && playerScore < winGreat)
            {
                // the tags like shake and a are behavior tags shake causes text to shake a is the amplitude of the shake
                text.text = "<shake a=0.05>GOOD</shake>";
                text.color = goodColor;
                endConditionPrefab.SetActive(true);
            }
            // Check if player score is between winGreat and winPerfect
            else if(playerScore >= winGreat && playerScore <= winPerfect)
            {
                text.text = "<shake a=0.2>GREAT</shake>";
                text.color = greatColor;
                endConditionPrefab.SetActive(true);
            }
            // Check if player score is greater than or equal to winPerfect
            else if (playerScore >= winPerfect)
            {
                text.text = "<Incr a=0.5><shake a=0.2>PERFECT</shake></incr>";
                text.color = perfectColor;
                endConditionPrefab.SetActive(true);
            }
            // If player score is less than winGood then we lose
            else
            {
                text.text = "<bounce a=0.02>Game Over!!!!</bounce>";
                text.color = missColor;
                endConditionPrefab.SetActive(true);
            }
        }
        // destroy all of our game objects 
        LazerOff();
        CheckArrowsOff();
        ExplosionFeedbackOff();
        ScoreTextOff();
        StartCoroutine(ShakeNyanCat());
    }

    // A simple coroutine to play are music
    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(2.5f);
        nyanCatSong.Play();
    }

    // This function just gives a time bit of time for the rest of the arrows to pass by before calling check win
    IEnumerator ShowWinText()
    {
        yield return new WaitForSeconds(4f);
        CheckWin();
    }

    IEnumerator ShakeNyanCat()
    {
        yield return new WaitForSeconds(4f);

    }

    public void StruggleCat()
    {
        Destroy(NyanCat);
        Destroy(endCondition);
        Instantiate(struggleNyanCat);
        gameObject.SetActive(false);
    }
    
}
