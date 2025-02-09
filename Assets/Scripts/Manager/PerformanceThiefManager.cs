using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PerformanceThiefManager : AbstractManager
{
    public static event Action PThiefStarted;
    public static event Action<float> PThiefUpdate;
    public static event Action PThiefEnded;
    public float pModifier = 0.5f;
    public float pTime = 5f;
    public bool isActive = false;
    public int random;

    public void Awake() {
        UnityEngine.Random.InitState(System.Environment.TickCount);
        random = UnityEngine.Random.Range(0, 10);
    }

    public override bool CanProgress()
    {   
        return true;
    }

    public override void StartHazard()
    {
        isActive = true;
        Debug.Log("Performance Thief Started.");
        PThiefStarted?.Invoke();
        StartCoroutine(RunPerformanceThief());
    }

    // Basically going to have the performance thief ramp up as it runs for longer,
    // encouraging the player to not ignore it
    IEnumerator RunPerformanceThief()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(pTime);
            PThiefUpdate?.Invoke(pModifier);
            if(UnityEngine.Random.Range(0, 10) == random && pModifier >= 0.005)
            {
                pModifier -= 0.01f;
            }
        }
    }

    public override void StopHazard()
    {
        if(isActive){
            Debug.Log("Performance Thief Ended.");
            PThiefEnded?.Invoke();
            StopAllCoroutines();
            isActive = false;
        }
        else 
        {
            Debug.Log("Performance Thief Already Stopped");
        }
    }

    public void UpdatePerformanceThief(float modifier)
    {
        pModifier = modifier;
    }

    public void BIOSPerformanceHandler(int index) {
        switch (index) {
            case 0: Debug.Log("Case 0"); break;
            case 1: Debug.Log("Case 1"); 
                UpdatePerformanceThief(0.75f);
                StopAllCoroutines();
                PThiefUpdate?.Invoke(pModifier);
                    break;
            case 2: {
                UpdatePerformanceThief(1f);
                StopAllCoroutines();
                PThiefUpdate?.Invoke(pModifier);
                    break;
            }
            default: break;
        }
    }

    // This is a method that will be called by the BIOSManager to stop the performance thief
    public void StopProcess()
    {
        StopHazard();
    }
}
