using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

using Random = UnityEngine.Random;

/// <summary>
/// Author Quinn Contaldi
/// FIRST NOTE!!!!!!! THIS IS A SINGLETON CLASS. There should only be one!
/// Nyan cat boss is a rhythem game and the short version of the song will be used
/// Thus the arrows will spawn on half the nyan cat BPM 72 arrows per minute
/// Different Types of arrows will be spawned thus a generic arrow class is used
/// </summary>
public class NyanceNyanceRevolution : AbstractBossTask
{
    // This will change the amount of arrows that will spawn  
    public static int max = 142;

    // Makes sure we dont exceed our arrow count  
    private int arrowCount = 0;

    // We Have to score the player!
    public int playerScore;

    // Currently we are using 4 different types of arrows
    // This is where the different types of arrow prefabs will be held.This will be invoked to give a new arrow its prefab before instanction 
    public GameObject[] arrowsTypesArray = new GameObject[4];

    // This is what plays the cool explosions when the player hits the arrow. We got different types so they are shoved in an array
    public GameObject[] ExplosionFeedbackPrefab = new GameObject[4];

    // This will be the animator for the lines that will be spawned and will be used to show progression of the level
    public GameObject[] LazerPrefab = new GameObject[5];

    // Score Text Prefabs that will allow us to 
    public GameObject[] scoreTextPrefab = new GameObject[4];

    public GameObject damageTextPrefab; 
    public GameObject NyanCat;


    // This creates a public event for all of our keys
    public UnityEvent UpArrow;
    public UnityEvent DownArrow;
    public UnityEvent LeftArrow;
    public UnityEvent RightArrow;


    [SerializeField] public Color perfectColor;
    [SerializeField] public Color greatColor;
    [SerializeField] public Color goodColor;
    [SerializeField] public Color missColor;
    // We create a static variable so it only holds one NyanceNyanceRevalution variable at a time 
    private static NyanceNyanceRevolution NyanceNyanceRevolutionSingleton;

    // Its Contstructor is private so we can present others from instantiating the object.
    private NyanceNyanceRevolution()
    {
    }

    // We have a method that will enable others to get our singleton instance 
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

    private void Awake()
    {
        NyanceNyanceRevolutionSingleton = GetInstance();
    }

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine("SpawnArrows");

    }

    public void Update()
    {
        CheckForKeyPresses();
        UpdateLazers();
    }

    public override void startTask()
    {
        //meow
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

    public IEnumerator SpawnArrows()
    {
        while (arrowCount < max)
        {
            int randIndex = Random.Range(0, arrowsTypesArray.Length);
            GameObject newArrowObject = Instantiate(arrowsTypesArray[randIndex]);
            Arrows newArrow = newArrowObject.GetComponent<Arrows>();

            switch (randIndex)
            {
                case 0:
                    newArrow.ArrowEvent = UpArrow;
                    newArrow.explosionPrefab = ExplosionFeedbackPrefab[0];
                    newArrow.scoringTextPrefab = scoreTextPrefab[0];
                    UpArrow.AddListener(newArrow.ScoreCheck);
                    break;
                case 1:
                    newArrow.ArrowEvent = DownArrow;
                    newArrow.explosionPrefab = ExplosionFeedbackPrefab[1];
                    newArrow.scoringTextPrefab = scoreTextPrefab[1];
                    DownArrow.AddListener(newArrow.ScoreCheck);
                    break;
                case 2:
                    newArrow.ArrowEvent = LeftArrow;
                    newArrow.explosionPrefab = ExplosionFeedbackPrefab[2];
                    newArrow.scoringTextPrefab = scoreTextPrefab[2];
                    LeftArrow.AddListener(newArrow.ScoreCheck);
                    break;
                case 3:
                    newArrow.ArrowEvent = RightArrow;
                    newArrow.explosionPrefab = ExplosionFeedbackPrefab[3];
                    newArrow.scoringTextPrefab = scoreTextPrefab[3];
                    RightArrow.AddListener(newArrow.ScoreCheck);
                    break;
                default:
                    break;
            }

            arrowCount++;
            yield return new WaitForSeconds(0.43f);
        }

        Debug.Log("FinalSCORE: " + NyanceNyanceRevolutionSingleton.playerScore);
        yield break;
    }

    // We need a way to invoke our observers in order to update the states of our subjects, When method is invoked associated arrows will perform score check
    private void CheckForKeyPresses()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpArrow.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownArrow.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftArrow.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightArrow.Invoke();
        }
    }

    //Creates callback functions for key presses that will be defined in the arrow class 
    // Burnice Burnice Burnice Burnice GO GO, Burnice Burnice Burnice Burnice let them watch it burn!

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

    public void UpdateExplosion(string explosionCondition, GameObject ExplosionPrefab)
    {
        if (ExplosionPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Explosion prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            var explosionAnimator = ExplosionPrefab.GetComponent<Animator>();
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

    public void FirstPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var Lazer in LazerPrefab)
            {
                var lazerAnimator =  Lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("Basic1",true);
            }
        }
    }
    public void SecondPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var Lazer in LazerPrefab)
            {
                var lazerAnimator =  Lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("Basic1",false);
                lazerAnimator.SetBool("Basic2",true);
            }
        }
    }

    public void ThirdPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var Lazer in LazerPrefab)
            {
                var lazerAnimator =  Lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("Basic2",false);
                lazerAnimator.SetBool("GreatLazer",true);
            }
        }
    }

    public void FullPower()
    {
        if (LazerPrefab == null)
        {
            Debug.LogWarning("Trying to pull from a Lazer prefab, not a instantated object, Have you tried putting the prefab in the scene?");
        }
        else
        {
            foreach (var Lazer in LazerPrefab)
            {
                var lazerAnimator =  Lazer.GetComponent<Animator>();
                lazerAnimator.SetBool("GreatLazer",false);
                lazerAnimator.SetBool("NyanLazer",true);
            }
        }
    }

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
            case 100:
                FullPower();
                break;
        }
    }

    public void showDamageText(int value)
    {
        if (damageTextPrefab == null)
        {
            Debug.LogError("We are trying to call componentes from an uninstantated object. Drag DamageText prefab into scene then into our damaged text object into nyancatbosstask");
        }
        var textMesh = damageTextPrefab.GetComponent<TextMeshProUGUI>();
        textMesh.text = value.ToString();
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
        Instantiate(damageTextPrefab, NyanCat.transform.position, Quaternion.identity, NyanCat.transform);
    }
}
