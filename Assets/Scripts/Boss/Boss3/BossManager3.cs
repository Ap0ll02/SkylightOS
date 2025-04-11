using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Author Quinn or something like that
/// Use this class to manage things that need to persist past individual stages
///
public class BossManager3 : BossManager
{
    public void Start()
    {
        currentBossStageIndex = 0;
        Debug.Log("Starting Boss Fight");
        NextStage();
    }

}
