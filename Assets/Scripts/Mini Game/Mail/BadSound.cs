using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSound : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        Destroy(gameObject, 2);
    }

}
