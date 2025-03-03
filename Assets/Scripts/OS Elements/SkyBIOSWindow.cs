using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SkyBIOSWindow : MonoBehaviour
{
    // Reference to the peripheral stuff
    public TMP_Text peripheralText;
    public GameObject openPeripheralWindowButton;
    public PeripheralWindow peripheralWindow;

    // Reference to bios window itself
    public BasicWindow window;

    public enum PeripheralState
    {
        Working,
        NotWorking,
        NotWorkingInteractable
    }

    public PeripheralState currentState;

    private void Awake()
    {
        window = GetComponent<BasicWindow>();
        peripheralWindow = FindObjectOfType<PeripheralWindow>();
        SetState(PeripheralState.Working);
    }

    private void Start()
    {
        window.ForceCloseWindow();
    }


    public void SetState(PeripheralState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case PeripheralState.Working:
                peripheralText.text = "Peripherals are working";
                peripheralText.color = new Color32(0xFF, 0xED, 0x00, 0xFF);
                openPeripheralWindowButton.SetActive(false);
                break;

            case PeripheralState.NotWorking:
                peripheralText.text = "Critical! Peripheral failure!";
                peripheralText.color = Color.red;
                openPeripheralWindowButton.SetActive(false);
                break;

            case PeripheralState.NotWorkingInteractable:
                peripheralText.text = "Critical! Peripheral failure!";
                peripheralText.color = Color.red;
                openPeripheralWindowButton.SetActive(true);
                break;
        }
    }

    public void PeripheralButtonPressed()
    {
        peripheralWindow.TryOpenWindow();
    }

}
