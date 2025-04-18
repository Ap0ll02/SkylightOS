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
    private List<AbstractTask> tasks;

    // List to hold the tasks
    private List<AbstractManager> hazards;

    // Reference to the boss task
    public AbstractBossTask bossTask;

    // Reference to the button prefab
    [SerializeField] public GameObject buttonPrefab;

    // Parent transform to hold the buttons
    [SerializeField] public Transform buttonContainer;

    // Reference to the current task
    [SerializeField] public AbstractTask currentTask;

    // List to hold the task buttons
    private List<Button> taskButtons = new List<Button>();

    // Reference to the GameObject containing all tasks
    [SerializeField] public GameObject tasksContainer;

    // Reference to the GameObject containing all hazards
    [SerializeField] public GameObject hazardsContainer;

    // Reference to the basic window
    [SerializeField] public BasicWindow window;

    // Reference to the level trans button
    [SerializeField] public GameObject levelTransButton;

    // Reference to the EvidenceManager
    [SerializeField] public EvidenceManager evidenceManager;

    // Difficulty to be set in each task and hazard 
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public Difficulty difficulty;

    void Awake()
    {
        window = GetComponent<BasicWindow>();
        difficulty = (Difficulty)SaveLoad.GameDifficulty;
        if (tasksContainer != null)
        {
            tasks = GetTasksFromContainer(tasksContainer);
            SetTaskDifficulty(tasks, difficulty);
        }
        else
        {
            Debug.LogError("No tasks container found");
            return;
        }
        if (hazardsContainer != null)
        {
            hazards = GetHazardsFromContainer(hazardsContainer);
            SetHazardDifficulty(hazards, difficulty);
        }
        else
        {
            Debug.LogError("No hazards container found");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        window.ForceCloseWindow();
        CreateTaskButtons();
        SubscribeToTaskEvents();
        if (levelTransButton != null)
        {
            levelTransButton.SetActive(false);
        }
        else
        {
            Debug.LogError("LevelTransition GameObject not found.");
        }

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
            Debug.LogError("Boss task is null - so no boss fight will be started");
            return;
        }
        currentTask = bossTask;
        bossTask.startTask();
    }

    // Method to finish a task based on an index  
    public void FinishTask()
    {
        if (currentTask != null)
        {
            // Check if the current task is the boss task  
            if (currentTask == bossTask)
            {
                if (levelTransButton != null)
                {
                    levelTransButton.SetActive(true);
                    for(int i = 0; i < evidenceManager.EvidenceCount; i++)
                    {
                        SaveLoad.IncrementEvidence();
                    }
                }
                else
                {
                    Debug.LogError("LevelTransition GameObject not found.");
                }
                return;
            }

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
                StartBossTask();
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
    List<AbstractTask> GetTasksFromContainer(GameObject taskContainer)
    {
        List<AbstractTask> newTasks = new List<AbstractTask>();
        foreach (Transform child in taskContainer.transform)
        {
            AbstractTask task = child.GetComponent<AbstractTask>();
            if (task != null)
            {
                newTasks.Add(task);
            }
        }
        return newTasks;
    }

    List<AbstractManager> GetHazardsFromContainer(GameObject hazardContainer)
    {
        List<AbstractManager> newHazards = new List<AbstractManager>();
        foreach (Transform child in hazardContainer.transform)
        {
            AbstractManager hazard = child.GetComponent<AbstractManager>();
            if (hazard != null)
            {
                newHazards.Add(hazard);
            }
        }
        return newHazards;
    }

    void SetTaskDifficulty(List<AbstractTask> tasks, Difficulty difficulty)
    {
        foreach (var task in tasks)
        {
            task.SetDifficulty(difficulty);
        }
    }

    void SetHazardDifficulty(List<AbstractManager> hazards, Difficulty difficulty)
    {
        foreach (var hazard in hazards)
        {
            hazard.SetDifficulty(difficulty);
        }
    }

}
