using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractInsect : AbstractEnemy
{
    public float BugSpeedBuff = 0.5f;
    public float BugSpeed;
    private float percent;
    private float duration =0;

    public override void SlowDownHit(int damage = 0, float percent = 1, float duration = 0)
    {
        currentHealth -= damage;
    }

}
