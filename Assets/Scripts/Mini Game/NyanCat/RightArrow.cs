using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// We want this child object to just focus on Right Arrow specific logic
/// </summary>
public class RightArrow : Arrows
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        // We need to be moving our arrow, This is defined in parent Class
        Move();
        OutOfBounds(transform.position.y);
    }

}
