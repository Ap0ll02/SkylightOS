using TMPro;
using UnityEngine;

public class FileSystemWindow : MonoBehaviour
{
    public TMP_Text status_text;
    public GameObject recover_btn;
    public GameObject recover_panel;

    public enum WindowState
    {
        Working,
        NotWorking,
        NotWorkingInteractable
    }

    public void Awake()
    {
        SetState(WindowState.Working);
    }

    // Start is called before the first frame update  
    public void Start()
    {
        GetComponent<BasicWindow>().CloseWindow();
    }

    public void UpdateStatus(string status, bool panel, bool interactable)
    {
        status_text.text = status;
        recover_btn.SetActive(interactable);
        recover_panel.SetActive(panel);
    }

    public void SetState(WindowState state)
    {
        switch (state)
        {
            case WindowState.Working:
                UpdateStatus("EXT6 File System working as normal", false, false);
                break;
            case WindowState.NotWorking:
                UpdateStatus("EXT6 File System Corrupted.\n Please Recover File System", true, false);
                break;
            case WindowState.NotWorkingInteractable:
                UpdateStatus("EXT6 File System Corrupted.\n Please Recover File System", true, true);
                break;
        }
    }
}
