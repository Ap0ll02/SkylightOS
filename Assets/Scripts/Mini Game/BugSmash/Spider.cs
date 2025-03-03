using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : AbstractBug
{
    // Start is called before the first frame update
    public void Start()
    {
        score = 40;
        moveSpeed = UnityEngine.Random.Range(1f, 3f);
        damage = 3;
        hearts = 6;
        base.Start();
    }

    public void Update()
    {
        TrackPlayer();
        base.GameOverCheck();
    }

    // Update is called once per frame
}
