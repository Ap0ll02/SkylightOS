using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
/// <summary>
/// Author L:ord Quinn Barron of catgirls
/// Down arrow to hold our specific down arrow features
/// This class inherates from the Arrows parent class
/// We want this child object to just focus on Right Arrow specific logic 
/// </summary>
public class DownArrow : Arrows
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
    }
}
