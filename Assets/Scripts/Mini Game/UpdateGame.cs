using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGame : MonoBehaviour
{
    public BasicWindow window;

    void Awake()
    {
        window = GetComponent<BasicWindow>();
    }
    // Start is called before the first frame update
    void Start()
    {
        window.isClosable = false;
        window.CloseWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
