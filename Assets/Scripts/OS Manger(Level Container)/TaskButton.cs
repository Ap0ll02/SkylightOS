using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Garrett Sharp
/// Used for the buttons, assign
/// </summary>
public class TaskButton : MonoBehaviour
{
    // Task index, should correlate with the task number that should be activated when the button is pressed. 
    [SerializeField] public int taskIndex;

    // OSManager, will send what task to start.
    private OSManager osManager;

    // Finding the OSManager
    void Awake()
    {
        osManager = FindObjectOfType<OSManager>();
    }

    // Adding the listener for when the button is clicked
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    // OS Manager starting task
    private void OnButtonClick()
    {
        osManager.OnTaskButtonPress(taskIndex);
    }

    public void SetTaskIndex(int index)
    {
        taskIndex = index;
    }
}
