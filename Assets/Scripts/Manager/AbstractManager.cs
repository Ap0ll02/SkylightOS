using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractManager : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public Difficulty difficulty = Difficulty.Medium;

    public abstract void StartHazard();
    public abstract void StopHazard();
    public abstract bool CanProgress();
}
