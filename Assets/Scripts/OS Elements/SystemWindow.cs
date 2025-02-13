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

    // Reference to the window
    public BasicWindow window;

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
        window = GetComponent<BasicWindow>();
        currentState = WindowState.MAIN;
        currentPanel = mainPanel;
        resourcesPanel.ClosePanel();
        driversPanel.ClosePanel();
        updatePanel.ClosePanel();
        backButton.SetActive(false);
    }

    private void Start()
    {
        window.CloseWindow();
    }
    // Switches the state of the window
    public void SwitchState(WindowState state)
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

    // Open the Resources window
    public void OpenResources()
    {
        SwitchState(WindowState.RESOURCES);
    }

    // Open the Drivers window
    public void OpenDrivers()
    {
        SwitchState(WindowState.DRIVERS);
    }

    // Open the Update window
    public void OpenUpdate()
    {
        SwitchState(WindowState.UPDATE);
    }

    // When the window is enabled, update the text
    public void OnEnable()
    {
        window.OnWindowOpen += BackButton;
    }

    public void OnDisable()
    {
        window.OnWindowOpen -= BackButton;
    }
}
