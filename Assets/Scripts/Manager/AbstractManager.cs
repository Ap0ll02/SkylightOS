using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractManager : MonoBehaviour
{
    public OSManager.Difficulty difficulty;

    public abstract void StartHazard();
    public abstract void StopHazard();
    public abstract bool CanProgress();

    public void SetDifficulty(OSManager.Difficulty difficulty)
    {
        this.difficulty = difficulty;
    }
}
