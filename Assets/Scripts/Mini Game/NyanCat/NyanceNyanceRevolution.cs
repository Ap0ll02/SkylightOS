using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
    public static int max = 70;
    // Makes sure we dont exceed our arrow count  
    private int arrowCount = 0;
    // We Have to score the player!
    public int playerScore;
    // Currently we are using 4 different types of arrows
    // This is where the different types of arrow prefabs will be held.This will be invoked to give a new arrow its prefab before instanction 
    public GameObject[] arrowsTypesArray = new GameObject[4];
    // This creates a public event for all of our keys
    public UnityEvent UpArrow;
    public UnityEvent DownArrow;
    public UnityEvent LeftArrow;
    public UnityEvent RightArrow;
    // We create a static variable so it only holds one NyanceNyanceRevalution variable at a time 
    private static NyanceNyanceRevolution NyanceNyanceRevolutionSingleton;
    // Its Contstructor is private so we can present others from instantiating the object.
    private NyanceNyanceRevolution() {}
    // We have a method that will enable others to get our singleton instance 
    public static NyanceNyanceRevolution GetInstance()
    {
        if(NyanceNyanceRevolutionSingleton == null)
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
                    Debug.Log("Spawning UpArrow");
                    newArrow.ArrowEvent = UpArrow;
                    UpArrow.AddListener(newArrow.ScoreCheck);
                    break;
                case 1:
                    Debug.Log("Spawning DownArrow");
                    newArrow.ArrowEvent = DownArrow;
                    DownArrow.AddListener(newArrow.ScoreCheck);
                    break;
                case 2:
                    Debug.Log("Spawning LeftArrow");
                    newArrow.ArrowEvent = LeftArrow;
                    LeftArrow.AddListener(newArrow.ScoreCheck);
                    break;
                case 3:
                    Debug.Log("Spawning RightArrow");
                    newArrow.ArrowEvent = RightArrow;
                    RightArrow.AddListener(newArrow.ScoreCheck);
                    break;
                default:
                    break;
            }

            arrowCount++;
            yield return new WaitForSeconds(0.83f);
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
}
