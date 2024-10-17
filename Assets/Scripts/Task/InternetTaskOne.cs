using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// Jack Ratermann
// Main Internet Task 1 Script
// Depends on Abstract Task
// Interacts with multiple UI elements

public class InternetTaskOne : AbstractTask
{
    [SerializeField] GameObject wifiPopUpMenu;
    [SerializeField] GameObject diagnosisMenu;
    bool i = false;

    public void Start()
    {
        wifiPopUpMenu.SetActive(false);
        diagnosisMenu.SetActive(false);
    }

    public void OnWifiBtnClick()
    {
        // Toggles Visibility With Button Press
        if ( i == false ) {
            wifiPopUpMenu.SetActive(true);
            i = true;
        }
        else
        {
            wifiPopUpMenu.SetActive(false);
            i = false;
        }
    }

    public void OnDiagnosisBtnClick()
    {
        // Toggles Visibility With Button Press
        if (i == false)
        {
            diagnosisMenu.SetActive(true);
            i = true;
        }
        else
        {
            diagnosisMenu.SetActive(false);
            i = false;
        }
    }

    // Initiates a task
    public override void startTask()
    {

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
}
