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
    public static int max = 75;
    // Makes sure we dont exceed our arrow count  
    private int arrowCount = 0;
    // This is where the different types of arrow prefabs will be held.This will be invoked to give a new arrow its prefab before instanction 
    public GameObject[] arrowsTypesArray = new GameObject[4];
    // This creates a public event 
    public static event Action<KeyCode> OnKeyPress;
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
            // Randomizes arrow spawns
            GameObject newArrow = Instantiate(arrowsTypesArray[((Random.Range(0,arrowsTypesArray.Length)))]);
            arrowCount++;
            yield return new WaitForSeconds(0.83f);
        }
        yield break;
    }
    //Creats callback functions for key presses that will be defined in the arrow class 
    private void CheckForKeyPresses()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnKeyPress?.Invoke(KeyCode.W);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnKeyPress?.Invoke(KeyCode.A);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnKeyPress?.Invoke(KeyCode.S);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnKeyPress?.Invoke(KeyCode.D);
        }
    }



}
