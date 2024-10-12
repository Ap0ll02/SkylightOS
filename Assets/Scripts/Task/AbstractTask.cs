using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Garrett Sharp
// to create a cool ass abstract task manager
// this is dependent on nothing
// I should really put this in the design document

public abstract class AbstractTask : MonoBehaviour
{
    //dont forget to state your purpose
    public List<AbstractManager> hazardManagers;

    // Start is called before the first frame update
    void Start()
    {
        if (hazardManagers == null)
            Debug.Log("No Hazard Managers");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // initiate our task
    public abstract void startTask();
    // Ask the hazard manager if our task can progress
    // Idea use a percentage to slow down the task progress instead of completely stopping it
    public abstract void checkHazards();
    // This will request the manager to stop / end a hazard
    public abstract void stopHazards();
    // this will request our manager to start making hazards
    public abstract void startHazards();


}
