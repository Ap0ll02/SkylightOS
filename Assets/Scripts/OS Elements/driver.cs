using UnityEngine;
using TMPro;

public class driver : MonoBehaviour
{
    public GameObject system_menu;
    public TMP_Text driver_desc;
    public DriverGame dgs;
    public GameObject update_panel;
    void Awake() {
        // FIXME When Garrett updates this menu the reference will need to be reacquired
        system_menu = GetComponentInParent<SystemWindow>().gameObject;
        //dgs = GameObject.Find("DriverMGWindow").GetComponent<DriverGame>();
        update_panel.GetComponent<CanvasGroup>().alpha = 1;
        update_panel.GetComponent<CanvasGroup>().interactable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        update_panel.GetComponent<CanvasGroup>().alpha = 0;
        update_panel.GetComponent<CanvasGroup>().interactable = false;
    }

    public void GameOn() {
        //dgs.gameObject.SetActive(true);
    }

}