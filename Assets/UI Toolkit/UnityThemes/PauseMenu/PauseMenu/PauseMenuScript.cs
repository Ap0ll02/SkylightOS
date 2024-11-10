using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScript : MonoBehaviour
{
    public VisualElement pauseMenu ; // This contains a reference to the Pause Menu Icon. This is what is present on the desktop tool bar 
    private void OnEnable()
    {
        pauseMenu = GetComponent<UIDocument>().rootVisualElement;
        var buttonPlay = pauseMenu.Q<Button>("Play");
        if(buttonPlay == null)
            Debug.Log("Reference to button is missing");
        var buttonMenu = pauseMenu.Q<Button>("Menu");
        if(buttonMenu == null)
            Debug.Log("Reference to button is missing");
        var buttonQuit = pauseMenu.Q<Button>("Quit");
        if(buttonQuit == null)
            Debug.Log("Reference to button is missing");


    }
}
