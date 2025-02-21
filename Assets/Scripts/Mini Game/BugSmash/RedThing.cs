using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedThing : AbstractBug
{
    public void Start()
    {
        score = 5;
        hearts = 1;
        damage = 1;
        moveSpeed = UnityEngine.Random.Range(2.5f,4.5f);
        base.Start();
    }

    public void Update()
    {
        TrackPlayer();
        base.GameOverCheck();
    }
}

