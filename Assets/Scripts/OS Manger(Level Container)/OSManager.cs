using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.PackageManager.UI;
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

    // Reference to the boss task
    [SerializeField] public AbstractBossTask bossTask;

    // Reference to boss task button (temporary)
    [SerializeField] public Button bossTaskButton;

    // Reference to the GameObject containing all tasks
    [SerializeField] public GameObject tasksContainer;

    // Reference to the basic window
    [SerializeField] public BasicWindow window;

    void Awake()
    {
        window = GetComponent<BasicWindow>();
    }

    // Start is called before the first frame update
    void Start()
    {
        window.ForceCloseWindow();
        GetTasksFromContainer();
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

    // Method to start the boss task
    public void StartBossTask()
    {
        if (bossTask == null)
        {
            Debug.LogError("Boss task is null, you need to set the boss task");
            return;
        }
        currentTask = bossTask;
        bossTask.gameObject.SetActive(true);
        bossTask.startTask();

        // Disable the boss task button
        bossTaskButton.interactable = false;
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
                if (tasks[i].isComplete)
                {
                    TaskObject taskObject = taskButtons[i].GetComponentInParent<TaskObject>();
                    if (taskObject != null)
                    {
                        taskObject.SetTaskDone();
                    }
                }
                taskButtons[i].interactable = !tasks[i].isComplete;
            }

            // Check if all tasks are complete
            if (AllTasksComplete())
            {
                CreateBossTaskButton();
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
            GameObject taskObjectPrefab = Instantiate(buttonPrefab, buttonContainer);
            TaskObject taskObject = taskObjectPrefab.GetComponent<TaskObject>();
            taskObject.SetTaskInfo(tasks[i].taskTitle, tasks[i].taskDescription);
            taskObject.SetTaskIndex(i);

            // Add the button to the list of task buttons
            Button buttonComponent = taskObjectPrefab.GetComponentInChildren<Button>();
            taskButtons.Add(buttonComponent);
        }
    }

    // Method to create the boss task button
    void CreateBossTaskButton()
    {
        bossTaskButton.gameObject.SetActive(true);

        // Add the button to the list of task buttons
        //bossTaskButton = bossButton.GetComponentInChildren<Button>();
        //bossTaskButton.onClick.AddListener(StartBossTask);
        //bossTaskButton.interactable = true;
    }

    // Method to check if all tasks are complete
    bool AllTasksComplete()
    {
        foreach (var task in tasks)
        {
            if (!task.isComplete)
            {
                return false;
            }
        }
        return true;
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

    // Method to get tasks from the tasksContainer GameObject
    void GetTasksFromContainer()
    {
        tasks = new List<AbstractTask>();
        foreach (Transform child in tasksContainer.transform)
        {
            AbstractTask task = child.GetComponent<AbstractTask>();
            if (task != null)
            {
                tasks.Add(task);
            }
        }
    }
}
