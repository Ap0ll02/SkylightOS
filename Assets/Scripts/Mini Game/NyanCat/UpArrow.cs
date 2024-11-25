using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// Don't yell at me for all the similarities between these classes; the object-oriented programmer in me needs to make these separate.
/// The reason being is that I want to define different graphics based on different arrows.
/// So making different arrows have their own class and be extendable is important.
/// Anyways, enough yapping, this is the up arrow class.
/// We want this child object to just focus on Right Arrow specific logic
/// </summary>
public class ArrowUp : Arrows
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
