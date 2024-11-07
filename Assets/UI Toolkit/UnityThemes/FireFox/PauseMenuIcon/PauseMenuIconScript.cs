using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class IconManager : MonoBehaviour
{
    public VisualElement pauseMenuIcon ; // This contains a reference to the Pause Menu Icon. This is what is present on the desktop tool bar 
    private UIDocument pauseMenu; //this contains the reference to the Pause Menu, which is spawned once the tool bar is clicked
    private bool isPaused = false; 

    private void OnEnable()
    {
        // This is what grabs the reference to our rootVisualElement, which is our PauseMenuIcon
        pauseMenuIcon = GetComponent<UIDocument>().rootVisualElement;
        // We want to specifically target the button of our pause menu icon 
        var pauseMenuIconButton = pauseMenuIcon.Q<Button>("pauseMenuIconButton");
        if (pauseMenuIconButton != null)
        {
            //creates a callback event in order to execute a function when the Pause Menu Icon is clicked using a lamda HOW COOL
            pauseMenuIconButton.RegisterCallback<ClickEvent>((ClickEvent evt) => { TogglePauseMenu();});
            //creates a callback event in order to execute a function when the Pause Menu Icon is envoked by escape key using a lamda HOW COOL
            pauseMenuIconButton.RegisterCallback<KeyDownEvent>((KeyDownEvent evt) => {if(evt.keyCode == KeyCode.Escape) TogglePauseMenu();});
        }
        else
        {
            Debug.LogWarning("Pause Icon Button not found! Check the name in UI Builder. Should be the button name we are calling NYAAAAA");
        }
        
        // We want to find the game object caused Pause Menu. This Pause menu should be placed in the scene 
        GameObject pauseMenuObject = GameObject.Find("PauseMenu");
        // If we cant find it we should probably throw an error 
        if (pauseMenuObject != null)
        {
            // If we find the Object then sick! let's just make sure to grab the UI component from it 
            pauseMenu = pauseMenuObject.GetComponent<UIDocument>();
            // If it does not exist with in this scene we want to throw another error 
            if (pauseMenu == null)
            {
                Debug.LogError("Pause Menu UIDocument component not found on the GameObject named 'PauseMenu'!");
                return;
            }
        }
        else
        {
            Debug.LogError("GameObject named 'PauseMenu' not found in the scene!");
            return;
        }
        // root then style then how that style is displayed then we set the Display style as none in order to turn it off 
        pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
    }

    // This is the function that will actually pause the game and then call the pause menu
    private void TogglePauseMenu()
    {

        isPaused = !isPaused;
        
        // Show or hide the PauseMenu based on the current state
        if (isPaused)
        {
            pauseMenu.rootVisualElement.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
            Time.timeScale = 1;
        }
    }
}
