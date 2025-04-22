using UnityEngine;
using TMPro;

public class driver : MonoBehaviour
{
    public GameObject system_menu;
    public TMP_Text driver_desc;
    public DriverGame dgs;
    public GameObject update_panel;
    public GameObject updateButton;

    public enum DriversState
    {
        Working,
        NotWorking,
        NotWorkingInteractable
    }

    void Awake() {
        // FIXME When Garrett updates this menu the reference will need to be reacquired
        UpdateState(DriversState.Working);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateState(DriversState newState)
    {
        switch (newState)
        {
            case DriversState.Working:
                driver_desc.text = "Drivers are working normally.";
                updateButton.SetActive(false);
                break;
            case DriversState.NotWorking:
                driver_desc.text = "Drivers out of date. Updates required.";
                updateButton.SetActive(false);
                break;
            case DriversState.NotWorkingInteractable:
                driver_desc.text = "Drivers out of date. Updates required.";
                updateButton.SetActive(true);
                break;
        }
    }

}