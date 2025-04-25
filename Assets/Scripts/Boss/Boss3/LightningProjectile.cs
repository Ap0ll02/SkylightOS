using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : Projectile
{
    Vector3 motion;
    float smallMotionNumber = 0.11f;
    public override void Start() {
        base.Start();
        motion = transform.position;
    }
    public override void Update()
    {
        base.Update();
        Vector3 newMotion = transform.position;
        if(Mathf.Abs(Vector3.Distance(newMotion, motion)) < 0.11f) {
            CleanUp();
        }
        motion = transform.position;
    }
}
