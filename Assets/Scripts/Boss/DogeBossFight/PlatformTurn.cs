using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTurn: MonoBehaviour
{
    public int turnCoolDown;

    public int turnAngle;
    // Start is called before the first frame update
    IEnumerator PlatformCooldown()
    {
        yield return new WaitForSeconds(turnCoolDown);
        PlatformTurning();
    }

    public void PlatformTurning()
    {
        transform.Rotate(0, 0, turnAngle);
        StartCoroutine(PlatformCooldown());
    }

    // We only start the turning once the level is activated
    private void OnEnable()
    {
        StartCoroutine(PlatformCooldown());
    }

}
