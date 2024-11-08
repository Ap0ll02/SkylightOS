using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ExpandedWifiMenu : MonoBehaviour
{
    // Used to open the diagnosis window
    [SerializeField] GameObject diagnosisWindow;

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

    void Awake()
    {
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>().gameObject;
        currentWifiState = WifiState.Connected; // Default state
        UpdateWifiState();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update the wifi menu on enable if needed.
    void OnEnable()
    {
        CheckWifiState();
        UpdateWifiState();
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
        diagnosisWindow.SetActive(true);
        gameObject.SetActive(true);
    }

    // Method to check the current wifi state
    WifiState CheckWifiState()
    {
        Debug.Log("Current Wifi State: " + currentWifiState);
        return currentWifiState;
    }

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
