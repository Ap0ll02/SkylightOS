using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceThiefManager : AbstractManager
{
    public override bool CanProgress()
    {
        throw new System.NotImplementedException();
    }

    public override void StartHazard()
    {
        // TODO : Just broadcast event, have tasks listen to it.
    }

    public override void StopHazard()
    {
        throw new System.NotImplementedException();
    }
}
