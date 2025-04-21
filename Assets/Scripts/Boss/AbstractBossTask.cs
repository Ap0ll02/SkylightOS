using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// Using the power of OPP to create a similar but different boss task
/// We should add any boss specific task behavior here.
/// Alright I guess I shall go on and work on Nyan cat now
/// </summary>
public abstract class AbstractBossTask : AbstractTask
{

    /// <summary>  
    /// Event triggered when the boss task is finished.  
    /// </summary>  
    public event System.Action OnBossTaskFinished;

    /// <summary>  
    /// Method to invoke the boss finished event.  
    /// </summary>  
    protected void TriggerBossTaskFinished()
    {
        OnBossTaskFinished?.Invoke();
    }

}
