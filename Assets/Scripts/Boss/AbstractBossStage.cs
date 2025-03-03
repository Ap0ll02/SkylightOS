using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBossStage : MonoBehaviour
{
    // Ever stage should have a reference to a specific boss manager that it can
    public BossManager bossManager;
    public abstract void BossStartStage();
    public abstract void BossEndStage();

}
