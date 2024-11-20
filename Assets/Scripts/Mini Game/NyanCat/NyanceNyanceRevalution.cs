using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// Author Quinn Contaldi
/// Nyan cat boss is a rhythem game and the short version of the song will be used
/// Thus the arrows will spawn on half the nyan cat BPM 72 arrows per minute
/// Different Types of arrows will be spawned thus a generic arrow class is used
/// </summary>
public class NyanceNyanceRevalution : AbstractBossTask
{
    // This will change the amount of arrows that will spawn  
    public static int max = 75;
    // Makes sure we dont exceed our arrow count  
    private int arrowCount = 0;
    // This is where the different types of arrow prefabs will be held.This will be invoked to give a new arrow its prefab before instanction 
    public GameObject[] arrowsTypesArray = new GameObject[4];
    // This creates a public event 
    public UnityEvent UpArrow = new UnityEvent();
    public UnityEvent DownArrow = new UnityEvent();
    public UnityEvent LeftArrow = new UnityEvent();
    public UnityEvent RightArrow = new UnityEvent();
    // This is the method to trigger the event
    
    // Start is called before the first frame update
    public void Awake()
    {

    }
    public void Start()
    {
        StartCoroutine("spawnArrows");

    }

    public void Update()
    {
        CheckForKeyPresses();
    }

    public override void startTask()
    {
        //meow
    }
    // Ask the hazard manager if our task can progress
    // Idea use a percentage to slow down the task progress instead of completely stopping it
    public override void checkHazards()
    {

    }
    // This will request the manager to stop / end a hazard
    public override void stopHazards()
    {

    }
    // this will request our manager to start making hazards
    public override void startHazards()
    {

    }

    public IEnumerator spawnArrows()
    {
        while(arrowCount < max)
        {
            // Unity Random.Range function is exclusive thus we have to add +1 then the amount of arrows we have
            int randIndex = (Random.Range(0, arrowsTypesArray.Length));
            // Randomizes arrow spawns the getComponents Arrows ensure we have an arrow
            GameObject newArrowObject = Instantiate(arrowsTypesArray[randIndex]);
            Arrows newArrow = newArrowObject.GetComponent<Arrows>();
            //subscribe to our proper event
            switch (randIndex)
            {
                // Cases have A logical connection to type of arrows spawned. Please do not mess with the array 0, is up 1, is down
                case 0:
                    UpArrow.AddListener(newArrow.ScoreCheck);
                    // Needs to hold a reference to the UpArrow event so we can unsubscribe from it
                    newArrow.arrowEvent = UpArrow;
                    break;
                case 1:
                    DownArrow.AddListener(newArrow.ScoreCheck);
                    // Needs to hold a reference to the UpArrow event so we can unsubscribe from it
                    newArrow.arrowEvent = DownArrow;
                    break;
                case 2:
                    LeftArrow.AddListener(newArrow.ScoreCheck);
                    // Needs to hold a reference to the UpArrow event so we can unsubscribe from it
                    newArrow.arrowEvent = LeftArrow;
                    break;
                case 3:
                    RightArrow.AddListener(newArrow.ScoreCheck);
                    // Needs to hold a reference to the UpArrow event so we can unsubscribe from it
                    newArrow.arrowEvent = RightArrow;
                    break;
                default:
                    break;
            }
            // We want to stop our Corutine once we reach the maximum amount of Arrows 
            arrowCount++;
            yield return new WaitForSeconds(0.83f);
        }
        yield break;
    }
    //Creates callback functions for key presses that will be defined in the arrow class 
    // Burnice Burnice Burnice Burnice GO GO, Burnice Burnice Burnice Burnice let them watch it burn!

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


}
