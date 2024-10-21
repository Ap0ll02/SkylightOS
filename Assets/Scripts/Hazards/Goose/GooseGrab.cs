using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GooseGrab : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform tranform;

    void Start()
    {
        if (tranform != null){
            Debug.Log("No transform component detected");
        }
        tranform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (tranform.position == Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.6f,0,0)))
        // {
        //     Debug.Log("got you");
        // }

    }
}
