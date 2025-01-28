using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemWindow : MonoBehaviour
{
    // Enum to track the state of the window
    public enum WindowState
    {
        MAIN,
        RESOURCES,
        DRIVERS,
        UPDATE
    }

    // Current state of the window
    public WindowState currentState;

    // The back button
    public GameObject backButton;

    // The panels for each state
    public BasicWindowPanel mainPanel;
    public BasicWindowPanel resourcesPanel;
    public BasicWindowPanel driversPanel;
    public BasicWindowPanel updatePanel;

    // Reference to the current open panel
    public BasicWindowPanel currentPanel;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        currentState = WindowState.MAIN;
        currentPanel = mainPanel;
    }

    // Switches the state of the window
    void SwitchState(WindowState state)
    {
        if (currentPanel != null)
        {
            currentPanel.ClosePanel();
        }

        switch (state)
        {
            case WindowState.MAIN:
                backButton.SetActive(false);
                mainPanel.OpenPanel();
                currentPanel = mainPanel;
                currentState = WindowState.MAIN;
                break;
            case WindowState.RESOURCES:
                backButton.SetActive(true);
                resourcesPanel.OpenPanel();
                currentPanel = resourcesPanel;
                currentState = WindowState.RESOURCES;
                break;
            case WindowState.DRIVERS:
                backButton.SetActive(true);
                driversPanel.OpenPanel();
                currentPanel = driversPanel;
                currentState = WindowState.DRIVERS;
                break;
            case WindowState.UPDATE:
                backButton.SetActive(true);
                updatePanel.OpenPanel();
                currentPanel = updatePanel;
                currentState = WindowState.UPDATE;
                break;
        }
    }

    // Back button function
    public void BackButton()
    {
        if (currentState != WindowState.MAIN)
        {
            SwitchState(WindowState.MAIN);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
