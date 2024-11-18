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
///  
/// </summary>
public class NyanceNyanceRevalution : AbstractBossTask
{
    // This will change the amount of arrows that will spawn  
    public static int max = 72;
    // Makes sure we dont exceed our arrow count  
    private int arrowCount = 0;
    // This is where the different types of arrow prefabs will be held.This will be invoked to give a new arrow its prefab before instanction 
    public GameObject[] arrowsTypesArray = new GameObject[4];
    private GameObject newArrow;

    
    // Start is called before the first frame update
    public void Awake()
    {

    }
    public void Start()
    {
        StartCoroutine("spawnArrows");
        
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
            newArrow = Instantiate(arrowsTypesArray[(Random.Range(0,arrowsTypesArray.Length))]);
            arrowCount++;
            yield return new WaitForSeconds(0.83f);
        }
        yield break;
    }

    public void inputChecker(Arrows arrow)
    {
        // This will check if the player has pressed the correct key
        // We will subscribe the arrow to the event and then check if the player has pressed the correct key
    }

    UnityEvent upArrowPressed = new UnityEvent();
    UnityEvent downArrowPressed = new UnityEvent();
    UnityEvent rightArrowPressed = new UnityEvent();
    UnityEvent leftArrowPressed = new UnityEvent();

}
