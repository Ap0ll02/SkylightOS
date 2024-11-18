using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// This will serve as the parent class for all fucking arrows.
/// I'm not quite sure if I want to eventually turn this thing into an abstract base class.
/// Currently, arrows don't have different implementations for the same methods.
/// Thus, having just a parent class and allowing the child arrow objects to extend their behavior is good enough.
/// </summary>
public class Arrows : MonoBehaviour
{
    public float speed;
    
    // Fucking amazing brain moment just make a function in the parent class that checks if its position is greater then 7 Y. If so BYE BYE OBJECT
   public void checkDeystroy()
    {
        // The arrows only move up so we just compare its Y location 
        if (transform.position.y > 8)
        {
            Destroy(gameObject);
        }
    }
}
