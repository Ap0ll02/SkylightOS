using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
// Garrett Sharp
// to create a cool ass abstract task manager
// this is dependent on nothing
// I should really put this in the design document

public abstract class AbstractTask : MonoBehaviour
{
    OSManager.Difficulty difficulty;

    //dont forget to state your purpose
    public List<AbstractManager> hazardManagers;

    // Boolean checking if the task is complete or not
    public bool isComplete;

    // Event to notify when the task is completed
    public static event Action OnTaskCompleted;

    // Default title, should be given a better title for OS manager
    public string taskTitle = "Default title, please set in task";

    // Default description, should be changed
    public string taskDescription = "Default Description, please set in task";

    // Start is called before the first frame update
    public void Start()
    {
        if (hazardManagers == null)
            Debug.Log("No Hazard Managers");
    }

    // initiate our task
    public abstract void startTask();
    // Method to complete the task
    public virtual void CompleteTask()
    {
        OnTaskCompleted?.Invoke();
    }
    // Ask the hazard manager if our task can progress
    // Idea use a percentage to slow down the task progress instead of completely stopping it
    public abstract void checkHazards();
    // This will request the manager to stop / end a hazard
    public abstract void stopHazards();
    // this will request our manager to start making hazards
    public abstract void startHazards();

    public void SetDifficulty(OSManager.Difficulty difficulty)
    {
        this.difficulty = difficulty;
    }
}
