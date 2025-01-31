using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
/// <summary>
/// System resources, can be used for multiple different tasks. All you gotta do is set the states and it should run everything else basically
/// Garrett Sharp
/// </summary>
public class SystemResourcesWindow : MonoBehaviour
{
    // Enum to track the state of the GPU
    public enum GPUStatus
    {
        OK,
        WARNING,
        CRITICAL
    }

    // Enum to track the state of the RAM
    public enum RAMStatus
    {
        OK,
        WARNING,
        CRITICAL
    }

    // Current status of the GPU
    public GPUStatus currentGPUStatus;

    // Current status of the RAM
    public RAMStatus currentRAMStatus;

    // References to the GPU status text
    [SerializeField] TMP_Text GPUStatusText;

    // References to the RAM status text
    [SerializeField] TMP_Text RAMStatusText;

    // References to the buttons
    [SerializeField] GameObject GPUButton;
    [SerializeField] GameObject RAMButton;

    // Reference to the DiagnosisWindow
    [SerializeField] BasicWindow minigameWindow;

    // Reference to the window
    [SerializeField] BasicWindow window;


    // Set the default status of the system resources
    public void Awake()
    {
        currentGPUStatus = GPUStatus.OK;
        currentRAMStatus = RAMStatus.OK;
        window = GetComponentInParent<BasicWindow>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSystemResourcesText();

        // Add button listeners
        GPUButton.GetComponent<Button>().onClick.AddListener(OpenMinigameWindow);
        RAMButton.GetComponent<Button>().onClick.AddListener(OpenMinigameWindow);
    }

    private void OnEnable()
    {
        window.OnWindowOpen += UpdateSystemResourcesText;
    }

    private void OnDisable()
    {
        window.OnWindowOpen -= UpdateSystemResourcesText;
    }

    // Set the system resources
    public void SetSystemResources(GPUStatus newGPUStatus, RAMStatus newRAMStatus)
    {
        currentGPUStatus = newGPUStatus;
        currentRAMStatus = newRAMStatus;
    }

    public void UpdateSystemResourcesText()
    {
        switch (currentGPUStatus)
        {
            case (GPUStatus.OK):
                GPUStatusText.text = "OK";
                GPUButton.SetActive(false);
                break;
            case (GPUStatus.WARNING):
                GPUStatusText.text = "WARNING";
                break;
            case (GPUStatus.CRITICAL):
                GPUStatusText.text = "CRITICAL";
                GPUButton.SetActive(true);
                break;
        }

        switch (currentRAMStatus)
        {
            case (RAMStatus.OK):
                RAMStatusText.text = "OK";
                RAMButton.SetActive(false);
                break;
            case (RAMStatus.WARNING):
                RAMStatusText.text = "WARNING";
                break;
            case (RAMStatus.CRITICAL):
                RAMStatusText.text = "CRITICAL";
                RAMButton.SetActive(true);
                break;
        }
    }

    // Method to open the MinigameWindow
    public void OpenMinigameWindow()
    {
        minigameWindow.OpenWindow();
    }
}
