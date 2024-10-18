using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Jack Ratermann
// Main Internet Task 1 Script
// Depends on Abstract Task
// Interacts with multiple UI elements

public class InternetTaskOne : AbstractTask
{
    // References to the extended wifi pop up menu, and the diagnosis menu.
    [SerializeField] GameObject wifiPopUpMenu;
    [SerializeField] GameObject diagnosisMenu;

    // More Closely Related References to Task 
    [SerializeField] Image progBarMask;

    // Booleans for when to allow menus to show
    bool wifiPopUp = false;
    bool diagnoseWindow = false;

    // Initialization
    public void Start()
    {
        wifiPopUpMenu.SetActive(false);
        diagnosisMenu.SetActive(false);
    }

    // Toggle Visibility Of Wifi Pop Up Menu
    public void OnWifiBtnClick()
    {
        // Toggles Visibility With Button Press
        if (wifiPopUp == false && !diagnosisMenu.activeSelf ) {
            wifiPopUpMenu.SetActive(true);
            wifiPopUp = true;
        }
        else
        {
            wifiPopUpMenu.SetActive(false);
            wifiPopUp = false;
        }
    }

    // Pull up diagnosis menu
    public void OnDiagnosisBtnClick()
    {
        // Toggles Visibility With Button Press
        if (diagnoseWindow == false)
        {
            diagnosisMenu.SetActive(true);
            wifiPopUp = false;
            wifiPopUpMenu.SetActive(false);
            diagnoseWindow = true;
        }
        else
        {
            diagnosisMenu.SetActive(false);
            diagnoseWindow = false;
        }
        startTask();
    }

    // Start Internet Task 1
    public override void startTask()
    {
        StartCoroutine(WifiDiagnosisTimer());
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

    private IEnumerator WifiDiagnosisTimer()
    {
        while (progBarMask.fillAmount < 1)
        {
            yield return new WaitForSeconds(0.05f);
            progBarMask.fillAmount += 0.001f;

        }
    }
}
