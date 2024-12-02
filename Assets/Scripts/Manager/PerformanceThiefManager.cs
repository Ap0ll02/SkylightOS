using System;
using System.Collections;
using UnityEngine;

public class PerformanceThiefManager : AbstractManager
{
    public static event Action PThiefStarted;
    public static event Action PThiefEnded;
    public float pTime = 1f;
    private Coroutine timerCR;
    public static event Action PThiefIDisable;
    public static event Action PThiefUpdateDelay;
    public bool isActive = false;
    public int random;

    public void Awake() {
        gameObject.SetActive(true);
        UnityEngine.Random.InitState(System.Environment.TickCount);
        random = UnityEngine.Random.Range(0, 10);
    }

    public override bool CanProgress()
    {   
        // Debug.Log("RANDOM NUMBER: " + random);
        if(isActive) {
            PThiefUpdateDelay?.Invoke();
            //timerCR = StartCoroutine(Timer());
        }
        if(isActive && random == 6) {
            //Debug.Log("Stopping Input!");
            PThiefIDisable?.Invoke();
            random = UnityEngine.Random.Range(0, 10);
        }
        else if(isActive && random != 6) {
            random = UnityEngine.Random.Range(0, 10);
        }
        return true;
    }

    public override void StartHazard()
    {
        isActive = true;
        Debug.Log("Performance Thief Started.");
        PThiefStarted?.Invoke();
    }

    public override void StopHazard()
    {
        if(isActive){
            Debug.Log("Performance Thief Ended.");
            PThiefEnded?.Invoke();
            timerCR = null;
            //StopAllCoroutines();
            isActive = false;
        }
        else {
            Debug.Log("Performance Thief Already Stopped");
        }
    }

    // public IEnumerator Timer(float x = 4f) {
    //     yield return new WaitForSeconds(x);
    //     // Debug.Log("Performance Modifier: " + pTime);
    //     pTime = UnityEngine.Random.Range(0.01f, 0.09f);

    // }

    public void BIOSPerformanceHandler(int index) {
        switch (index) {
            case 0: Debug.Log("Case 0"); break;
            case 1: Debug.Log("Case 1"); break;
            case 2: {
                StopHazard();
                break;
            }
            default: break;
        }
    }
}
