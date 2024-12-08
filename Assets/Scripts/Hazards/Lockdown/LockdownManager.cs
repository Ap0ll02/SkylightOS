using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockdownManager : AbstractManager
{
    [SerializeField] private GameObject raycastBlocker;
    [SerializeField] private float minDelay = 3.0f;
    [SerializeField] private float maxDelay = 10.0f;
    [SerializeField] private LockdownCanvas lockdownCanvas; // Reference to LockdownCanvas
    private bool hazardStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the raycast blocker is assigned
        if (raycastBlocker == null)
        {
            Debug.LogError("RaycastBlocker is not assigned.");
        }

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
        if (raycastBlocker != null && lockdownCanvas != null && !hazardStarted)
        {
            hazardStarted = true;
            StartCoroutine(ActivateRaycastBlockerAfterDelay());
        }
    }

    private IEnumerator ActivateRaycastBlockerAfterDelay()
    {
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        raycastBlocker.SetActive(true);
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
        if (raycastBlocker != null)
        {
            raycastBlocker.SetActive(false);
            hazardStarted = false;
            // End the minigame here
        }
    }

    public override bool CanProgress()
    {
        // Check if the minigame is completed
        bool test = raycastBlocker.activeSelf;
        return raycastBlocker == null || !raycastBlocker.activeSelf;
    }
}
