using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OSManager : MonoBehaviour
{
    // List to hold the tasks
    [SerializeField] public List<AbstractTask> tasks;

    // Reference to the button prefab
    [SerializeField] public GameObject buttonPrefab;

    // Parent transform to hold the buttons
    [SerializeField] public Transform buttonContainer;

    // Start is called before the first frame update
    void Start()
    {
        CreateTaskButtons();
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
            tasks[taskIndex].gameObject.SetActive(true);
            tasks[taskIndex].startTask();
        }
        else
        {
            Debug.LogError("Invalid task index: " + taskIndex);
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
        }
    }
}
