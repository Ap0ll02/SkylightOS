using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskObject : MonoBehaviour
{
    public int taskIndex;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI buttonText;

    private OSManager osManager;

    private void Awake()
    {
        osManager = FindObjectOfType<OSManager>();
        titleText.text = "Task";
        descriptionText.text = "Set the task description brudah";
    }

    void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        osManager.OnTaskButtonPress(taskIndex);
        buttonText.text = "Current Task";
    }

    public void SetTaskIndex(int index)
    {
        taskIndex = index;
    }

    public void SetTaskInfo(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
    }

    public void SetTaskDone()
    {
        buttonText.text = "Task Complete";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
