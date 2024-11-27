
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static NyanceNyanceRevolution;

/// <summary>
/// Author Quinn Contaldi
/// This will serve as the parent class for all fucking arrows.
/// You may look at the arrow classes and be like wait... they are all the same can I just get ride of them?
/// Dont do that you will break the game. The arrows are all the same but they have different variables since they are inherited from the parent class.
/// They also have different behavior for their respective event and animator triggers set by NyanceNyanceRevolution to create different arrows. 
/// Also the child classes have different start methods that can set the variables for different scoring range, scoring values, animations, and arrow types.
/// If you want to change the scoring range, scoring values, animations, and arrow types you can do so in the child classes. just set them after the base.start() call.
/// Also this design allows our game to be closed to modification but open to extension. you can define more behaviors for the children in their respective class
/// </summary>
public class Arrows : MonoBehaviour
{
    //current speed of the Arrows 
    public float speed = 1;

    // Simply checks if we are out of bounds
    public float outOfBounds = 4.8f;

    // our arrows need to have a score for them to return 
    public int highScore = 100;
    public int greatscore = 50;

    public int goodscore = 25;

    // Holds the arrow events we want to unsubscribe too
    public UnityEvent ArrowEvent;

    // This holds the animator for the explosion!
    public GameObject explosionPrefab;

    // Will be given associated scoring text that will pop up during the game
    public GameObject scoringTextPrefab;

    // Holds the NyanceNyanceReveloution singelton class so we can update score and do other sick shit
    public NyanceNyanceRevolution NyanceNyanceRevolutionSingleton;

    // This all the variables that control the scoring range, they can be adjusted in the child start classes UNDER base.start() 
    public float perfectTop = 4.6f;
    public float perfectBottom = 4.4f;
    public float greatTop = 4.4f;
    public float greatBottom = 4.2f;
    public float goodTop = 4.1f;
    public float goodBottom = 3.9f;


    // Why not just use start in all the children? we want common initialization logic! thats pretty cool!
    protected virtual void Start()
    {
        // We need to get a reference to our singleton 
        NyanceNyanceRevolutionSingleton = NyanceNyanceRevolution.GetInstance();
    }

    public void Move()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    // This is the function that is evoked by the unity event as it is a subscriber to the input function. 
    // This function will also call the associated explosion animatior which provides player feedback
    // This will be called in the children and the child objects will provide the respective values for the shared varibles
    public void ScoreCheck()
    {
        float y = transform.position.y;

        if (y > perfectBottom && y <= perfectTop)
        {
            NyanceNyanceRevolutionSingleton.UpdateText("Perfect", scoringTextPrefab);
            NyanceNyanceRevolutionSingleton.UpdateExplosion("Perfect", explosionPrefab);
            NyanceNyanceRevolutionSingleton.playerScore += highScore;
            NyanceNyanceRevolutionSingleton.showDamageText(highScore);
            DestroyArrow();
        }
        else if (y > greatBottom && y <= greatTop)
        {
            NyanceNyanceRevolutionSingleton.UpdateText("Great", scoringTextPrefab);
            NyanceNyanceRevolutionSingleton.UpdateExplosion("Great", explosionPrefab);
            NyanceNyanceRevolutionSingleton.playerScore += greatscore;
            NyanceNyanceRevolutionSingleton.showDamageText(greatscore);
            DestroyArrow();
        }
        else if (y > goodBottom && y <= goodTop)
        {
            NyanceNyanceRevolutionSingleton.UpdateText("Good", scoringTextPrefab);
            NyanceNyanceRevolutionSingleton.UpdateExplosion("Good", explosionPrefab);
            NyanceNyanceRevolutionSingleton.playerScore += goodscore;
            NyanceNyanceRevolutionSingleton.showDamageText(goodscore);
            DestroyArrow();
        }
    }

    // This function will destroy our arrow
    public void DestroyArrow()
    {
        // We unsubscribe from the proper event, which we stored in the arrow event variable. This works with any arrow
        ArrowEvent.RemoveListener(ScoreCheck);
        // We poof the game object
        Destroy(gameObject);
    }

    // We need to seperate the out of bound function, if not it will only check out of bound when a key is pressed. That is not good
    public void OutOfBounds(float position)
    {
        if (position > outOfBounds)
        {
            NyanceNyanceRevolutionSingleton.UpdateText("Miss", scoringTextPrefab);
            DestroyArrow();
        }
    }

}




