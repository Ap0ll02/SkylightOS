using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractManager : MonoBehaviour
{

    public abstract void StartHazard();
    public abstract void StopHazard();
    public abstract bool CanProgress();
}
