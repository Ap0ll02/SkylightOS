using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// ##Author Quinn Contaldi Lord Knight Commander of Castle Catgirl
/// IDK its another fucking arrow
/// </summary>
public class RainbowArrow : Arrows
{
    //@var speed controls the speed 
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        // Testing out the movement for our poor arrows 
        transform.position += Vector3.up * speed * Time.deltaTime;
        checkDeystroy();
    }
}
