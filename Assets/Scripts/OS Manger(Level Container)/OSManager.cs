using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// Garrett Sharp
/// OS Manager
/// Basically manages the tasks and buttons to start those tasks
/// Will also manage the boss fight after the tasks are completed
/// </summary>
public class OSManager : MonoBehaviour
{
    // List to hold the tasks
    [SerializeField] public List<AbstractTask> tasks;

    // Reference to the button prefab
    [SerializeField] public GameObject buttonPrefab;

    // Parent transform to hold the buttons
    [SerializeField] public Transform buttonContainer;

    // Reference to the current task
    [SerializeField] public AbstractTask currentTask;

    // List to hold the task buttons
    private List<Button> taskButtons = new List<Button>();

    // Start is called before the first frame update
    void Start()
    {
        CreateTaskButtons();
        SubscribeToTaskEvents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to start a task based on an index
    public void StartTask(int taskIndex)
    {
        if (taskIndex >= 0 && taskIndex < tasks.Count)
        {
            currentTask = tasks[taskIndex];
            currentTask.gameObject.SetActive(true);
            currentTask.startTask();

            // Disable all buttons
            foreach (var button in taskButtons)
            {
                button.interactable = false;
            }
        }
        else
        {
            Debug.LogError("Invalid task index: " + taskIndex);
        }
    }

    // Method to finish a task based on an index
    public void FinishTask()
    {
        if (currentTask != null)
        {
            currentTask.isComplete = true;
            currentTask = null;

            // Enable all buttons except the completed task buttons
            for (int i = 0; i < taskButtons.Count; i++)
            {
                taskButtons[i].interactable = !tasks[i].isComplete;
            }
        }
    }

    // Example method to handle button press and start a task
    public void OnTaskButtonPress(int taskIndex)
    {
        StartTask(taskIndex);
    }

    // Method to create task buttons dynamically
    void CreateTaskButtons()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject taskButton = Instantiate(buttonPrefab, buttonContainer);
            taskButton.GetComponentInChildren<TMP_Text>().text = "Task " + (i + 1);

            // Find the TaskButton script on the child object and set the task index
            TaskButton taskButtonScript = taskButton.GetComponentInChildren<TaskButton>();
            taskButtonScript.SetTaskIndex(i);

            // Add the button to the list of task buttons
            Button buttonComponent = taskButton.GetComponentInChildren<Button>();
            taskButtons.Add(buttonComponent);
        }
    }

    // Method to subscribe to task events
    void SubscribeToTaskEvents()
    {
        AbstractTask.OnTaskCompleted += FinishTask;
    }

    // Unsubscribe from events when the object is destroyed
    void OnDestroy()
    {
        AbstractTask.OnTaskCompleted -= FinishTask;
    }
}
