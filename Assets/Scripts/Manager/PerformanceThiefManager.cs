using System;
using System.Collections;
using UnityEngine;

public class PerformanceThiefManager : AbstractManager
{
    public static event Action PThiefStarted;
    public static event Action PThiefEnded;
    public float pTime = 1f;
    private Coroutine timerCR;
    public override bool CanProgress()
    {
        UnityEngine.Random.InitState(System.Environment.TickCount);
        if(timerCR == null) {
            timerCR = StartCoroutine(Timer());
        }
        return true;
    }

    public override void StartHazard()
    {
        // TODO : Just broadcast event, have tasks listen to it.
        Debug.Log("Performance Thief Started.");
        PThiefStarted?.Invoke();
    }

    public override void StopHazard()
    {
        // TODO This will need to implement some condition to stop, from when the CPU
        // gets overclocked
        Debug.Log("Performance Thief Ended.");
        PThiefEnded?.Invoke();
    }

    public IEnumerator Timer() {
        while(true) {
            yield return new WaitForSeconds(4f);
            // Debug.Log("Performance Modifier: " + pTime);
            pTime = UnityEngine.Random.Range(0.01f, 0.9f);
        }
    }
}
