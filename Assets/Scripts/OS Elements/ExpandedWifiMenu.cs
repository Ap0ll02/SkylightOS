using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
//using UnityEditor.PackageManager.UI;
/// <summary>
/// Garrett Sharp
/// The wifi menu that is expanded when the wifi button is pressed
/// Used to show the status of the wifi
/// </summary>
public class ExpandedWifiMenu : MonoBehaviour
{
    // Used to open the diagnosis window
    [SerializeField] DiagnosisWindow diagnosisWindow;

    // TextMeshPro object to display the wifi state
    [SerializeField] TMP_Text wifiStateText;

    // Button to be hidden when connected
    [SerializeField] GameObject wifiButton;

    // Reference to the window
    [SerializeField] BasicWindow window;

    // Reference to the wifi icon
    [SerializeField] GameObject wifiIcon;

    // Enum to track the state of the wifi
    public enum WifiState
    {
        Connected,
        Disconnected,
        DisconnectedInteractable
    }

    // Current state of the wifi
    public WifiState currentWifiState;

    // Getting references to the diagnosis window and setting the default state
    void Awake()
    {
        wifiStateText = GetComponentInChildren<TMP_Text>();
        wifiButton = GetComponentInChildren<Button>().gameObject;
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>();
        currentWifiState = WifiState.Connected; // Default state
        window = GetComponent<BasicWindow>();
        UpdateWifiState();
    }

    // Called when the diagnosis window is closed
    public void OnEnable()
    {
        window.OnWindowOpen += OnWindowOpen;
        window.CloseWindow();
    }

    public void OnDisable()
    {
        window.OnWindowOpen -= OnWindowOpen;
    }

    public void OnWindowOpen()
    {
        CheckWifiState();
        UpdateWifiState();
    }

    // When you press the 'run diagnossis' button
    public void RunDiagnosisPressed()
    {
        diagnosisWindow.OpenWindow();
        window.CloseWindow();
    }

    // Method to check the current wifi state
    WifiState CheckWifiState()
    {
        Debug.Log("Current Wifi State: " + currentWifiState);
        return currentWifiState;
    }

    // Method to set the wifi state
    public void SetWifiState(WifiState state)
    {
        currentWifiState = state;
        UpdateWifiState();
    }

    // Method to update the TextMeshPro object with the current wifi state  
    void UpdateWifiState()
    {
        if (currentWifiState == WifiState.Connected)
        {
            wifiStateText.text = "Successfully Connected to Network";
            wifiButton.SetActive(false);
            wifiIcon.transform.GetChild(0).gameObject.SetActive(true);
            wifiIcon.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (currentWifiState == WifiState.Disconnected)
        {
            wifiStateText.text = "Unable to connect to network";
            wifiButton.SetActive(false);
            wifiIcon.transform.GetChild(0).gameObject.SetActive(false);
            wifiIcon.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (currentWifiState == WifiState.DisconnectedInteractable)
        {
            wifiStateText.text = "Unable to connect to network";
            wifiButton.SetActive(true);
            wifiIcon.transform.GetChild(0).gameObject.SetActive(false);
            wifiIcon.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            wifiStateText.text = "You broke it dumbfuck";
        }
    }
}
