using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverBeatle : AbstractBug
{
    void Start()
    {
        score = 30;
        hearts = 3;
        damage = 2;
        moveSpeed = UnityEngine.Random.Range(1f,2f);;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }
}
