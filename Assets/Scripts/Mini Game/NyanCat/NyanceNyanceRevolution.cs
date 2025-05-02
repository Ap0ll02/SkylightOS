using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Febucci.UI; 
using Febucci.UI.Core;
using Random = UnityEngine.Random;
//using UnityEditor.PackageManager.UI;

/// <summary>
/// Author Quinn Contaldi
/// FIRST NOTE!!!!!!! THIS IS A SINGLETON CLASS. There should only be one!
/// This Singleton class is massive, The reason it packs so much inside of it, instead of creating a bunch of different classes is for several reasons
/// 1. We want to create a mini game that is as "drag and drop" as possible. The Singleton design pattern is perfect for this job
/// 2. We want to make sure that the boss is closed for modification, but open to extension. THe core functionality of stages are containined in Nyan cat
/// However it is very easy to add additional class references to the boss fight allowing for easy extension
/// 3. This one class should be the manager for each stage of the boss fight. All auxillary mechanics should be implimented in their own classes and referenced
/// here. Meaning this is the class that should be controlling all of the logic for the nyan cat boss fight. For Example the arrow class!
/// The arrows class is a generic class that will handle arrows spawning and behavior. However NyanceNyanceRevolution uses this arrow abstraction
/// for the logic of its game. Arrows will spawn half the nyan cat BPM 72 arrows per minute for baby waaaaaaa waaaaaa mode but 142 for real chad gamers
/// Different Types of arrows will be spawned, which have their own specific behavior, Nyan cat does not care about the specific implimentation
/// All it cares about is the generic abstracted arrow behavior, that an arrow exist, the arrow subscribes a key input event, and it has a score method.
/// TLDR NyanceNyanceRevolution should impliment abstract game logic, axulliary classes should impliment specific behavior.
/// </summary>
public class NyanceNyanceRevolution : MonoBehaviour
{
    // Must be assigned so different prefabs can be spawned in
    public GameObject canvas;
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
    // An array of all the dialogues Lines
    public string[] dialogueLines = new string[5];
    // This is the item list we use to shake all the items out of Nyan Cat
    public GameObject[] NyanCatItems = new GameObject[40];
    // This is the current stage we are in
    public int Stage;
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
    // Different dialogues we use through out the first stage thus we have strings we are going to pass to our dialogue function 
    public string textOne = "<rainb><shake a=0.5>MUHAHAHAHA!</rainb></shake><waitfor=0.5> I have been here since 2008. You know how many IT persons have tried... and <incr>failed!!!</incr> to remove me ";
    public string textTwo = "Seriously<waitfor=0.5>, You know how rude it is to try and close a process! The <rainb>Rainbow Sprinkle Gall</rainb> of you people. YOU shall face my <bounce a=0.05>revenge!</bounce>";
    public string textThree = "..... AND YOU WILL FACE MY REVENGE!";
    public string textFour = "....Really? come on... Why are the LAZERS NOT TURNING ON ...";
    public string textFive = "... THERE WE GO!... AS I WAS SAYING MUHAHAHA AND YOU WILL FACE MY REVENGE";
    public GameObject textBoxPrefab;
    public GameObject textBox;
    public GameObject bossTextPrefab;
    public GameObject bossText;
    public TextAnimator_TMP text;
    // This is the icon on the tool bar how cool!
    public GameObject nyanCatIcon;
    // This is the Nyan Cats that we will be using for our game
    // The Nyan Cat Idle is used for the first stage
    public GameObject nyanCatIdlePrefab;
    public GameObject nyanCatIdle;
    // The Flying Nyan Cat is for the second stage of our level
    public GameObject nyanCatFlyingPrefab;
    public GameObject nyanCatFlying;
    // The Struggling Nyan Cat is for the third and final stage where you shake out all his loot!
    public GameObject nyanCatStrugglingPrefab;
    public GameObject nyanCatStruggling;
    // These are the lines that you must cross to shake out that Nyan cat loot! 
    public GameObject Line1Prefab;
    public GameObject Line1;
    public Vector3 Line1Position;
    public bool Line1On = false;
    public GameObject Line2Prefab;
    public GameObject Line2;
    public Vector3 Line2Position;
    public bool Line2On = false;
    public bool northstarSpoke = false;
    // The index  into the Nyan Cat item array
    public int index = 0;
    // The amount of items that can be accessed this is set based on player performance 
    public int itemsCanAccess;
    public int itemsCanAccessPerfect = 40;
    public int itemsCanAccessGreat = 20;
    public int itemsCanAccessGood = 10;
    public int itemsCanAccessFail = 0;
    // The trash can used to dump nyan cat
    public GameObject trashCan;
    public GameObject newTrashCan;
    bool isTrashCanSpawned = false;
    // The typewriter refference used to control the typewriter
    public TypewriterByCharacter typewriter;
    // We want our wonderful AI to provide commentary 
    public GameObject NorthStar;
    public Northstar northstar;
    // This provides us with the ending text once we beat Nyan Cat. 
    public GameObject endConditionPrefab;
    public GameObject endCondition = null;
    // Currently not used 
    public GameObject damageTextPrefab;
    public GameObject damageText;
    // Nyan Cat song
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

    public BasicWindow evidenceWindow;

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
        NorthStar = GameObject.Find("Northstar");
        northstar = NorthStar.GetComponent<Northstar>();
        //canvas = GameObject.Find("BossCanvas");
        //StageOne();
    }


    public void StartNyanceNyanceRevolution()
    {
        StageOne();
    }

    // Start is called before the first frame update

    public void Update()
    {

        switch (Stage)
        {
            case 1:
                // If anything needs to be checked for the first stage we can do it here
                break;
            case 2:
                CheckForKeyPresses();
                UpdateLazers();
                break;
            case 3:
                FollowMouse();
                dropItem();
                break;
            case 4:
                EndGame();
                break;
        }
    }
    // This is stage one set up which will spawn all the items we need for the first stage 
    public void StageOne()
    {
        Stage = 1;
        northstar.WriteHint("Oh no... this is not good!!! You have to stop him", Northstar.Style.warm, true);

        // Ensure canvas is assigned
        if (canvas == null)
        {
            Debug.LogError("Canvas is not assigned.");
            return;
        }
        canvas.SetActive(true);

        // Instantiate bossTextPrefab and assign to bossText
        bossText = Instantiate(bossTextPrefab, canvas.transform);
        if (bossText == null)
        {
            Debug.LogError("Can't instantiate bossText");
            return;
        }

        // Instantiate nyanCatIdlePrefab and assign to nyanCatIdle
        if (nyanCatIdle == null)
            nyanCatIdle = Instantiate(nyanCatIdlePrefab);
        if (nyanCatIdle == null)
        {
            Debug.LogError("Can't instantiate NyanCatIdle");
            return;
        }

        // Get TextAnimator_TMP component from bossText
        text = bossText.GetComponent<TextAnimator_TMP>();
        if (text == null)
        {
            Debug.LogError("TextAnimator_TMP component is not assigned or missing on bossText");
            return;
        }

        // Instantiate textBoxPrefab and assign to textBox
        textBox = Instantiate(textBoxPrefab);
        if (textBox == null)
        {
            Debug.LogError("Can't instantiate textBox");
            return;
        }

        // Get TypewriterByCharacter component from bossText
        if (typewriter == null)
        {
            typewriter = bossText.GetComponent<TypewriterByCharacter>();
            if (typewriter == null)
            {
                Debug.LogError("TypewriterByCharacter component is not assigned or missing on bossText");
                return;
            }
        }

        // Start the dialogue coroutine
        StartCoroutine(playDialogue());
    }

    // This is the set up for stage two it only needs to be called once
    public void StageTwo()
    {
        Stage = 2;
        northstar.WriteHint("Hes is <incr>FIRING</incr> HIS <shake> LAZERS</shake> <waitfor=0.5> We got to stop those arrows from hitting our CPU",Northstar.Style.cold,true);
        StageOneOff();
        nyanCatSong = GetComponent<AudioSource>();
        if (nyanCatSong == null)
            Debug.LogError("we cant instantiate nyanCatSong");
        SpawnLazers();
        SpawnCheckArrows();
        SpawnExplosionFeedback();
        SpawnScoreText();
        nyanCatFlying = Instantiate(nyanCatFlyingPrefab);
        StartCoroutine(SpawnArrows());
        StartCoroutine(PlayMusic());
    }
    // This is the set up for the stage three it only needs to be called once 
    public void StageThree()
    {
        northstar.WriteHint("<bounce a=0.5>TAKE THAT CAT!</bounce> Now lets see what files this pesky cat has been stealing.<waitfor=0.5> Shake him across the red lines",Northstar.Style.cold,true);
        Stage = 3;
        StageTwoOff();
        nyanCatStruggling = Instantiate(nyanCatStrugglingPrefab);
        if (nyanCatStruggling == null)
            Debug.LogError("Cant instantiate NyanCatStruggling");
        Line1 = Instantiate(Line1Prefab);
        if (Line1 == null)
            Debug.LogError("Cant Instantiate Line 1");
        Line2 = Instantiate(Line2Prefab);
        if (Line2 == null)
            Debug.LogError("Cant instantiate Line 2!");
        var Lazer1transform = Line1.GetComponent<Transform>();
        var Lazer2transform = Line2.GetComponent<Transform>();
        Line1Position = Lazer1transform.position;
        Line2Position = Lazer2transform.position;
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
        //Instantiate(damageTextPrefab, NyanCat.transform.position, Quaternion.identity, NyanCat.transform);
    }
    // This snaps nyan cat to our mouse 
    void FollowMouse()
    {
        // His line retrieves the current position of the mouse cursor in screen space (measured in pixels). The Input.mousePosition property returns a Vector3 
        Vector3 mousePosition = Input.mousePosition;
        // We need to set the z Coordinante since its set to 0 thus we find the closest distance from the camera in which objects can be rendered aka nearClipPlane
        mousePosition.z = Camera.main.nearClipPlane; // Set this to the distance from the camera to the object
        // This line converts the mousePosition from screen space to world space. The ScreenToWorldPoint method takes a Vector3 is screen space and returns a Vector 3 in world space 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // This just places our Nyan Cat in the correct place 
        nyanCatStruggling.transform.position = worldPosition;
    }
    // We use this function to drop items from Nyan Cat 
    public void dropItem()
    {
        if (northstarSpoke == false)
        {
            northstar.WriteHint("Ha Now time to take out the <incr a=1.2>trash</incr>");
            northstarSpoke = true;
        }
        // (index < itemsCanAccess): Check if the current index is less than the number of items that can be accessed.
        // Check if the y-coordinate of the NyanCatStrugglingScript GameObject's position is greater than the y-coordinate of the lazer1 GameObject's position.
        // �(Lazer1On == false): Check if Lazer1On is false (i.e., the first laser is not on).
        if ((index < itemsCanAccess) && (nyanCatStruggling.transform.position.y > Line1Position.y) && (Line1On == false))
        {
            var spawnedItem = Instantiate(NyanCatItems[index]);
            spawnedItem.SetActive(true);
            Destroy(spawnedItem, 5.0f);
            index++;
            Line1On = true;
            Line2On = false;
        }

        // Since Laser2 is at a negative y thus we need to check if the position is less then this y
        // 
        if ((index < itemsCanAccess) && (nyanCatStruggling.transform.position.y < Line2Position.y) && (Line2On == false))
        {
            var spawnedItem = Instantiate(NyanCatItems[index]);
            spawnedItem.SetActive(true);
            Destroy(spawnedItem, 5.0f);
            index++;
            Line1On = false;
            Line2On = true;
        }

        if(index == 39)
        {
            evidenceWindow.OpenWindow();
        }

        if (index == itemsCanAccess)
        {
            northstar.WriteHint("TAKE THAT Nyan Cat <incr a=1.2>Shake</incr> Nyan Cat Across the two red lines");
            TrashCat();
        }
    }
    // Used to put the cat in the trash! that is pretty cool
    public void TrashCat()
    {
        if (isTrashCanSpawned == false)
        {
            isTrashCanSpawned = true;
            newTrashCan = Instantiate(trashCan);
        }
        else
        {
            // Calculate the distance for x and y coordinates only
            float distanceX = nyanCatStruggling.transform.position.x - newTrashCan.transform.position.x;
            float distanceY = nyanCatStruggling.transform.position.y - newTrashCan.transform.position.y;
            float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
            if (distance <= 2.0f)
            {
                // This will set the case equal to four which will be the ending case for the game. 
                Stage = 4; 
            }
        }
    }
    // This function simply sets all of the Arrows off 
    public void LazerOff()
    {
        foreach (var laser in Lazer)
        {
            Destroy(laser);
        }
    }
    // Turns off our check arrows
    public void CheckArrowsOff()
    {
        foreach (var checkArrow in checkArrows)
        {
            Destroy(checkArrow);
        }
    }
    // Turns off the explosion feed back 
    public void ExplosionFeedbackOff()
    {
        foreach (var explosionFeedback in ExplosionFeedback)
        {
            Destroy(explosionFeedback);
        }
    }

    // Turns off the score text
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
            // Check if player score is between winGood and winGreat
            if (playerScore >= winGood && playerScore < winGreat)
            {
                // the tags like shake and a are behavior tags shake causes text to shake a is the amplitude of the shake
                endText.text = "<shake a=0.05>GOOD</shake>";
                endText.color = goodColor;
                itemsCanAccess = itemsCanAccessGood;
            }
            // Check if player score is between winGreat and winPerfect
            else if(playerScore >= winGreat && playerScore <= winPerfect)
            {
                endText.text = "<shake a=0.2>GREAT</shake>";
                endText.color = greatColor;
                itemsCanAccess = itemsCanAccessGreat;
            }
            // Check if player score is greater than or equal to winPerfect
            else if (playerScore >= winPerfect)
            {
                endText.text = "<incr a=0.5><shake a=0.2>PERFECT</shake></incr>";
                endText.color = perfectColor;
                itemsCanAccess = itemsCanAccessPerfect;
            }
            // If player score is less than winGood then we lose
            else
            {
                endText.text = "<bounce a=0.02>Terrible Job!!!!</bounce>";
                endText.color = missColor;
                itemsCanAccess = itemsCanAccessFail;
            }
        }
        // Start Stage Three
        WaitWinText();
        StageThree();
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

    IEnumerator WaitWinText()
    {
        yield return new WaitForSeconds(3f);
    }
    // Corutine that will be used for the shake cat
    IEnumerator ShakeNyanCat()
    {
        yield return new WaitForSeconds(3f);

    }
    // Create a coroutine that will be used to itterate through each dialogue line 

    IEnumerator playDialogue()
    {
        dialogueLines = new string[] { textOne, textTwo, textThree, textFour, textFive };

        // Ensure typewriter is assigned
        if (typewriter == null)
        {
            Debug.LogError("TypewriterByCharacter component is not assigned.");
            yield break;
        }

        // Go into each string in the dialogueLines array
        foreach (var line in dialogueLines)
        {
            // Check if the line in the array is null
            if (line != null)
            {
                // Log the line being processed
                Debug.Log("Processing line: " + line);

                // Have our typewriter start typing out each word
                typewriter.ShowText(line);

                // This function will continue and wait until the typewriter is done showing text
                // It is asking itself if it is still typing; if so, continue the function
                // Once it reaches the end, it will return false and break our statement
                yield return new WaitUntil(() => typewriter.isShowingText == false);

                // We then wait for a couple of seconds before loading in the next string
                yield return new WaitForSeconds(0.5f);
            }
        }

        // Start stage two after the end of stage one
        StageTwo();
    }
    // Destroy all of the stage one game objects
    public void StageOneOff()
    {
        Destroy(bossText);
        Destroy(nyanCatIdle);
        Destroy(textBox);
    }
    // Destroy all of the stage two game objects
    public void StageTwoOff()
    {
        StageOneOff();
        LazerOff();
        CheckArrowsOff();
        ExplosionFeedbackOff();
        ScoreTextOff();
        Destroy(nyanCatFlying);
    }
    // Destroy all of the stage three game objects 
    public void StageThreeOff()
    {
        StageTwoOff();
    }
    // Add an event to trigger after the game ends
    public event Action OnGameEnd;

    // Modify the EndGame method to invoke the event
    public void EndGame()
    {
        Destroy(Line1);
        Destroy(Line2);
        Destroy(newTrashCan);
        Destroy(endCondition);
        Destroy(nyanCatStruggling);
        Destroy(nyanCatIcon);
        // Trigger the OnGameEnd event
        canvas.SetActive(false);
        OnGameEnd?.Invoke();
        gameObject.SetActive(false);

    }
}

