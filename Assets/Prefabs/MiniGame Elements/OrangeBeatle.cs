using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBeatle : AbstractBug
{
    // Start is called before the first frame update
    void Start()
    {
        score = 10;
        hearts = 1;
        damage = 1;
        moveSpeed = UnityEngine.Random.Range(3f,4.5f);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
    }
}
