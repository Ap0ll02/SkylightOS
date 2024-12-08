using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
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

    // Enum to track the state of the wifi
    public enum WifiState
    {
        Connected,
        Disconnected
    }

    // Current state of the wifi
    public WifiState currentWifiState;

    // Getting references to the diagnosis window and setting the default state
    void Awake()
    {
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>();
        currentWifiState = WifiState.Connected; // Default state
        UpdateWifiState();
    }

    // Disabling the game object on start
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update the wifi menu on enable if needed.
    void OnEnable()
    {
        CheckWifiState();
        UpdateWifiState();
        transform.SetAsLastSibling();
    }

    // Called by the wifi button
    public void ToggleMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // When you press the 'run diagnossis' button
    public void RunDiagnosisPressed()
    {
        diagnosisWindow.OpenWindow();
        gameObject.SetActive(false);
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
        }
        else if (currentWifiState == WifiState.Disconnected)
        {
            wifiStateText.text = "Unable to connect to network";
            wifiButton.SetActive(true);
        }
        else
        {
            wifiStateText.text = "You broke it dumbfuck";
        }
    }
}
