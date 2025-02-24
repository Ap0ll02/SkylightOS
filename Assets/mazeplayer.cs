using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeplayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {        
        Debug.Log("Collision");
    }
}
