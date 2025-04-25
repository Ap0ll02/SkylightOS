using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : Projectile
{
    Vector3 motion;
    public override void Start() {
        base.Start();
        motion = transform.position;
    }
    void Update()
    {
        Vector3 newMotion = transform.position;

    }
}
