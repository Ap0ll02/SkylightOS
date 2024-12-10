using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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

    // Enum to track the state of the CPU
    public enum CPUStatus
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

    // Current status of the CPU
    public CPUStatus currentCPUStatus;

    // Current status of the RAM
    public RAMStatus currentRAMStatus;

    // References to the GPU status text
    [SerializeField] TMP_Text GPUStatusText;

    // References to the CPU status text
    [SerializeField] TMP_Text CPUStatusText;

    // References to the RAM status text
    [SerializeField] TMP_Text RAMStatusText;

    // References to the buttons
    [SerializeField] GameObject GPUButton;
    [SerializeField] GameObject CPUButton;
    [SerializeField] GameObject RAMButton;

    // Reference to the DiagnosisWindow
    [SerializeField] DiagnosisWindow diagnosisWindow;

    // Set the default status of the system resources
    public void awake()
    {
        currentGPUStatus = GPUStatus.OK;
        currentCPUStatus = CPUStatus.OK;
        currentRAMStatus = RAMStatus.OK;
        diagnosisWindow = FindObjectOfType<DiagnosisWindow>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSystemResourcesText();
        gameObject.SetActive(false);

        // Add button listeners
        GPUButton.GetComponent<Button>().onClick.AddListener(OpenDiagnosisWindow);
        CPUButton.GetComponent<Button>().onClick.AddListener(OpenDiagnosisWindow);
        RAMButton.GetComponent<Button>().onClick.AddListener(OpenDiagnosisWindow);
    }

    // When the window is enabled, update the text
    public void OnEnable()
    {
        UpdateSystemResourcesText();
    }

    public void ToggleMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            gameObject.transform.SetAsLastSibling();
        }
    }

    // Set the system resources
    public void SetSystemResources(GPUStatus newGPUStatus, CPUStatus newCPUStatus, RAMStatus newRAMStatus)
    {
        currentGPUStatus = newGPUStatus;
        currentCPUStatus = newCPUStatus;
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

        switch (currentCPUStatus)
        {
            case (CPUStatus.OK):
                CPUStatusText.text = "OK";
                CPUButton.SetActive(false);
                break;
            case (CPUStatus.WARNING):
                CPUStatusText.text = "WARNING";
                break;
            case (CPUStatus.CRITICAL):
                CPUStatusText.text = "CRITICAL";
                CPUButton.SetActive(true);
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

    // Method to open the DiagnosisWindow
    public void OpenDiagnosisWindow()
    {
        diagnosisWindow.OpenWindow();
    }
}
