using TMPro;
using UnityEngine;

public class FileSystemWindow : MonoBehaviour
{
    public TMP_Text status_text;

    public GameObject recover_btn;
    // Start is called before the first frame update
    public void Start()
    {
        GetComponent<BasicWindow>().CloseWindow();
    }
    public void UpdateStatus(string status, bool interactable){
        status_text.text = status;
        recover_btn.SetActive(interactable);
    }
}
