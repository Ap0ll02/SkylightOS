using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockdownManager : AbstractManager
{
    [SerializeField] private float minDelay = 3.0f;
    [SerializeField] private float maxDelay = 10.0f;
    [SerializeField] private LockdownCanvas lockdownCanvas; // Reference to LockdownCanvas
    private bool hazardStarted = false;

    void Awake()
    {
        lockdownCanvas = GameObject.FindObjectOfType<LockdownCanvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the lockdown canvas is assigned
        if (lockdownCanvas == null)
        {
            Debug.LogError("LockdownCanvas is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void StartHazard()
    {
        if (lockdownCanvas != null && !hazardStarted)
        {
            hazardStarted = true;
            StartCoroutine(ActivateRaycastBlockerAfterDelay());
        }
    }

    private IEnumerator ActivateRaycastBlockerAfterDelay()
    {
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        lockdownCanvas.OpenCanvas();
        StartCoroutine(CheckLockdownCanvasComplete());
        // Start the minigame here
    }

    private IEnumerator CheckLockdownCanvasComplete()
    {
        while (!lockdownCanvas.isComplete)
        {
            yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
        }
        lockdownCanvas.ResetLoading();
        StopHazard();
    }

    public override void StopHazard()
    {
        lockdownCanvas.CloseCanvas();
        hazardStarted = false;
    }

    public override bool CanProgress()
    {
        // Check if the minigame is completed
        return !lockdownCanvas.loadingScript.canContinue;
    }
}
