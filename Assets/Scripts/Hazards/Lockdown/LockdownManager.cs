using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockdownManager : AbstractManager
{
    [SerializeField] private float minDelay = 5.0f;
    [SerializeField] private float maxDelay = 15.0f;
    [SerializeField] private LockdownCanvas lockdownCanvas; // Reference to LockdownCanvas
    private bool hazardStarted = false;

    public static event Action LockdownCanContinue;
    public static event Action LockdownCantContinue;


    // Start is called before the first frame update
    void Start()
    {
        // Ensure the lockdown canvas is assigned
        if (lockdownCanvas == null)
        {
            Debug.LogError("LockdownCanvas is not assigned.");
        }
    }

    public override void StartHazard()
    {
        Debug.Log("Starting Lockdown Hazard");
        StartLockdownDelayTimer();
    }

    public void StartLockdownDelayTimer()
    {
        if (lockdownCanvas != null && !hazardStarted)
        {
            hazardStarted = true;
            StartCoroutine(ActivateRaycastBlockerAfterDelay());
        }
    }

    private IEnumerator ActivateRaycastBlockerAfterDelay()
    {
        yield return new WaitForSeconds(GetDelay());
        lockdownCanvas.OpenCanvas();
        LockdownCantContinue?.Invoke();
        StartCoroutine(CheckLockdownCanvasComplete());
    }

    private IEnumerator CheckLockdownCanvasComplete()
    {
        while (!lockdownCanvas.isComplete)
        {
            yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
        }
        CanvasClosed();
    }

    public override void StopHazard()
    {
        StopAllCoroutines();
        lockdownCanvas.CloseCanvas();
        hazardStarted = false;
    }

    public override bool CanProgress()
    {
        // Check if the minigame is completed
        return !lockdownCanvas.loadingScript.isLoaded;
    }

    public void CanvasClosed()
    {
        lockdownCanvas.ResetLoading();
        lockdownCanvas.CloseCanvas();
        hazardStarted = false;
        LockdownCanContinue?.Invoke();
        if (difficulty != OSManager.Difficulty.Easy)
            StartLockdownDelayTimer();
        else
            StopHazard();
    }

    public float GetDelay()
    {
        if (difficulty == OSManager.Difficulty.Medium)
        {
            return UnityEngine.Random.Range(minDelay / 2, maxDelay / 2);
        }
        else if (difficulty == OSManager.Difficulty.Hard)
        {
            return UnityEngine.Random.Range(minDelay / 3, maxDelay / 3);
        }
        return UnityEngine.Random.Range(minDelay, maxDelay);
    }
}
