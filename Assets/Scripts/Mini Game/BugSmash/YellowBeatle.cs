using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class YellowBeatle : AbstractBug
{
    // Start is called before the first frame update
    void Start()
    {
        score = 20;
        hearts = 2;
        damage = 2;
        moveSpeed = UnityEngine.Random.Range(1.5f,2.5f); //( 1.5, 2.5)

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }
}
